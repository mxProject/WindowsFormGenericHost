using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mxProject.WindowFormHosting
{

    /// <summary>
    /// Provides information about a Windows Forms applications.
    /// </summary>
    public interface IWindowsFormAppInfo
    {
        /// <summary>
        /// Gets the type of the application startup object.
        /// </summary>
        Type StartupObjectType { get; }

        /// <summary>
        /// Performs when the application starts.
        /// </summary>
        /// <param name="context">The context.</param>
        void OnStart(IWindowsFormAppContext context);

        /// <summary>
        /// Performs when the application exits.
        /// </summary>
        /// <param name="context">The context.</param>
        void OnExit(IWindowsFormAppContext context);
    }

    /// <summary>
    /// Provides information about a Windows Forms applications.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public interface IWindowsFormAppInfo<TContext>
        where TContext : IWindowsFormAppContext
    {
        /// <summary>
        /// Gets the type of the application startup object.
        /// </summary>
        Type StartupObjectType { get; }

        /// <summary>
        /// Performs when the application starts.
        /// </summary>
        /// <param name="context">The context.</param>
        void OnStart(TContext context);

        /// <summary>
        /// Performs when the application exits.
        /// </summary>
        /// <param name="context">The context.</param>
        void OnExit(TContext context);
    }

}
