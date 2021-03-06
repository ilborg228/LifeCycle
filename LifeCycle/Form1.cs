﻿using System;
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
        bool startGame;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            cells = new Cells(pictureBox1.Width, pictureBox1.Height, (int)numericUpDown1.Value);
            graphics.Clear(Color.Black);
            startGame = true;
        }

        private void StartGame()
        {
            startGame = false;
            cells.InitialField();
            for (int i = 0; i < cells.Rows; i++)
            {
                for (int j = 0; j < cells.Columns; j++)
                {
                    if (cells.Field[i,j])
                    {
                        graphics.FillRectangle(Brushes.Crimson,
                            j * cells.Size, i * cells.Size, cells.Size, cells.Size);
                    }
                }
            }
            timer1.Enabled = true;
        }
        private void ContinueGame()
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);
            bool[,] newField = new bool[cells.Rows, cells.Columns];
            for (int i = 0; i < cells.Rows; i++)
            {
                for (int j = 0; j < cells.Columns; j++)
                {
                    bool hasLife = cells.Field[i,j];
                    int countNeighbors=cells.CountNeighbors(j,i);
                    if (!hasLife && countNeighbors == 3)
                        newField[i, j] = true;
                    else if (hasLife && (countNeighbors < 2 || countNeighbors > 3))
                        newField[i, j] = false;
                    else
                        newField[i, j] = cells.Field[i, j];
                    if (hasLife)
                    {
                        graphics.FillRectangle(Brushes.Crimson,
                            j * cells.Size, i * cells.Size, cells.Size, cells.Size);
                    }
                }
            }
            cells.Field = newField;
            pictureBox1.Refresh();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            bStart.Enabled = false;
            bStop.Enabled = true;
            if (startGame)
                StartGame();
            else
                ContinueGame();
        }


        private void bStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            bStart.Enabled = true;
            bStop.Enabled = false;
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
