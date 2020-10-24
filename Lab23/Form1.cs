using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Node;

namespace Lab23
{
    public partial class Form1 : Form
    {
        public int number { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            number++;
            Vertex ob = new Vertex(new Point(MousePosition.X, MousePosition.Y), number); 
            this.Controls.Add(ob);
        }
    }
}
