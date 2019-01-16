using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mobile_Shop_Management_System
{
    public partial class FrmRestore : Form
    {
        public FrmRestore()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)

        {
            openFileDialog1.Filter = "Database Files|*.db";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                
                label1.Text = openFileDialog1.FileName;
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
          RestoreDB(openFileDialog1.FileName, Environment.CurrentDirectory, "database.db");
        }
        private static void RestoreDB(string srcfilePath, string destfilePath, string destFileName)
        {
            //var srcfile = Path.Combine(srcfilePath, srcFilename);
            var destfile = Path.Combine(destfilePath, destFileName);
            

            if (File.Exists(destfile)) File.Delete(destfile);

            File.Copy(srcfilePath, destfile);
        }
    }
}
