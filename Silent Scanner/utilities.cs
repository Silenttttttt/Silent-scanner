using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Silent_Scanner
{
    public partial class utilities : Form
    {
        private List<string> allprograms = new List<string>();
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private bool dragging = false;
        public utilities()
        {
            InitializeComponent();
            FormClosing += Form2_FormClosing;
            try
            {
                label10.Text = "Silent scanner " + File.ReadAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\version");
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Thread button1_Clickee = new Thread(button1_Clicke);
            button1_Clickee.Start();

        }
        private void button1_Clicke()
        {


            try
            {
                using (var clientlauncher = new WebClient())
                {
                    string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\";
                    string download = @"https://silentscanner.000webhostapp.com/Silent_scanner/Tools/ProcessHacker.exe";
                    clientlauncher.DownloadFile(download, path + download.Split('/')[download.Split('/').Length - 1]);
                    Process.Start(path + download.Split('/')[download.Split('/').Length - 1]);
                    allprograms.Add(path + download.Split('/')[download.Split('/').Length - 1]);
                }
            }
            catch { }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            Thread button10_Clickee = new Thread(button10_Clicke);
            button10_Clickee.Start();


        }
        private void button10_Clicke()
        {


            try
            {
                using (var clientlauncher = new WebClient())
                {
                    string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\";
                    string download = @"https://silentscanner.000webhostapp.com/Silent_scanner/Tools/LastActivityView.exe";
                    clientlauncher.DownloadFile(download, path + download.Split('/')[download.Split('/').Length - 1]);
                    Process.Start(path + download.Split('/')[download.Split('/').Length - 1]);
                    allprograms.Add(path + download.Split('/')[download.Split('/').Length - 1]);
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread button3_Clickee = new Thread(button3_Clicke);
            button3_Clickee.Start();



        }
        private void button3_Clicke()
        {
            try
            {
                using (var clientlauncher = new WebClient())
                {
                    string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\";
                    string download = @"https://silentscanner.000webhostapp.com/Silent_scanner/Tools/Everything.exe";
                    clientlauncher.DownloadFile(download, path + download.Split('/')[download.Split('/').Length - 1]);
                    Process.Start(path + download.Split('/')[download.Split('/').Length - 1]);
                    allprograms.Add(path + download.Split('/')[download.Split('/').Length - 1]);
                }
            }
            catch { }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            Thread button4_Clickee = new Thread(button4_Clicke);
            button4_Clickee.Start();
        }
        private void button4_Clicke()
        {
            try
            {
                using (var clientlauncher = new WebClient())
                {
                    string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\";
                    string download = @"https://silentscanner.000webhostapp.com/Silent_scanner/Tools/DriveView.exe";
                    clientlauncher.DownloadFile(download, path + download.Split('/')[download.Split('/').Length - 1]);
                    Process.Start(path + download.Split('/')[download.Split('/').Length - 1]);
                    allprograms.Add(path + download.Split('/')[download.Split('/').Length - 1]);
                }
            }
            catch { }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Thread button5_Clickee = new Thread(button5_Clicke);
            button5_Clickee.Start();

        }
        private void button5_Clicke()
        {
            try
            {
                using (var clientlauncher = new WebClient())
                {
                    string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\";
                    string download = @"https://silentscanner.000webhostapp.com/Silent_scanner/Tools/ShellBagsView.exe";
                    clientlauncher.DownloadFile(download, path + download.Split('/')[download.Split('/').Length - 1]);
                    Process.Start(path + download.Split('/')[download.Split('/').Length - 1]);
                    allprograms.Add(path + download.Split('/')[download.Split('/').Length - 1]);
                }
            }
            catch { }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Thread button6_Clickee = new Thread(button6_Clicke);
            button6_Clickee.Start();

        }
        private void button6_Clicke()
        {

            try
            {
                using (var clientlauncher = new WebClient())
                {
                    string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\";
                    string download = @"https://silentscanner.000webhostapp.com/Silent_scanner/Tools/UltraFileSearch.exe";
                    clientlauncher.DownloadFile(download, path + download.Split('/')[download.Split('/').Length - 1]);
                    Process.Start(path + download.Split('/')[download.Split('/').Length - 1]);
                    allprograms.Add(path + download.Split('/')[download.Split('/').Length - 1]);
                }
            }
            catch { }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Thread button7_Clickee = new Thread(button7_Clicke);
            button7_Clickee.Start();


        }
        private void button7_Clicke()
        {

            try
            {
                using (var clientlauncher = new WebClient())
                {
                    string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\";
                    string download = @"https://silentscanner.000webhostapp.com/Silent_scanner/Tools/UserAssist.exe";
                    clientlauncher.DownloadFile(download, path + download.Split('/')[download.Split('/').Length - 1]);
                    Process.Start(path + download.Split('/')[download.Split('/').Length - 1]);
                    allprograms.Add(path + download.Split('/')[download.Split('/').Length - 1]);
                }
            }
            catch { }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Thread button8_Clickee = new Thread(button8_Clicke);
            button8_Clickee.Start();

        }
        private void button8_Clicke()
        {
            try
            {
                using (var clientlauncher = new WebClient())
                {
                    string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\";
                    string download = @"https://silentscanner.000webhostapp.com/Silent_scanner/Tools/CachedProgramsList.exe";
                    clientlauncher.DownloadFile(download, path + download.Split('/')[download.Split('/').Length - 1]);
                    Process.Start(path + download.Split('/')[download.Split('/').Length - 1]);
                    allprograms.Add(path + download.Split('/')[download.Split('/').Length - 1]);
                }
            }
            catch { }


        }
        private void button9_Click(object sender, EventArgs e)
        {
            Thread button9_Clickee = new Thread(button9_Clicke);
            button9_Clickee.Start();


        }
        private void button9_Clicke()
        {
            try
            {
                using (var clientlauncher = new WebClient())
                {
                    string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\";
                    string download = @"https://silentscanner.000webhostapp.com/Silent_scanner/Tools/PreviousFilesRecovery.exe";
                    clientlauncher.DownloadFile(download, path + download.Split('/')[download.Split('/').Length - 1]);
                    Process.Start(path + download.Split('/')[download.Split('/').Length - 1]);
                    allprograms.Add(path + download.Split('/')[download.Split('/').Length - 1]);
                }
            }
            catch { }


        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                for (int i = 0; i < allprograms.Count; i++)
                {
                    try
                    {
                        File.Delete(allprograms[i]);
                    }
                    catch { }
                }

            }

            catch { }
            Hide();
            //    Form1 f1 = new Form1();
            //    f1.ShowDialog();
            //   Close();
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void label1_Click(object sender, EventArgs e)
        {


        }

        private void label2_Click(object sender, EventArgs e)
        {


        }

        private void label3_Click(object sender, EventArgs e)
        {


        }

        private void label4_Click(object sender, EventArgs e)
        {


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < allprograms.Count; i++)
                {
                    try
                    {
                        File.Delete(allprograms[i]);
                    }
                    catch { }
                }
                Hide();
                //    Form1 f1 = new Form1();
                //    f1.ShowDialog();
                Close();
            }
            catch { }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            /*   WindowState.
               WindowState = FormWindowState.Minimized;*/
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Thread button2_Click_1ee = new Thread(button2_Click_1e);
            button2_Click_1ee.Start();


        }

        private void button2_Click_1e()
        {
            try
            {
                using (var clientlauncher = new WebClient())
                {
                    string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\";
                    string download = @"https://silentscanner.000webhostapp.com/Silent_scanner/Tools/jd-gui-1.6.6.jar";
                    clientlauncher.DownloadFile(download, path + download.Split('/')[download.Split('/').Length - 1]);
                    Process.Start(path + download.Split('/')[download.Split('/').Length - 1]);
                    allprograms.Add(path + download.Split('/')[download.Split('/').Length - 1]);
                }
            }
            catch { }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Thread button11_Clickee = new Thread(button11_Clicke);
            button11_Clickee.Start();
        }
        private void button11_Clicke()
        {

            try
            {
                using (var clientlauncher = new WebClient())
                {
                    string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\";
                    string download = @"https://silentscanner.000webhostapp.com/Silent_scanner/Tools/RegScanner.exe";
                    clientlauncher.DownloadFile(download, path + download.Split('/')[download.Split('/').Length - 1]);
                    Process.Start(path + download.Split('/')[download.Split('/').Length - 1]);
                    allprograms.Add(path + download.Split('/')[download.Split('/').Length - 1]);
                }
            }
            catch { }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            Thread button12_Clickee = new Thread(button12_Clicke);
            button12_Clickee.Start();
        }
        private void button12_Clicke()
        {
            try
            {
                using (var clientlauncher = new WebClient())
                {
                    string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\";
                    string download = @"https://silentscanner.000webhostapp.com/Silent_scanner/Tools/USBDeview.exe";
                    clientlauncher.DownloadFile(download, path + download.Split('/')[download.Split('/').Length - 1]);
                    Process.Start(path + download.Split('/')[download.Split('/').Length - 1]);
                    allprograms.Add(path + download.Split('/')[download.Split('/').Length - 1]);
                }
            }
            catch { }
        }
    }
}