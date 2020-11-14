using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompGraphics_Lab8
{
    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile("D:\\fieldSprite.jpg");
            // Hello
            // Helllooooooooooooooooooooo
            Console.WriteLine("Hello!");
        }

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
