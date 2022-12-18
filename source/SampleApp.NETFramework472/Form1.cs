#nullable enable

using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using mxProject.WindowFormHosting;

namespace WindowsFormGenericHostSample
{

    internal partial class Form1 : Form
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            m_FormProvider = new MyWindowsFormProvider();
            m_Logger = null;

            Init();
        }

        /// <summary>
        /// Constructor for DI.
        /// </summary>
        /// <param name="formProvider"></param>
        /// <param name="logger"></param>
        /// <param name="publisher"></param>
        /// <param name="subscriber"></param>
        public Form1(
            IWindowsFormProvider formProvider,
            ILogger<Form1> logger
            )
        {
            InitializeComponent();

            m_FormProvider = formProvider;
            m_Logger = logger;

            Init();
        }

        private readonly IWindowsFormProvider? m_FormProvider;
        private readonly ILogger<Form1>? m_Logger;

        private void Init()
        {
            Load += Form1_Load;
            Disposed += Form1_Disposed;
            button1.Click += Button1_Click;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            this.Text = $"{MyAppContext.Current?.CurrentAppOptions.AppName}: {MyAppContext.Current?.CurrentAppOptions.FormNames?.Form1}";

            m_Logger?.LogInformation($"{this.Name} is loaded.");
        }

        private void Form1_Disposed(object? sender, EventArgs e)
        {
            m_Logger?.LogInformation($"{this.Name} is disposed.");
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            if (m_FormProvider == null) { return; }

            using var frm = m_FormProvider.CreateForm<Form2>();

            frm.ShowDialog(this);
        }
    }

}
