using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CompGraphics_Lab8
{
    public partial class Form1 : Form
    {
        PictureBox tank = new PictureBox();
        PictureBox[] GroundObjects = new PictureBox[maxCountGroundObjects];
        const int maxCountGroundObjects = 65;
        int[,] matrix;
        int xCountMatrix, yCountMatrix;

        int speedTank = 5;
        int x1, y1;
        bool isPlaying = true;
        int lives = 5;
        Random rand = new Random();

        public Form1() {
            InitializeComponent();         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Label1.Text = lives + "/5 жизней осталось.";
            pbBackground.Image = Image.FromFile("D:\\fieldSprite.jpg");

            tank.Image = Image.FromFile("D:\\tankFront.png");
            x1 = pbBackground.Width / 2 - tank.Image.Width / 2;
            y1 = pbBackground.Height - tank.Image.Height;
            tank.BackColor = Color.Transparent;
            tank.Location = new Point(x1, y1);
            tank.Size = new Size(tank.Image.Width, tank.Image.Height);
            tank.Tag = "Tank";
            pbBackground.Controls.Add(tank);
            tank.BringToFront();

            xCountMatrix = (pbBackground.Width + 50) / 50;
            yCountMatrix = (pbBackground.Height - tank.Height + 50) / 50;

            matrix = new int[xCountMatrix, yCountMatrix];
            int typeObject, count = 0;
            for (int i = 0; i < maxCountGroundObjects; i++)
            {
                GroundObjects[count] = new PictureBox();
                typeObject = rand.Next(0, 5);
                if (typeObject == 0) GroundObjects[count].Image = Image.FromFile("D:\\Tree_2.png");
                else if (typeObject == 1) GroundObjects[count].Image = Image.FromFile("D:\\Tree_3.png");
                else if (typeObject == 2) GroundObjects[count].Image = Image.FromFile("D:\\landMine_1.png");
                else GroundObjects[count].Image = Image.FromFile("D:\\landMine_2.png");

                ret:
                int x = rand.Next(0, xCountMatrix);
                int y = rand.Next(0, yCountMatrix - 1);
                if (matrix[x, y] == 1) goto ret;
                
                matrix[x, y] = 1;

                GroundObjects[count].BackColor = Color.Transparent;
                GroundObjects[count].Location = new Point(x * 50, y * 50);
                GroundObjects[count].Size = new Size(GroundObjects[count].Image.Width, GroundObjects[count].Image.Height);
                GroundObjects[count].Tag = "Enemy";
                pbBackground.Controls.Add(GroundObjects[count]);
                GroundObjects[count].BringToFront();

                count++;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "Остановить игру")
            {
                btnPause.Text = "Начать игру";
                isPlaying = false;
            }
            else
            {
                btnPause.Text = "Остановить игру";
                isPlaying = true;
            }
        }

        private void pbBackground_Paint(object sender, PaintEventArgs e)
        {
            if (tank.Location.Y <= -10) ShowEndDialog("Вы дошли до финала и у вас осталось " + lives + " жизней!");
            else if (lives == 0) ShowEndDialog("Вы потеряли все жизни и проиграли!");

            /*Rectangle rectTank = new Rectangle(tank.Location.X, tank.Location.Y, tank.Width - 10, tank.Height - 10);
            rectTank.Location = tank.Location;
            for (int i = 0; i < GroundObjects.Length; i++)
            {
                Rectangle rectEnemy = GroundObjects[i].DisplayRectangle;
                rectEnemy.Location = GroundObjects[i].Location;

                if (rectTank.IntersectsWith(rectEnemy))
                {
                    lives--;
                    Label1.Text = lives + "/5 жизней осталось.";
                    GroundObjects[i].Invalidate();
                    GroundObjects[i].Dispose();
                }
            }*/
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (isPlaying)
            {
                if (e.KeyCode == Keys.A)
                {
                    if (tank.Location.X > 0)
                    {
                        tank.Location = new Point(tank.Location.X - speedTank, tank.Location.Y);
                        tank.Image = Image.FromFile("D:\\tankLeft.png");
                    }
                }
                else if (e.KeyCode == Keys.D)
                {
                    if (tank.Location.X < this.ClientRectangle.Width - tank.Size.Width)
                    {
                        tank.Location = new Point(tank.Location.X + speedTank, tank.Location.Y);
                        tank.Image = Image.FromFile("D:\\tankRight.png");
                    }
                }
                else if (e.KeyCode == Keys.W)
                {
                    if (tank.Location.Y > -10)
                    {
                        tank.Location = new Point(tank.Location.X, tank.Location.Y - speedTank);
                        tank.Image = Image.FromFile("D:\\tankFront.png");
                    }
                }
                else if (e.KeyCode == Keys.S)
                {
                    if (tank.Location.Y < pbBackground.Height - tank.Size.Height)
                    {
                        tank.Location = new Point(tank.Location.X, tank.Location.Y + speedTank);
                        tank.Image = Image.FromFile("D:\\tankBack.png");
                    }
                }
            }
            if (e.KeyCode == Keys.Enter) btnPause_Click(sender, e);
        }

        private void ShowEndDialog(string text)
        {
            DialogResult result;
            result = MessageBox.Show(text);
            if (result == DialogResult.OK) Application.Exit();
        }
    }
}