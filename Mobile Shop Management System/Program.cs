using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Mobile_Shop_Management_System
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var destfile = Path.Combine(Environment.CurrentDirectory,"database.db");

            if (File.Exists(destfile))
            {
               
                Application.Run(new frmSplashScreen());
                
            }
            else
            {
                MessageBox.Show("Database is Missing!");
                Application.Run(new FrmRestore());
               
            }
           
        }
    }
}
