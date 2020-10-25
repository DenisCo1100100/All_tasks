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

        private void Form1_Load(object sender, EventArgs e)
        {
            ContextMenu contextMenu1; contextMenu1 = new ContextMenu();
            MenuItem menuItem1;
            menuItem1 = new MenuItem(); 
            MenuItem menuItem2;
            menuItem2 = new MenuItem();
            MenuItem menuItem3; 
            menuItem3 = new MenuItem();

            contextMenu1.MenuItems.AddRange(new MenuItem[] { menuItem1, menuItem2, menuItem3 });

            menuItem1.Index = 0; 
            menuItem1.Text = "MenuItem1"; 
            menuItem2.Index = 1; 
            menuItem2.Text = "MenuItem2"; 
            menuItem3.Index = 2; 
            menuItem3.Text = "MenuItem3";

            this.ContextMenu = contextMenu1;
        }
    }
}
