using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;


namespace Silent_Scanner
{
    public partial class Form3 : Form
    {
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private bool dragging = false;
        public Form3()
        {
            InitializeComponent();
            try
            {
                label10.Text = "Silent scanner " + File.ReadAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\version");
            }
            catch { }
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
            Process.Start(@"C:\Windows\Temp");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // C: \Users\X240\AppData\Local\Temp
            Process.Start(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\" + Environment.UserName + @"\Recent");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            //C:\Users\X240\AppData\Roaming\.minecraft
            Process.Start(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft");
        }

        private void label6_Click(object sender, EventArgs e)
        {
            //C:\Windows\SoftwareDistribution\Download
            Process.Start(@"C:\Windows\SoftwareDistribution\Download");
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\$Recycle.Bin");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            /*   WindowState.
               WindowState = FormWindowState.Minimized;*/
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                Hide();
                //    Form1 f1 = new Form1();
                //    f1.ShowDialog();
                Close();
            }
            catch { }
        }

    }
}
