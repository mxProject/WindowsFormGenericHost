using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mxProject.WindowFormHosting;

namespace WindowsFormGenericHostSample
{

    /// <summary>
    /// Custom FormProvider for this application.
    /// </summary>
    internal class MyWindowsFormProvider : IWindowsFormProvider
    {
        /// <inheritdoc/>
        public TForm CreateForm<TForm>() where TForm : Form
        {
            return Activator.CreateInstance<TForm>();
        }

        /// <inheritdoc/>
        public Form CreateForm(Type formType)
        {
            return (Form)Activator.CreateInstance(formType)!;
        }
    }

}
