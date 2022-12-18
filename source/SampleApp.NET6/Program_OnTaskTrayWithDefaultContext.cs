﻿#nullable enable

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
        /// Run as a task tray resident application.
        /// </summary>
        static void RunOnTaskTrayWithDefaultContext(string[] args)
        {
            var builder = MyWindowsAppHostBuilder.CreateTaskTrayAppWithDefaultContext<MyNotifyIconProvider>(args, (hostContext, services) =>
            {
                services.AddTransient<Form1>();
                services.AddTransient<Form2>();
            });

            builder.Build().Run();
        }
    }

    partial class MyWindowsAppHostBuilder
    {
        /// <summary>
        /// Creates a host builder that runs as a task tray resident application.
        /// </summary>
        /// <returns></returns>
        public static IHostBuilder CreateTaskTrayAppWithDefaultContext<TNotifyIcon>(string[] args, Action<HostBuilderContext, IServiceCollection>? configureDelegate = null)
            where TNotifyIcon : INotifyIconProvider
        {
            static void OnStart(IWindowsFormAppContext context)
            {
                context.LoggerFactory.CreateLogger(typeof(MyWindowsAppHostBuilder)).LogDebug("OnStart");
            }

            static void OnExit(IWindowsFormAppContext context)
            {
                context.LoggerFactory.CreateLogger(typeof(MyWindowsAppHostBuilder)).LogDebug("OnExit");
            }

            return CreateDefaultHostBuilder(args, configureDelegate)
                .AddTaskTrayApp<TNotifyIcon>(OnStart, OnExit, ConfigureHostOptions);
        }
    }

}
