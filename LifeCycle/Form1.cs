using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeCycle
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        Cells cells;

        public Form1()
        {
            InitializeComponent();
        }

        private void StartGame()
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            cells = new Cells(pictureBox1.Width, pictureBox1.Height);
            cells.InitialField();
            for (int y = 0; y < cells.Rows; y++)
            {
                for (int x = 0; x < cells.Columns; x++)
                {
                    if (cells.Field[y,x])
                    {
                        graphics.FillRectangle(Brushes.Crimson, x * 5, y * 5, 5, 5);
                    }
                }
            }
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);
            bool[,] newField = new bool[cells.Rows, cells.Columns];
            for (int y = 0; y < cells.Rows; y++)
            {
                for (int x = 0; x < cells.Columns; x++)
                {
                    bool hasLife = cells.Field[y,x];
                    int countNeighbors=cells.CountNeighbors(x,y);
                    if (!hasLife && countNeighbors == 3)
                        newField[y, x] = true;
                    else if (hasLife && (countNeighbors < 2 || countNeighbors > 3))
                        newField[y, x] = false;
                    else
                        newField[y, x] = cells.Field[y, x];
                    if (hasLife)
                    {
                        graphics.FillRectangle(Brushes.Crimson, x * 5, y * 5, 5, 5);
                    }
                }
            }
            cells.Field = newField;
            pictureBox1.Refresh();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
