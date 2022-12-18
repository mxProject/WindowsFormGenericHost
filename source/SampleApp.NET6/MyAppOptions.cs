#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormGenericHostSample
{

    /// <summary>
    /// Options of this application.
    /// </summary>
    /// <remarks>
    /// "MyApp" section is bound.
    /// </remarks>
    internal class MyAppOptions
    {
        public string? AppName { get; private set; }

        public FormOptions? FormNames { get; private set; }

        public int StartupDelay { get; set; } = 3000;

        internal class FormOptions
        {
            public string? Form1 { get; private set; }
            public string? Form2 { get; private set; }
        }
    }


}
