using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DenemeHash
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       /* private static string GetChecksum(string file)
        {
            using (System.IO.FileStream stream = File.OpenRead(file))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }
        */
        private static string GetChecksumBuffered(Stream stream)
        {
            using (var bufferedStream = new BufferedStream(stream, 1024 * 32))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(bufferedStream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        private void btnFileUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string FilePath = openFileDialog.FileName;
            MessageBox.Show(FilePath);
            string filesize = "";
            long size = new System.IO.FileInfo(FilePath).Length;
            filesize = size.ToString();
            var stopwatch = new Stopwatch();
            txtFileSize.Text = filesize;
            stopwatch.Start();
            //   string str = "";
            stopwatch.Stop();
            var stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            var fileStream = new System.IO.FileStream(FilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read);
            string str2 = GetChecksumBuffered(fileStream);
            txtHash.Text = str2;
            long a = stopwatch2.ElapsedMilliseconds;
            string responsetime = "";
            responsetime = a.ToString();
            txtResponseTime.Text = responsetime;
        }
    }
}
