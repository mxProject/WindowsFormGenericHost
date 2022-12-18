using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormGenericHostSample
{

    /// <summary>
    /// Splash window form.
    /// </summary>
    internal class MySplashWindow
    {

        /// <summary>
        /// Creates a new form.
        /// </summary>
        /// <returns></returns>
        internal static Form Create()
        {
            var splashForm = new Form
            {
                StartPosition = FormStartPosition.CenterScreen,
                ControlBox = false,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.Orange,
            };

            return splashForm;
        }

    }

}
