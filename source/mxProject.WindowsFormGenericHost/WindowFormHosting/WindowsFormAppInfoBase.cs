#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mxProject.WindowFormHosting
{

    /// <summary>
    /// Basic implementation of <see cref="IWindowsFormAppInfo{TContext}"/>.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public abstract class WindowsFormAppInfoBase<TContext> : IWindowsFormAppInfo<TContext>
        where TContext : IWindowsFormAppContext
    {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        protected WindowsFormAppInfoBase()
        {
            m_OnStart = null;
            m_OnExit = null;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="onStart">The method to perform when the application starts.</param>
        /// <param name="onExit">The method to perform when the application exits.</param>
        protected WindowsFormAppInfoBase(Action<TContext>? onStart = null, Action<TContext>? onExit = null)
        {
            m_OnStart = onStart == null ? null : x => onStart((TContext)x);
            m_OnExit = onExit == null ? null : x => onExit((TContext)x);
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="onStart">The method to perform when the application starts.</param>
        /// <param name="onExit">The method to perform when the application exits.</param>
        protected WindowsFormAppInfoBase(Action<IWindowsFormAppContext>? onStart = null, Action<IWindowsFormAppContext>? onExit = null)
        {
            m_OnStart = onStart;
            m_OnExit = onExit;
        }

        private readonly Action<IWindowsFormAppContext>? m_OnStart;
        private readonly Action<IWindowsFormAppContext>? m_OnExit;

        /// <inheritdoc/>
        public abstract Type StartupObjectType { get; }

        /// <inheritdoc/>
        public Type ContextType => typeof(TContext);

        /// <inheritdoc/>
        public virtual void OnStart(TContext context)
        {
            m_OnStart?.Invoke(context);
        }

        /// <inheritdoc/>
        public virtual void OnExit(TContext context)
        {
            m_OnExit?.Invoke(context);
        }
    }

}
