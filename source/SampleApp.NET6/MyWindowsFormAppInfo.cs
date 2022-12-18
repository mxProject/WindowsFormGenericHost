using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using mxProject.WindowFormHosting;

namespace WindowsFormGenericHostSample;

/// <summary>
/// This application information.
/// </summary>
internal class MyWindowsFormAppInfoWithDefaultContext : IWindowsFormAppInfo
{
    internal MyWindowsFormAppInfoWithDefaultContext()
    {
    }

    /// <inheritdoc/>
    public Type StartupObjectType => typeof(Form1);

    /// <inheritdoc/>
    public void OnStart(IWindowsFormAppContext context)
    {
        context.LoggerFactory.CreateLogger<MyWindowsFormAppInfoWithDefaultContext>().LogDebug("OnStart");

        static Form ActivateSplashWindow()
        {
            var splashForm = new Form
            {
                StartPosition = FormStartPosition.CenterScreen,
                ControlBox = false,
                FormBorderStyle = FormBorderStyle.None
            };

            return splashForm;
        }

        WindowsFormAppUtility.ExecuteWithSplashWindow(() => System.Threading.Thread.Sleep(3000), ActivateSplashWindow);
    }

    /// <inheritdoc/>
    public void OnExit(IWindowsFormAppContext context)
    {
        context.LoggerFactory.CreateLogger<MyAppContext>().LogInformation("OnExit");
    }
}

/// <summary>
/// This application information.
/// </summary>
internal class MyWindowsFormAppInfo : IWindowsFormAppInfo<MyAppContext>
{
    internal MyWindowsFormAppInfo()
    {
    }

    /// <inheritdoc/>
    public Type StartupObjectType => typeof(Form1);

    /// <inheritdoc/>
    public void OnStart(MyAppContext context)
    {
        context.LoggerFactory.CreateLogger<MyAppContext>().LogInformation("OnStart");

        static Form ActivateSplashWindow()
        {
            var splashForm = new Form
            {
                StartPosition = FormStartPosition.CenterScreen,
                ControlBox = false,
                FormBorderStyle = FormBorderStyle.None
            };

            return splashForm;
        }

        WindowsFormAppUtility.ExecuteWithSplashWindow(() => context.InitializeApplicationAsync().Wait(), ActivateSplashWindow);
    }

    /// <inheritdoc/>
    public void OnExit(MyAppContext context)
    {
        context.LoggerFactory.CreateLogger<MyAppContext>().LogInformation("OnExit");
    }
}

