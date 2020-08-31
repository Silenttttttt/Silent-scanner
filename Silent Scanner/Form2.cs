using System;
using System.Drawing;
using System.Windows.Forms;

namespace Silent_Scanner
{
    public partial class Form2 : Form
    {
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private bool dragging = false;

        public Form2()
        {
            InitializeComponent();
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
            System.Diagnostics.Process.Start("https://github.com/Silenttttttt/Silent-scanner");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Minimized;
            }
            catch { }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Hide();
            Close();
        }
    }
}