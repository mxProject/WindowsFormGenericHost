using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace mxProject.WindowFormHosting
{

    /// <summary>
    /// Basic implementation of <see cref="IWindowsFormAppContext"/>.
    /// </summary>
    public abstract class WindowsFormAppContextBase : IWindowsFormAppContext
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="formProvider">The form provider.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        protected WindowsFormAppContextBase(IWindowsFormProvider formProvider, ILoggerFactory loggerFactory)
        {
            FormProvider = formProvider;
            LoggerFactory = loggerFactory;
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~WindowsFormAppContextBase()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        /// <inheritdoc/>
        public IWindowsFormProvider FormProvider { get; }

        /// <inheritdoc/>
        public ILoggerFactory LoggerFactory { get; }
    }

}
