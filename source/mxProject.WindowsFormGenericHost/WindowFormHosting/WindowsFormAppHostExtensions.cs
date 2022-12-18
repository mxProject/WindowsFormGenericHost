#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using mxProject.WindowFormHosting.Internals;

namespace mxProject.WindowFormHosting
{

    /// <summary>
    /// Extension methods for hosting Windows Forms applications.
    /// </summary>
    public static class WindowsFormAppHostExtensions
    {

        #region IHostBuilder.AddWindowsFormApp

        /// <summary>
        /// Registers the Windows Forms application that starts with the specified main form.
        /// </summary>
        /// <typeparam name="TForm">The type of the main form.</typeparam>
        /// <param name="builder"></param>
        /// <param name="onStart">The method to perform when the application starts.</param>
        /// <param name="onExit">The method to perform when the application exits.</param>
        /// <param name="configure">The method to configure options.</param>
        /// <returns></returns>
        public static IHostBuilder AddWindowsFormApp<TForm>(
            this IHostBuilder builder,
            Action<IWindowsFormAppContext>? onStart = null,
            Action<IWindowsFormAppContext>? onExit = null,
            Action<WindowsFormAppHostOptions>? configure = null
            )
            where TForm : Form
        {
            return AddWindowsFormAppMain(builder, CreateWindowsFormAppInfo<TForm>(onStart, onExit), configure, false);
        }

        /// <summary>
        /// Registers the Windows Forms application that starts with the specified main form.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TForm">The type of the main form.</typeparam>
        /// <param name="builder"></param>
        /// <param name="onStart">The method to perform when the application starts.</param>
        /// <param name="onExit">The method to perform when the application exits.</param>
        /// <param name="configure">The method to configure options.</param>
        /// <returns></returns>
        public static IHostBuilder AddWindowsFormApp<TContext, TForm>(
            this IHostBuilder builder,
            Action<TContext>? onStart = null,
            Action<TContext>? onExit = null,
            Action<WindowsFormAppHostOptions>? configure = null
            )
            where TContext : class, IWindowsFormAppContext
            where TForm : Form
        {
            return AddWindowsFormAppMain(builder, CreateWindowsFormAppInfo<TContext, TForm>(onStart, onExit), configure, true);
        }

        /// <summary>
        /// Registers the specified Windows Forms application.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="appInfo">The application information.</param>
        /// <param name="configure">The method to configure options.</param>
        /// <returns></returns>
        public static IHostBuilder AddWindowsFormApp(
            this IHostBuilder builder,
            IWindowsFormAppInfo appInfo,
            Action<WindowsFormAppHostOptions>? configure = null
            )
        {
            return AddWindowsFormAppMain(builder, CreateWindowsFormAppInfo(appInfo), configure, false);
        }

        /// <summary>
        /// Registers the specified Windows Forms application.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="builder"></param>
        /// <param name="appInfo">The application information.</param>
        /// <param name="configure">The method to configure options.</param>
        /// <returns></returns>
        public static IHostBuilder AddWindowsFormApp<TContext>(
            this IHostBuilder builder,
            IWindowsFormAppInfo<TContext> appInfo,
            Action<WindowsFormAppHostOptions>? configure = null
            )
            where TContext : class, IWindowsFormAppContext
        {
            return AddWindowsFormAppMain(builder, appInfo, configure, true);
        }

        /// <summary>
        /// Registers the specified Windows Forms application.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="builder"></param>
        /// <param name="appInfo">The application information.</param>
        /// <param name="configure">The method to configure options.</param>
        /// <param name="useTypedContext">A value indicating whether to inject the context with a concrete type.</param>
        /// <returns></returns>
        private static IHostBuilder AddWindowsFormAppMain<TContext>(
            IHostBuilder builder,
            IWindowsFormAppInfo<TContext> appInfo,
            Action<WindowsFormAppHostOptions>? configure,
            bool useTypedContext
            )
            where TContext : class, IWindowsFormAppContext
        {
            builder.ConfigureServices((hostContext, services) =>
            {
                var options = new WindowsFormAppHostOptions();

                // hostContext.Configuration.Bind("WindowsFormAppHost", options);

                configure?.Invoke(options);

                AddWindowsFormAppMain(
                    services,
                    appInfo,
                    options,
                    useTypedContext);
            });
            
            return builder;
        }

        #endregion

        #region IServiceCollection.AddWindowsFormApp

        /// <summary>
        /// Registers the Windows Forms application that starts with the specified main form.
        /// </summary>
        /// <typeparam name="TForm">The type of the main form.</typeparam>
        /// <param name="services"></param>
        /// <param name="onStart">The method to perform when the application starts.</param>
        /// <param name="onExit">The method to perform when the application exits.</param>
        /// <param name="options">The option.</param>
        /// <returns></returns>
        public static IServiceCollection AddWindowsFormApp<TForm>(
            this IServiceCollection services,
            Action<IWindowsFormAppContext>? onStart = null,
            Action<IWindowsFormAppContext>? onExit = null,
            WindowsFormAppHostOptions? options = null
            )
            where TForm : Form
        {
            return AddWindowsFormAppMain(services, CreateWindowsFormAppInfo<TForm>(onStart, onExit), options, false);
        }

