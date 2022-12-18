#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mxProject.WindowFormHosting.Internals
{

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TNotifyIcon"></typeparam>
    internal class TaskTrayAppInfo<TContext, TNotifyIcon> : WindowsFormAppInfoBase<TContext>
        where TContext : IWindowsFormAppContext
        where TNotifyIcon : INotifyIconProvider
    {

        /// <summary>
        ///
        /// </summary>
        public TaskTrayAppInfo() : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onStart"></param>
        /// <param name="onExit"></param>
        public TaskTrayAppInfo(Action<TContext>? onStart = null, Action<TContext>? onExit = null) : base(onStart, onExit)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onStart"></param>
        /// <param name="onExit"></param>
        public TaskTrayAppInfo(Action<IWindowsFormAppContext>? onStart = null, Action<IWindowsFormAppContext>? onExit = null) : base(onStart, onExit)
        {
        }

        /// <inheritdoc/>
        public override Type StartupObjectType => typeof(TNotifyIcon);

    }

}
