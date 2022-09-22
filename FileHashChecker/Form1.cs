using Microsoft.VisualBasic.Devices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System;


namespace FileHashChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private async void BtnCalcMD5_Click(object sender, EventArgs e)
        {
            string filename = textBox1.Text;
            if (File.Exists(filename) ){
                byte[] md5HashBytes = await Task.Run(() => ComputeMd5Hash(filename));
                byte[] sha256HashBytes = await Task.Run(() => ComputeSha256Hash(filename));

                textBox2.Text = ToHexadecimal(md5HashBytes);
                textBox3.Text = ToHexadecimal(sha256HashBytes);
            }
            else
            {
                textBox2.Text = "invalid";
            }
        }




          private byte[] ComputeSha256Hash(string filename)
           {
            // Create a SHA256 hash from string   
            byte[] result2 = null;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                int bufferSize = 10 * 1024 * 1024; //10MB
                using (var steam2 = new BufferedStream(File.OpenRead(filename), bufferSize))
                {
                    result2 = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(filename));
                }

            }
            return result2;

        }

        private byte[] ComputeMd5Hash(string filename)
        {
            byte[] result = null;
            using (MD5 md5 = MD5.Create())
            {
                int bufferSize = 10 * 1024 * 1024; //10MB
                using (var stream = new BufferedStream(File.OpenRead(filename), bufferSize))
                {
                    result = md5.ComputeHash(stream);
                }
            }
            return result;
        }
        static string ToHexadecimal(byte[] source)
        {
            if (source == null) return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (byte b in source)
            {
                sb.Append(b.ToString("X2")); // print bytes as Hexadecimal
            }
            return sb.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}