        /// <summary>
        /// Registers the Windows Forms application that starts with the specified main form.
        /// </summary>
        /// <typeparam name="TForm">The type of the main form.</typeparam>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="services"></param>
        /// <param name="onStart">The method to perform when the application starts.</param>
        /// <param name="onExit">The method to perform when the application exits.</param>
        /// <param name="options">The option.</param>
        /// <returns></returns>
        public static IServiceCollection AddWindowsFormApp<TContext, TForm>(
            this IServiceCollection services,
            Action<TContext>? onStart = null,
            Action<TContext>? onExit = null,
            WindowsFormAppHostOptions? options = null
            )
            where TContext : class, IWindowsFormAppContext
            where TForm : Form
        {
            return AddWindowsFormAppMain(services, CreateWindowsFormAppInfo<TContext, TForm>(onStart, onExit), options, true);
        }

        /// <summary>
        /// Registers the specified Windows Forms application.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="services"></param>
        /// <param name="appInfo">The application information.</param>
        /// <param name="options">The option.</param>
        /// <returns></returns>
        public static IServiceCollection AddWindowsFormApp<TContext>(
            this IServiceCollection services,
            IWindowsFormAppInfo<TContext> appInfo,
            WindowsFormAppHostOptions? options = null
            )
            where TContext : class, IWindowsFormAppContext
        {
            return AddWindowsFormAppMain(services, appInfo, options, true);
        }

        /// <summary>
        /// Registers the specified Windows Forms application.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="services"></param>
        /// <param name="appInfo">The application information.</param>
        /// <param name="options">The option.</param>
        /// <param name="useTypedContext">A value indicating whether to inject the context with a concrete type.</param>
        /// <returns></returns>
        private static IServiceCollection AddWindowsFormAppMain<TContext>(
            IServiceCollection services,
            IWindowsFormAppInfo<TContext> appInfo,
            WindowsFormAppHostOptions? options,
            bool useTypedContext
            )
            where TContext : class, IWindowsFormAppContext
        {
            // hosted service
            services.AddHostedService<WindowsFormAppHostedService<TContext>>();

            // form provider
            services.AddSingleton(typeof(IWindowsFormProvider), options?.CustomWindowsFormProviderType ?? typeof(WindowsFormProvider));

            // application information
            services.AddSingleton(typeof(IWindowsFormAppInfo<TContext>), appInfo);

            // startup object
            services.AddTransient(appInfo.StartupObjectType);

            // context
            if (useTypedContext)
            {
                services.AddSingleton(typeof(TContext));
            }
            else
            {
                services.AddSingleton(typeof(IWindowsFormAppContext), typeof(TContext));
            }

            return services;
        }

        /// <summary>
        /// Create a application information.
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <typeparam name="TForm"></typeparam>
        /// <param name="onStart"></param>
        /// <param name="onExit"></param>
        /// <returns></returns>
        private static IWindowsFormAppInfo<TContext> CreateWindowsFormAppInfo<TContext, TForm>(Action<TContext>? onStart, Action<TContext>? onExit)
            where TForm : Form
            where TContext : IWindowsFormAppContext
        {
            return new WindowsFormAppInfo<TContext, TForm>(onStart, onExit);
        }

        /// <summary>
        /// Create a application information.
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <param name="onStart"></param>
        /// <param name="onExit"></param>
        /// <returns></returns>
        private static IWindowsFormAppInfo<WindowsFormAppContext> CreateWindowsFormAppInfo<TForm>(Action<IWindowsFormAppContext>? onStart, Action<IWindowsFormAppContext>? onExit)
            where TForm : Form
        {
            return new WindowsFormAppInfo<WindowsFormAppContext, TForm>(onStart, onExit);
        }

        /// <summary>
        /// Create a application information.
        /// </summary>
        /// <param name="appInfo"></param>
        /// <returns></returns>
        private static IWindowsFormAppInfo<WindowsFormAppContext> CreateWindowsFormAppInfo(IWindowsFormAppInfo appInfo)
        {
            return new WindowsFormAppInfo(appInfo);
        }

        #endregion


        #region IHostBuilder.AddTaskTrayApp

