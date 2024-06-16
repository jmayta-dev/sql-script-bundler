using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace SSB.Presentation.WinForm;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        var builder = new ConfigurationBuilder();
        BuildConfig(builder);

        // serilog configuration for passing appsettings.json
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        Log.Logger.Information("Iniciando Aplicación.");

        try
        {
            // configure generic host
            var host = Host.CreateDefaultBuilder()
            .UseSerilog()
            .ConfigureServices((context, services) =>
            {
                // register services
                services.AddTransient<FrmBundler>();
            })
            .Build();

            // prevent catching the exceptions when debuggin
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                // Add the event handler for handling UI thread exceptions to the event.
                Application.ThreadException += (sender, args) =>
                {
                    Log.Error(args.Exception, "Excepción de subproceso no controlada.");
                    MessageBox.Show("Ocurrió un error inesperado.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                };

                // Set the unhandled exception mode to force all Windows Forms errors
                // to go through our handler.
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                // Add the event handler for handling non-UI thread exceptions to the event. 
                AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    Log.Fatal(args.ExceptionObject as Exception, "Excepción no controlada.");
                };
            }

            // To customize application configuration such as set high DPI settings or default
            // font, see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var startupForm = host.Services.GetRequiredService<FrmBundler>();
            Application.Run(startupForm);

        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "La aplicación terminó inesperadamente");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    static void BuildConfig(IConfigurationBuilder builder)
    {
        var aspNetCoreEnvironment = Environment.GetEnvironmentVariable(
            "ASPNETCORE_ENVIRONMENT") ?? "Production";

        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{aspNetCoreEnvironment}.json", optional: true)
            .AddEnvironmentVariables();
    }
}