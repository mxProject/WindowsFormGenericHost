using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace mxProject.WindowFormHosting.Internals
{

    /// <summary>
    ///
    /// </summary>
    internal sealed class WindowsFormAppContext : WindowsFormAppContextBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="formProvider"></param>
        /// <param name="loggerFactory"></param>
        public WindowsFormAppContext(IWindowsFormProvider formProvider, ILoggerFactory loggerFactory)
            : base(formProvider, loggerFactory)
        {
        }
    }

}
