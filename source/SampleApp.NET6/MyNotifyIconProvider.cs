using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mxProject.WindowFormHosting;

namespace WindowsFormGenericHostSample;

/// <summary>
/// Provides a NotifyIcon for this application.
/// </summary>
internal class MyNotifyIconProvider : INotifyIconProvider
{
    public MyNotifyIconProvider(MyAppContext context)
    {
        m_Context = context;
    }

    public MyNotifyIconProvider(IWindowsFormAppContext context)
    {
        m_Context = context;
    }

    private readonly IWindowsFormAppContext m_Context;

    /// <inheritdoc/>
    public NotifyIcon CreateNotifyIcon()
    {
        return new NotifyIcon
        {
            Icon = new System.Drawing.Icon("sample.ico"),
            ContextMenuStrip = ContextMenu(),
            Text = "Sample Application",
            Visible = true
        };
    }

    private ContextMenuStrip ContextMenu()
    {
        var menu = new ContextMenuStrip();

        menu.Items.Add("Show Form1", null, (sender, e) =>
        {
            using var form = m_Context.FormProvider.CreateForm<Form1>();

            form.ShowDialog();
        });

        menu.Items.Add("Show Form2", null, (sender, e) =>
        {
            using var form = m_Context.FormProvider.CreateForm<Form2>();

            form.ShowDialog();
        });

        menu.Items.Add("-");

        menu.Items.Add("Exit", null, (sender, e) =>
        {
            Application.Exit();
        });

        return menu;
    }
}

