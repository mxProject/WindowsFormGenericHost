using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mxProject.WindowFormHosting
{

    /// <summary>
    /// Provides the functionality needed to create an instance of <see cref="Form"/>.
    /// </summary>
    public interface IWindowsFormProvider
    {
        /// <summary>
        /// Creates a Form instance of the specified type.
        /// </summary>
        /// <typeparam name="TForm">The type of the form.</typeparam>
        /// <returns></returns>
        TForm CreateForm<TForm>() where TForm : Form;

        /// <summary>
        /// Creates a Form instance of the specified type.
        /// </summary>
        /// <param name="formType">The type of the form.</param>
        /// <returns></returns>
        Form CreateForm(Type formType);
    }

}
