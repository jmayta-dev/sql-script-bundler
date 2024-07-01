using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SSB.Application;
using SSB.Services;
using System.Text;

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
                services.AddApplicationLayer();
                services.AddServiceLayer();
            })
            .Build();

            // prevent catching the exceptions when debuggin
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                // Add the event handler for handling UI thread exceptions to the event.
                System.Windows.Forms.Application.ThreadException += OnThreadException;

                System.Windows.Forms.Application.ApplicationExit += OnApplicationExit;

                // Set the unhandled exception mode to force all Windows Forms errors
                // to go through our handler.
                System.Windows.Forms.Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

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
            System.Windows.Forms.Application.Run(startupForm);

        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "La aplicación terminó inesperadamente");
        }
        finally
        {
            System.Windows.Forms.Application.Exit();
        }
    }

    private static void OnThreadException(object sender, ThreadExceptionEventArgs args)
    {
        Log.Error(args.Exception, "Excepción de subproceso no controlada.");
        MessageBox.Show("Ocurrió un error inesperado.", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private static void OnApplicationExit(object? sender, EventArgs args)
    {
        // Detach ThreadException event handler to avoid memory leaks.
        System.Windows.Forms.Application.ThreadException -= OnThreadException;
        Log.CloseAndFlush();
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