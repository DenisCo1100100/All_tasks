using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Node
{
    public partial class Vertex: UserControl
    {
        bool isDown;

        public Color Цвет_номер
        {
            set { label1.ForeColor = value; this.Refresh(); }
            get { return label1.ForeColor; }
        }
        public string Номер
        {
            set { label1.Text = value; this.Refresh(); }
            get { return label1.Text; }
        }

        public Vertex()
        {
            InitializeComponent();
            Номер = "1";
            Цвет_номер = Color.Black;
        }
        public Vertex(Point p, int number)
        {
            Location = new Point(p.X - Form.ActiveForm.Location.X - 28, p.Y - Form.ActiveForm.Location.Y - 50);
            InitializeComponent();
            Номер = number.ToString();
            Цвет_номер = Color.Black;
        }

        private void Vertex_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath(); 
            path.AddEllipse(0, 0, 60, 60); 
            this.Region = new Region(path);
        }

        private void Vertex_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
        }

        private void Vertex_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                Location = new Point(MousePosition.X - Form.ActiveForm.Location.X - 28, 
                    MousePosition.Y - Form.ActiveForm.Location.Y - 50);
            }
        }

        private void Vertex_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                Location = new Point(MousePosition.X - Form.ActiveForm.Location.X - 28,
                    MousePosition.Y - Form.ActiveForm.Location.Y - 50);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                Location = new Point(MousePosition.X - Form.ActiveForm.Location.X - 28,
                    MousePosition.Y - Form.ActiveForm.Location.Y - 50);
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
        }
    }
}
