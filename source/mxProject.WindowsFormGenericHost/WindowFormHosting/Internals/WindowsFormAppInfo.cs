#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mxProject.WindowFormHosting.Internals
{

    /// <summary>
    ///
    /// </summary>
    internal sealed class WindowsFormAppInfo : WindowsFormAppInfoBase<WindowsFormAppContext>
    {

        /// <summary>
        /// 
        /// </summary>
        public WindowsFormAppInfo(IWindowsFormAppInfo appInfo) : base(appInfo.OnStart, appInfo.OnExit)
        {
            m_AppInfo = appInfo;
        }

        private readonly IWindowsFormAppInfo m_AppInfo;

        /// <inheritdoc/>
        public override Type StartupObjectType => m_AppInfo.StartupObjectType;

    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TForm"></typeparam>
    internal sealed class WindowsFormAppInfo<TContext, TForm> : WindowsFormAppInfoBase<TContext>
        where TContext : IWindowsFormAppContext
        where TForm : Form
    {

        /// <summary>
        /// 
        /// </summary>
        public WindowsFormAppInfo() : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onStart"></param>
        /// <param name="onExit"></param>
        public WindowsFormAppInfo(Action<TContext>? onStart = null, Action<TContext>? onExit = null) : base(onStart, onExit)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onStart"></param>
        /// <param name="onExit"></param>
        public WindowsFormAppInfo(Action<IWindowsFormAppContext>? onStart = null, Action<IWindowsFormAppContext>? onExit = null) : base(onStart, onExit)
        {
        }

        /// <inheritdoc/>
        public override Type StartupObjectType => typeof(TForm);

    }

}
