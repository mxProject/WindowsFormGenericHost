using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mxProject.WindowFormHosting
{

    /// <summary>
    /// Extension methods for <see cref="IWindowsFormAppInfo{TContext}"/>.
    /// </summary>
    public static class IWindowsFormAppInfoExtensions
    {
        /// <summary>
        /// Gets a value indicating whether this application is a Windows Forms application.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public static bool IsWindowsFormApp<TContext>(this IWindowsFormAppInfo<TContext> app)
             where TContext : IWindowsFormAppContext
        {
            return typeof(Form).IsAssignableFrom(app.StartupObjectType);
        }

        /// <summary>
        /// Gets a value indicating whether this application is a tasktray-resident application.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public static bool IsTaskTrayApp<TContext>(this IWindowsFormAppInfo<TContext> app)
             where TContext : IWindowsFormAppContext
        {
            return typeof(INotifyIconProvider).IsAssignableFrom(app.StartupObjectType);
        }
    }

}
