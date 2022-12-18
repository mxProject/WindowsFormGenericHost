#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mxProject.WindowFormHosting
{

    /// <summary>
    /// Options for Windows Forms application host.
    /// </summary>
    public sealed class WindowsFormAppHostOptions
    {

        /// <summary>
        /// Gets the type of the form provider. This type must implement <see cref="IWindowsFormProvider"/> interface.
        /// </summary>
        public Type? CustomWindowsFormProviderType
        {
            get { return m_CustomWindowsFormProviderType; }
            set
            {
                if (m_CustomWindowsFormProviderType == value) { return; }
                AssertCustomWindowsFormProviderType(value);
                m_CustomWindowsFormProviderType = value;
            }
        }
        private Type? m_CustomWindowsFormProviderType;

        private static void AssertCustomWindowsFormProviderType(Type? type)
        {
            if (type == null) { return; }

            if (!type.IsClass)
            {
                throw new ArgumentException($"The specified type is not a class. Type = {type.FullName}");
            }

            if (!typeof(IWindowsFormProvider).IsAssignableFrom(type))
            {
                throw new ArgumentException($"The specified type does not implement IWindowsFormProvider interface. Type = {type.FullName}");
            }
        }
    }

}
