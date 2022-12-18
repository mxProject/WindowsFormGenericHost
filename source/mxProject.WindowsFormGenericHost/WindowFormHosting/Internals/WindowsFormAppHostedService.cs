using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace mxProject.WindowFormHosting.Internals
{

    /// <summary>
    ///
    /// </summary>
    internal sealed class WindowsFormAppHostedService<TContext> : BackgroundService
        where TContext : IWindowsFormAppContext
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="applicationLifetime"></param>
        /// <param name="logger"></param>
        public WindowsFormAppHostedService(IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime, ILogger<WindowsFormAppHostedService<TContext>> logger)
        {
            m_ApplicationLifetime = applicationLifetime;
            m_ServiceProvider = serviceProvider;
            m_Logger = logger;
        }

        private readonly IHostApplicationLifetime m_ApplicationLifetime;
        private readonly IServiceProvider m_ServiceProvider;
        private readonly ILogger<WindowsFormAppHostedService<TContext>> m_Logger;

        ///// <inheritdoc/>
        //public override Task StartAsync(CancellationToken cancellationToken)
        //{
        //    return base.StartAsync(cancellationToken).ContinueWith(_ =>
        //    {
        //    },
        //    cancellationToken);
        //}

        ///// <inheritdoc/>
        //public override Task StopAsync(CancellationToken cancellationToken)
        //{
        //    return base.StopAsync(cancellationToken).ContinueWith(_ =>
        //    {
        //    },
        //    cancellationToken);
        //}

        /// <inheritdoc/>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            m_ApplicationLifetime.ApplicationStarted.Register(() =>
            {
                m_Logger.LogInformation("This application host has fully started.");
                RunWindowsFormApp();
                m_ApplicationLifetime.StopApplication();
            });

            m_ApplicationLifetime.ApplicationStopping.Register(() =>
            {
                m_Logger.LogInformation("This application host is starting a graceful shutdown.");
            });

            m_ApplicationLifetime.ApplicationStopped.Register(() =>
            {
                m_Logger.LogInformation("This application host has completed a graceful shutdown.");
            });

            return Task.CompletedTask;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private void RunWindowsFormApp()
        {
            using var scope = m_ServiceProvider.CreateScope();

            IWindowsFormAppInfo<TContext> app = null!;

            try
            {
                app = scope.ServiceProvider.GetRequiredService<IWindowsFormAppInfo<TContext>>();

                if (app == null || app.StartupObjectType == null)
                {
                    throw new NullReferenceException("The type of the application startup object is null.");
                }

#pragma warning disable CA2254
                m_Logger.LogInformation($"Start application. StartupObjectType = {app.StartupObjectType.FullName}");
#pragma warning restore CA2254

                using var context = scope.ServiceProvider.GetService<TContext>() ?? (TContext)scope.ServiceProvider.GetRequiredService<IWindowsFormAppContext>();

                app.OnStart(context);

                if (app.IsWindowsFormApp())
                {
                    using var form = scope.ServiceProvider.GetRequiredService<IWindowsFormProvider>().CreateForm(app.StartupObjectType);

                    using var appContext = new ApplicationContext(form);

                    Application.Run(appContext);
                }
                else if (app.IsTaskTrayApp())
                {
                    using var icon = ((INotifyIconProvider)scope.ServiceProvider.GetRequiredService(app.StartupObjectType)).CreateNotifyIcon();

                    Application.Run();
                }
                else
                {
                    throw new NotSupportedException($"Unable to start application. StartupObjectType = {app.StartupObjectType}");
                }

                app.OnExit(context);
            }
            catch (Exception ex)
            {
#pragma warning disable CA2254
                m_Logger.LogError(ex, ex.Message);
#pragma warning restore CA2254
            }
            finally
            {
#pragma warning disable CA2254
                m_Logger.LogInformation($"Exit application. StartupObjectType = {app?.StartupObjectType?.FullName}");
#pragma warning restore CA2254
            }
        }

    }

}
