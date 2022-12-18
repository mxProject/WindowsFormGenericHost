#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using mxProject.WindowFormHosting;

namespace WindowsFormGenericHostSample;

/// <summary>
/// Context of this application.
/// </summary>
internal class MyAppContext : WindowsFormAppContextBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="formProvider"></param>
    /// <param name="loggerFactory"></param>
    /// <param name="appOptions"></param>
    /// <param name="appOptionsMonitor"></param>
    /// <param name="messagePipeProvider"></param>
    public MyAppContext(
        IWindowsFormProvider formProvider,
        ILoggerFactory loggerFactory,
        MyAppOptions appOptions,
        IOptionsMonitor<MyAppOptions> appOptionsMonitor
        )
        : base(formProvider, loggerFactory)
    {
        AppOptions = appOptions;

        m_CurrentAppOptions = appOptionsMonitor.CurrentValue;
        appOptionsMonitor.OnChange(value => { m_CurrentAppOptions = value; });

        // Context is a singleton.
        Current = this;
    }

    /// <summary>
    /// Gets the current instance.
    /// </summary>
    internal static MyAppContext? Current { get; private set; } 

    /// <summary>
    /// Gets the options.
    /// </summary>
    public MyAppOptions AppOptions { get; }

    /// <summary>
    /// Gets the options.
    /// </summary>
    public MyAppOptions CurrentAppOptions
    {
        get { return m_CurrentAppOptions; }
    }
    private MyAppOptions m_CurrentAppOptions;

    internal Task InitializeApplicationAsync()
    {
        // Represents initial processing that takes a long time.
        return Task.Delay(AppOptions.StartupDelay);
    }

}

