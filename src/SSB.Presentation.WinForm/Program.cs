using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace SSB.Presentation.WinForm;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default
        // font, see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        var builder = new ConfigurationBuilder();
        BuildConfig(builder);

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {

            }).Build();

        Application.Run(new Form1());
    }

    static void BuildConfig(IConfigurationBuilder builder)
    {
        var aspNetCoreEnvironment = Environment.GetEnvironmentVariable(
            "ASPNETCORE_ENVIRONMENT") ?? "Production";

        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{aspNetCoreEnvironment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }
}