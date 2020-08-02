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
        int rows;
        int columns;
        bool[,] field;
        int[,] neighbors;
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
            set
            {
                rows = value;
            }
        }
        public int Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
            }
        }

        public Cells(int width,int height)
        {
            rows = height/5; 
            columns = width/5;
            field = new bool[rows, columns];
            neighbors = new int[rows, columns];
        }

        public void InitialField()
        {
            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    field[i, j] = random.Next(5) == 0;
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
                    if (field[row,column] && field[y,x] == field[row,column] && !self)
                        count++;
                }
            }
            return count;
        }
    }
}