        /// <summary>
        /// Registers a tasktray-resident application to start with the specified NotifyIcon.
        /// </summary>
        /// <typeparam name="TNotifyIcon">The type of the NotifyIcon provider.</typeparam>
        /// <param name="builder"></param>
        /// <param name="onStart">The method to perform when the application starts.</param>
        /// <param name="onExit">The method to perform when the application exits.</param>
        /// <param name="configure">The method to configure options.</param>
        /// <returns></returns>
        public static IHostBuilder AddTaskTrayApp<TNotifyIcon>(
            this IHostBuilder builder,
            Action<IWindowsFormAppContext>? onStart = null,
            Action<IWindowsFormAppContext>? onExit = null,
            Action<WindowsFormAppHostOptions>? configure = null
            )
            where TNotifyIcon : INotifyIconProvider
        {
            return AddWindowsFormAppMain(builder, CreateTaskTrayAppInfo<TNotifyIcon>(onStart, onExit), configure, false);
        }

        /// <summary>
        /// Registers a tasktray-resident application to start with the specified NotifyIcon.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TNotifyIcon">The type of the NotifyIcon provider.</typeparam>
        /// <param name="builder"></param>
        /// <param name="onStart">The method to perform when the application starts.</param>
        /// <param name="onExit">The method to perform when the application exits.</param>
        /// <param name="configure">The method to configure options.</param>
        /// <returns></returns>
        public static IHostBuilder AddTaskTrayApp<TContext, TNotifyIcon>(
            this IHostBuilder builder,
            Action<TContext>? onStart = null,
            Action<TContext>? onExit = null,
            Action<WindowsFormAppHostOptions>? configure = null
            )
            where TContext : class, IWindowsFormAppContext
            where TNotifyIcon : INotifyIconProvider
        {
            return AddWindowsFormAppMain(builder, CreateTaskTrayAppInfo<TContext, TNotifyIcon>(onStart, onExit), configure, true);
        }

        #endregion

        #region IServiceCollection.AddTaskTrayApp

        /// <summary>
        /// Registers a tasktray-resident application to start with the specified NotifyIcon.
        /// </summary>
        /// <typeparam name="TNotifyIcon">The type of the NotifyIcon provider.</typeparam>
        /// <param name="services"></param>
        /// <param name="onStart">The method to perform when the application starts.</param>
        /// <param name="onExit">The method to perform when the application exits.</param>
        /// <param name="options">The option.</param>
        /// <returns></returns>
        public static IServiceCollection AddTaskTrayApp<TNotifyIcon>(
            this IServiceCollection services,
            Action<IWindowsFormAppContext>? onStart = null,
            Action<IWindowsFormAppContext>? onExit = null,
            WindowsFormAppHostOptions? options = null
            )
            where TNotifyIcon : INotifyIconProvider
        {
            return AddWindowsFormAppMain(services, CreateTaskTrayAppInfo<TNotifyIcon>(onStart, onExit), options, false);
        }

        /// <summary>
        /// Registers a tasktray-resident application to start with the specified NotifyIcon.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TNotifyIcon">The type of the NotifyIcon provider.</typeparam>
        /// <param name="services"></param>
        /// <param name="onStart">The method to perform when the application starts.</param>
        /// <param name="onExit">The method to perform when the application exits.</param>
        /// <param name="options">The option.</param>
        /// <returns></returns>
        public static IServiceCollection AddTaskTrayApp<TContext, TNotifyIcon>(
            this IServiceCollection services,
            Action<TContext>? onStart = null,
            Action<TContext>? onExit = null,
            WindowsFormAppHostOptions? options = null
            )
            where TContext : class, IWindowsFormAppContext
            where TNotifyIcon : INotifyIconProvider
        {
            return AddWindowsFormAppMain(services, CreateTaskTrayAppInfo<TContext, TNotifyIcon>(onStart, onExit), options, true);
        }

        /// <summary>
        /// Create a application information.
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <typeparam name="TNotifyIcon"></typeparam>
        /// <param name="onStart"></param>
        /// <param name="onExit"></param>
        /// <returns></returns>
        private static IWindowsFormAppInfo<TContext> CreateTaskTrayAppInfo<TContext, TNotifyIcon>(Action<TContext>? onStart, Action<TContext>? onExit)
            where TNotifyIcon : INotifyIconProvider
            where TContext : IWindowsFormAppContext
        {
            return new TaskTrayAppInfo<TContext, TNotifyIcon>(onStart, onExit);
        }

        /// <summary>
        /// Create a application information.
        /// </summary>
        /// <typeparam name="TNotifyIcon"></typeparam>
        /// <param name="onStart"></param>
        /// <param name="onExit"></param>
        /// <returns></returns>
        private static IWindowsFormAppInfo<WindowsFormAppContext> CreateTaskTrayAppInfo<TNotifyIcon>(Action<IWindowsFormAppContext>? onStart, Action<IWindowsFormAppContext>? onExit)
            where TNotifyIcon : INotifyIconProvider
        {
            return new TaskTrayAppInfo<WindowsFormAppContext, TNotifyIcon>(onStart, onExit);
        }

        #endregion

    }

}
