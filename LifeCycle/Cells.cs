using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeCycle
{
    class Cells
    {
        private int rows;
        private int columns;
        private int size;
        private bool[,] field;

        public bool[,] Field
        {
            get
            {
                return field;
            }
            set
            {
                field = value;
            }
        }
        public int Rows
        {
            get
            {
                return rows;
            }
        }
        public int Columns
        {
            get
            {
                return columns;
            }
        }

        public int Size { 
            get 
            { 
                return size; 
            }
        }

        public Cells(int width, int height, int size)
        {
            this.size = size;
            rows = height / size;
            columns = width / size;
            field = new bool[rows, columns];
        }

        public Cells(int width,int height)
        {
            size = 10;
            rows = height/size; 
            columns = width/size;
            field = new bool[rows, columns];
        }

        public void InitialField()
        {
            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    field[i, j] = random.Next(7) == 0;
                }
            }
        }

        public int CountNeighbors(int x,int y)
        {
            int count=0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int column = (x + j + columns) % columns;
                    int row = (y + i + rows) % rows;
                    bool self = column == x && row == y;
                    if (field[row,column] && !self)
                        count++;
                }
            }
            return count;
        }
    }
}
