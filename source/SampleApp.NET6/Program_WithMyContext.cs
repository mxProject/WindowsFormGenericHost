#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using mxProject.WindowFormHosting;

namespace WindowsFormGenericHostSample
{

    partial class Program
    {
        /// <summary>
        /// Run this application specifying the main form type.
        /// </summary>
        static void RunWithMyContext(string[] args)
        {
            var builder = MyWindowsAppHostBuilder.CreateDefault<Form1>(args, configureDelegate: (hostContext, services) =>
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
        public static IHostBuilder CreateDefault<TForm>(
            string[] args,
            Action<HostBuilderContext, IServiceCollection>? configureDelegate = null
            )
            where TForm : Form
        {
            void OnStart(MyAppContext context)
            {
                context.LoggerFactory.CreateLogger(typeof(MyWindowsAppHostBuilder)).LogDebug("OnStart");
                WindowsFormAppUtility.ExecuteWithSplashWindow(() => context.InitializeApplicationAsync().Wait(), MySplashWindow.Create);
            }

            static void OnExit(MyAppContext context)
            {
                context.LoggerFactory.CreateLogger(typeof(MyWindowsAppHostBuilder)).LogDebug("OnExit");
            }

            return CreateDefaultHostBuilder(args, configureDelegate)
                .AddWindowsFormApp<MyAppContext, TForm>(OnStart, OnExit, ConfigureHostOptions);
        }
    }

}
