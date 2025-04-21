using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OEE1.PL;

namespace OEE1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //WindowsLogin windowsLogin = new WindowsLogin();
            //windowsLogin.Show();
            //windowsLogin.FormClosed += WindowsLogin_Closed;
            //Application.Run();
            Application.Run(new MenuOEE());
        }

        //private static void WindowsLogin_Closed(object sender, FormClosedEventArgs e)
        //{
        //    ((Form)sender).FormClosed -= WindowsLogin_Closed;

        //    if (Application.OpenForms.Count == 0)
        //    {
        //        Application.ExitThread();
        //        Application.Exit();
        //    }
        //    else
        //    {
        //        Application.OpenForms[0].FormClosed += WindowsLogin_Closed;
        //    }
        //}
    }
}
