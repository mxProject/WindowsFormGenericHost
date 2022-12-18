using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace mxProject.WindowFormHosting
{

    /// <summary>
    /// Provides the necessary functionality for a context that manages application state.
    /// </summary>
    public interface IWindowsFormAppContext : IDisposable
    {

        /// <summary>
        /// Gets the form provider.
        /// </summary>
        IWindowsFormProvider FormProvider { get; }

        /// <summary>
        /// Gets the logger factory.
        /// </summary>
        ILoggerFactory LoggerFactory { get; }

    }

}
