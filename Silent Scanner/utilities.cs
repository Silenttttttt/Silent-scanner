using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.ProcessHacker);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.LastActivityView);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.Everything);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.DriveView);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.ShellBagsView);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.UltraFileSearch);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);

                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.UserAssist);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.CachedProgramsList);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.PreviousFilesRecovery);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
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
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.ProcessHacker);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.Everything);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.UltraFileSearch);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.ShellBagsView);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.PreviousFilesRecovery);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.DriveView);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.UserAssist);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.ShellBagsView);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
        }
        private void label9_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempFile = Path.GetTempFileName();
                File.WriteAllBytes(strTempFile, Properties.Resources.LastActivityView);
                string newplace = strTempFile.Split('.')[0] + ".exe";
                File.Move(strTempFile, newplace);
                System.Diagnostics.Process.Start(newplace);
                allprograms.Add(newplace);
            }
            catch { }
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
    }
}