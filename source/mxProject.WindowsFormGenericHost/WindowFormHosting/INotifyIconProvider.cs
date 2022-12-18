using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mxProject.WindowFormHosting
{

    /// <summary>
    /// Provides the functionality needed to create an instance of <see cref="NotifyIcon"/>.
    /// </summary>
    public interface INotifyIconProvider
    {
        /// <summary>
        /// Creates a new instance of <see cref="NotifyIcon"/>.
        /// </summary>
        /// <returns></returns>
        NotifyIcon CreateNotifyIcon();
    }

}
