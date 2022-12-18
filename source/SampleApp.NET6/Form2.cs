#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using mxProject.WindowFormHosting;

namespace WindowsFormGenericHostSample
{
    internal partial class Form2 : Form
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Form2()
        {
            InitializeComponent();

            m_Context = null;
            m_Logger = null;

            Init();
        }

        /// <summary>
        /// Constructor for DI.
        /// </summary>
        /// <param name="context"></param>
        public Form2(IWindowsFormAppContext context)
        {
            InitializeComponent();

            m_Logger = context.LoggerFactory.CreateLogger<Form2>();

            if (context is MyAppContext m_Context)
            {
            }

            Init();
        }

        /// <summary>
        /// Constructor for DI.
        /// </summary>
        /// <param name="context"></param>
        public Form2(MyAppContext context)
        {
            InitializeComponent();

            m_Context = context;
            m_Logger = m_Context.LoggerFactory.CreateLogger<Form2>();

            Init();
        }

        private readonly MyAppContext? m_Context;
        private readonly ILogger<Form2>? m_Logger;

        private void Init()
        {
            Load += Form2_Load;
            Disposed += Form2_Disposed;
        }

        private void Form2_Load(object? sender, EventArgs e)
        {
            this.Text = $"{m_Context?.CurrentAppOptions.AppName}: {m_Context?.CurrentAppOptions.FormNames?.Form2}";

            m_Logger?.LogInformation($"{this.Name} is loaded.");
        }

        private void Form2_Disposed(object? sender, EventArgs e)
        {
            m_Logger?.LogInformation($"{this.Name} is disposed.");
        }

    }
}
