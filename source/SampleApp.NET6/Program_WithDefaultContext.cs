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
        static void RunWithDefaultContext(string[] args)
        {
            static void OnStart(IWindowsFormAppContext context)
            {
                // Represents heavy initialization.
                Task.Delay(3000).Wait();
            }

            var builder = MyWindowsAppHostBuilder.CreateDefaultWithDefaultContext<Form1>(args, onStart: OnStart, configureDelegate: (hostContext, services) =>
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
        public static IHostBuilder CreateDefaultWithDefaultContext<TForm>(
            string[] args,
            Action<IWindowsFormAppContext>? onStart = null,
            Action<IWindowsFormAppContext>? onExit = null,
            Action<HostBuilderContext, IServiceCollection>? configureDelegate = null
            )
            where TForm : Form
        {
            void OnStart(IWindowsFormAppContext context)
            {
                context.LoggerFactory.CreateLogger(typeof(MyWindowsAppHostBuilder)).LogDebug("OnStart");
                WindowsFormAppUtility.ExecuteWithSplashWindow(() => onStart?.Invoke(context), MySplashWindow.Create);
            }

            void OnExit(IWindowsFormAppContext context)
            {
                context.LoggerFactory.CreateLogger(typeof(MyWindowsAppHostBuilder)).LogDebug("OnExit");
                onExit?.Invoke(context);
            }

            return CreateDefaultHostBuilder(args, configureDelegate)
                .AddWindowsFormApp<TForm>(OnStart, OnExit, ConfigureHostOptions);
        }
    }

}
