using System;
using System.Windows.Forms;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using mxProject.WindowFormHosting;

namespace WindowsFormGenericHostSample;

internal static partial class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
#if NET6_0
        ApplicationConfiguration.Initialize();
#else
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
#endif

        int mode = 1;

        switch (mode)
        {
            case 1:
                RunWithMyContext(args);
                break;
            case 2:
                RunWithDefaultContext(args);
                break;
            case 3:
                RunWithAppInfo(args);
                break;
            case 4:
                RunWithAppInfoWithDefaultContext(args);
                break;
            case 5:
                RunOnTaskTray(args);
                break;
            case 6:
                RunOnTaskTrayWithDefaultContext(args);
                break;
        }
    }

}
