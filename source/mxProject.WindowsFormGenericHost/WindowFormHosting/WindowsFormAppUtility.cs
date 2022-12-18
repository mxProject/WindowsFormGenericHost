using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mxProject.WindowFormHosting
{

    /// <summary>
    /// Utility methods for Windows Forms applications.
    /// </summary>
    public static class WindowsFormAppUtility
    {

        /// <summary>
        /// Shows the splash window and executes the specified action.
        /// </summary>
        /// <param name="heavyAction">The action.</param>
        /// <param name="splashWindowActivator">The method to create the splash window form.</param>
        /// <returns></returns>
        public static void ExecuteWithSplashWindow(Action heavyAction, Func<Form> splashWindowActivator)
        {
            Task heavyTask = Task.Run(() =>
            {
                heavyAction();
            });

            if (heavyTask.IsCompleted) { return; }

            using var splashForm = splashWindowActivator();

            heavyTask.ContinueWith(t =>
            {
                if (splashForm.InvokeRequired)
                {
                    splashForm.Invoke(new Action(() => splashForm.Dispose()));
                }
                else
                {
                    splashForm.Dispose();
                }
            });

            if (heavyTask.IsCompleted) { return; }

            Application.Run(splashForm);
        }

    }

}
