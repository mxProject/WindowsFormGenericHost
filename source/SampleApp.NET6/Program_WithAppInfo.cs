#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using mxProject.WindowFormHosting;

namespace WindowsFormGenericHostSample
{

    partial class Program
    {
        /// <summary>
        /// Runs this application with <see cref="IWindowsFormAppInfo{TContext}"/>.
        /// </summary>
        static void RunWithAppInfo(string[] args)
        {
            var appInfo = new MyWindowsFormAppInfo();

            var builder = MyWindowsAppHostBuilder.CreateDefault(appInfo, args, (hostContext, services) =>
            {
                // Register the types to be injected in addition to the main form.
                services.AddTransient<Form2>();
            });

            builder.Build().Run();
        }
    }

    partial class MyWindowsAppHostBuilder
    {
        /// <summary>
        /// Creates a host builder that runs as a windows form application.
        /// </summary>
        /// <returns></returns>
        public static IHostBuilder CreateDefault(
            IWindowsFormAppInfo<MyAppContext> app,
            string[] args,
            Action<HostBuilderContext, IServiceCollection>? configureDelegate = null
            )
        {
            return CreateDefaultHostBuilder(args, configureDelegate)
                .AddWindowsFormApp(app, ConfigureHostOptions);
        }
    }

}
