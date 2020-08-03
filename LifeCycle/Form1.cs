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
            cells = new Cells(pictureBox1.Width, pictureBox1.Height,(int)numericUpDown1.Value);
            cells.InitialField();
            for (int y = 0; y < cells.Rows; y++)
            {
                for (int x = 0; x < cells.Columns; x++)
                {
                    if (cells.Field[y,x])
                    {
                        graphics.FillRectangle(Brushes.Crimson, x * cells.Size, y * cells.Size, cells.Size, cells.Size);
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
                        graphics.FillRectangle(Brushes.Crimson,
                            x * cells.Size, y * cells.Size, cells.Size, cells.Size);
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

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
                return;
            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / cells.Size;
                var y = e.Location.Y / cells.Size;
                cells.Field[y, x] = true;
            }
            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / cells.Size;
                var y = e.Location.Y / cells.Size;
                cells.Field[y, x] = false;
            }
        }
    }
}
