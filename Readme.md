# mxProject.WindowsFormGenericHost

## Overview

This is an extension library for Generic Host.

* Runs a Windows Forms application on Generic Host.
* Runs a task tray resident application.

## Requirement

* .NET 6 or .NET Framework 4.7.2+

## Usage

### Entry point implementation example

Instead of calling Application.Run method in the entry point, rewrite the application to run on Generic Host.

```c#
using Microsoft.Extensions.Hosting;
using mxProject.WindowFormHosting;

static void Main(string[] args)
{
#if NET6_0
    ApplicationConfiguration.Initialize();
#else
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
#endif

    // Application.Run(new Form1());
    RunWindowsFormApp(args);
}
```

MyAppContext class shown in the following example implementation is a class that implements mxProject.WindowFormHosting.IWindowsFormAppContext interface. If you want to use a specific type of context, define a context class that implements this interface.

### CASE1 : Specify the main form type 

Use AddWindowsFormApp&lt;TForm&gt; method.

```c#
/// <summary>
/// Runs this application.
/// </summary>
static void RunWindowsFormApp(string[] args)
{
    void OnStart(IWindowsFormAppContext context) { }
    void OnExit(IWindowsFormAppContext context) { }

    IHostBuilder builder = Host.CreateDefaultBuilder(args)
        .AddWindowsFormApp<Form1>(onStart: OnStart, onExit: OnExit)
        ;

    builder.Build().Run();
}
```

### CASE2 : Specify the main form type and the context type

Use AddWindowsFormApp&lt;TContext, TForm&gt; method.

```c#
/// <summary>
/// Runs this application.
/// </summary>
static void RunWindowsFormApp(string[] args)
{
    void OnStart(MyAppContext context) { }
    void OnExit(MyAppContext context) { }

    IHostBuilder builder = Host.CreateDefaultBuilder(args)
        .AddWindowsFormApp<MyAppContext, Form1>(onStart: OnStart, onExit: OnExit);

    builder.Build().Run();
}
```

### CASE3 : Specify the application information

Use AddWindowsFormApp(IWindowsFormAppInfo) method or AddWindowsFormApp&lt;TContext&gt;(IWindowsFormAppInfo&lt;TContext&gt;) method.

```c#
/// <summary>
/// Runs this application.
/// </summary>
static void RunWindowsFormApp(string[] args)
{
    IHostBuilder builder = Host.CreateDefaultBuilder(args)
        .AddWindowsFormApp(new MyWindowsFormAppInfo());

    builder.Build().Run();
}

internal class MyWindowsFormAppInfo : IWindowsFormAppInfo<MyAppContext>
{
    internal MyWindowsFormAppInfo() {}

    // It is possible to determine the type of the main form at runtime.
    public Type StartupObjectType => typeof(Form1)

    public void OnStart(MyAppContext context) {}
    public void OnExit(MyAppContext context) {}
}
```

### CASE4 : Run as a task tray resident application.

Use AddTaskTrayApp&lt;TNotifyIcon&gt; method.

```c#
/// <summary>
/// Run as a task tray resident application.
/// </summary>
static void RunWindowsFormApp(string[] args)
{
    void OnStart(IWindowsFormAppContext context) { }
    void OnExit(IWindowsFormAppContext context) { }

    IHostBuilder builder = Host.CreateDefaultBuilder(args)
        .AddTaskTrayApp<MyNotifyIconProvider>(onStart: OnStart, onExit: OnExit);

    builder.Build().Run();
}

internal class MyNotifyIconProvider : INotifyIconProvider
{
    public MyNotifyIconProvider(IWindowsFormAppContext context)
    {
        m_Context = context;
    }

    private readonly IWindowsFormAppContext m_Context;

    public NotifyIcon CreateNotifyIcon()
    {
        return new NotifyIcon
        {
            Icon = new System.Drawing.Icon("sampleapp.ico");,
            ContextMenuStrip = ContextMenu(),
            Text = "Sample Application",
            Visible = true
        };
    }

    private ContextMenuStrip ContextMenu()
    {
        var menu = new ContextMenuStrip();

        menu.Items.Add("Exit", null, (sender, e) =>
        {
            Application.Exit();
        });

        return menu;
    }
}
```

### CASE5 : Run as a task tray resident application. Specify the context type.

Use AddTaskTrayApp&lt;TContext, TNotifyIcon&gt; method.

```c#
/// <summary>
/// Run as a task tray resident application.
/// </summary>
static void RunWindowsFormApp(string[] args)
{
    void OnStart(MyAppContext context) { }
    void OnExit(MyAppContext context) { }

    IHostBuilder builder = Host.CreateDefaultBuilder(args)
        .AddTaskTrayApp<MyAppContext, MyNotifyIconProvider>(onStart: OnStart, onExit: OnExit);

    builder.Build().Run();
}

internal class MyNotifyIconProvider : INotifyIconProvider
{
    // An instance of MyAppContext class is injected.
    public MyNotifyIconProvider(MyAppContext context)
    {
        m_Context = context;
    }

    private readonly MyAppContext m_Context;

    // Omitted below
}
```

## Licence

[MIT](https://opensource.org/licenses/mit-license.php)

