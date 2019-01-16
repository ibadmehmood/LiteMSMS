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
    public partial class frmBackup : Form

    {
        private static string filename = "mock.db";
        private static readonly string filePath = Environment.CurrentDirectory;
        private static string bkupFilename = Path.GetFileNameWithoutExtension(filename) + ".bak";
        public frmBackup()
        {
            InitializeComponent();
        }

        

        private static void BackupDB(string srcfilePath, string destfilePath,string srcFilename, string destFileName)
        {
            var srcfile = Path.Combine(srcfilePath, srcFilename);
            var destfile = Path.Combine(destfilePath, destFileName);

            if (File.Exists(destfile)) File.Delete(destfile);

            File.Copy(srcfile, destfile);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
           
            label1.Text=folderBrowserDialog1.SelectedPath.ToString();
        }

        private void button2_Click(object sender, EventArgs e)

        {
            MessageBox.Show(filePath);
            BackupDB(filePath,folderBrowserDialog1.SelectedPath,"database.db", "data.db");
        }

    }
}
