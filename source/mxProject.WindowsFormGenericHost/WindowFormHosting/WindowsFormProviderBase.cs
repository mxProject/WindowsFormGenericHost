#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace mxProject.WindowFormHosting
{

    /// <summary>
    /// Basic implementation of <see cref="IWindowsFormProvider"/>.
    /// </summary>
    public abstract class WindowsFormProviderBase : IWindowsFormProvider
    {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="serviceProvider"></param>
        protected WindowsFormProviderBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <inheritdoc/>
        public virtual TForm CreateForm<TForm>() where TForm : Form
        {
            try
            {
                var frm = ServiceProvider.GetService<TForm>();

                if (frm != null) { return frm; }

                return Activator.CreateInstance<TForm>();
            }
            catch (Exception ex)
            {
                throw new Exception($"A new instance of the form of the specified type could not be activated. {ex.Message} Type = {typeof(TForm)}", ex);
            }
        }

        /// <inheritdoc/>
        public virtual Form CreateForm(Type formType)
        {
            try
            {
                if (ServiceProvider.GetService(formType) is Form frm) { return frm; }

                return (Form)Activator.CreateInstance(formType)!;
            }
            catch (Exception ex)
            {
                throw new Exception($"A new instance of the form of the specified type could not be activated. {ex.Message} Type = {formType}", ex);
            }
        }
    }

}
