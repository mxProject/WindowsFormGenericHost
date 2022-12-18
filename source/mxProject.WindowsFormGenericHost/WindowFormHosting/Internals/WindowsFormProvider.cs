using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace mxProject.WindowFormHosting.Internals
{

    /// <summary>
    ///
    /// </summary>
    internal sealed class WindowsFormProvider : WindowsFormProviderBase
    {
        public WindowsFormProvider(IServiceProvider serviceProvider, ILogger<WindowsFormProvider> logger) : base(serviceProvider)
        {
            m_Logger = logger;
        }

        private readonly ILogger<WindowsFormProvider> m_Logger;

        /// <inheritdoc/>
        public override TForm CreateForm<TForm>()
        {
            var formType = typeof(TForm);

            m_Logger.LogDebug("Create a new instance of the form. Type = {formType}", formType);

            try
            {
                return base.CreateForm<TForm>();
            }
            catch (Exception ex)
            {
#pragma warning disable CA2254
                m_Logger.LogError(ex, ex.Message);
#pragma warning restore CA2254
                throw;
            }
        }

        /// <inheritdoc/>
        public override Form CreateForm(Type formType)
        {
            m_Logger.LogDebug("Create a new instance of the form. Type = {formType}", formType);

            try
            {
                return base.CreateForm(formType);
            }
            catch (Exception ex)
            {
#pragma warning disable CA2254
                m_Logger.LogError(ex, ex.Message);
#pragma warning restore CA2254
                throw;
            }
        }
    }

}
