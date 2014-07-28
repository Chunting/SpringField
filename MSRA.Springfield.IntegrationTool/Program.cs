using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MSRA.Springfield.IntegrationTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                log4net.Config.DOMConfigurator.Configure();
                Application.Run(new MainForm());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
