#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using mxProject.WindowFormHosting;

namespace WindowsFormGenericHostSample;

/// <summary>
/// HostBuilder for this application.
/// </summary>
internal static partial class MyWindowsAppHostBuilder
{

    public static IHostBuilder CreateDefaultHostBuilder(string[] args, Action<HostBuilderContext, IServiceCollection>? configureDelegate = null)
    {
        return Host.CreateDefaultBuilder(args)

            .ConfigureServices((hostContext, services) =>
            {
                ConfigureMyOptions(hostContext, services);

                configureDelegate?.Invoke(hostContext, services);
            })
            .ConfigureLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddDebug();
            });
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="options"></param>
    static void ConfigureHostOptions(WindowsFormAppHostOptions options)
    {
        System.Diagnostics.Debug.WriteLine("ConfigureHostOptions");

        // options.CustomWindowsFormProviderType = typeof(MyWindowsFormProvider);
    }

    static void ConfigureMyOptions(HostBuilderContext hostContext, IServiceCollection services)
    {
        // Singleton
        var options = new MyAppOptions();

        hostContext.Configuration.GetSection("MyApp").Bind(options, binder =>
        {
            binder.BindNonPublicProperties = true;
        });

        services.AddSingleton(options);

        // IOption, IOptionMonitor, IOptionSnapshot
        services.Configure<MyAppOptions>(hostContext.Configuration.GetSection("MyApp"), binder =>
        {
            binder.BindNonPublicProperties = true;
        });

    }

}
