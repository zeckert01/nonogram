using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Challenge_Nonogram
{
    /// <summary>
    /// Nonogram board class. Keeps track of boardstate, clues, and prints current boardstate (for debugging etc.)
    /// </summary>
    public class Board
    {
        int rows;
        int cols;
        int[][] rowClues;
        int[][] colClues;
        int[][] boardstate;

        public Board(int rows, int cols)
        {
            //initialize rows, columns, and clue arrays
            this.rows = rows;
            this.cols = cols;
            this.rowClues = new int[rows][];
            this.colClues = new int[cols][];

            //initialize boardstate with 0's
            this.boardstate = new int[rows][];
            for (int i = 0; i < this.boardstate.Length; i++)
            {
                this.boardstate[i] = new int[cols];
            }
        }
        public Board(int rows, int cols, int[][] rowClues, int[][] colClues)
        {
            //initialize rows, columns, and clue arrays
            this.rows = rows;
            this.cols = cols;
            this.rowClues = new int[rows][];
            this.colClues = new int[cols][];

            //initialize boardstate with 0's
            this.boardstate = new int[rows][];
            for (int i = 0; i < this.boardstate.Length; i++)
            {
                this.boardstate[i] = new int[cols];
            }

            //set clue arrays with given clues
            this.setRowClues(rowClues);
            this.setColClues(colClues);
        }
        public void setRowClues(int[][] rowClues)
        {
            rowClues.CopyTo(this.rowClues, 0);
        }
        public void setColClues(int[][] colClues)
        {
            colClues.CopyTo(this.colClues, 0);
        }
        public int[][] getBoardstate()
        {
            return this.boardstate;
        }
        public int[][] getRowClues()
        {
            return this.rowClues;
        }
        public int[][] getColClues()
        {
            return this.colClues;
        }
        public int getNumRows()
        {
            return this.rows;
        }
        public int getNumCols()
        {
            return this.cols;
        }
        public void updateBoardstate(int[][] newBoardstate)
        {
            this.boardstate = newBoardstate;
        }
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Utility.maxColumnCount(this.colClues); i++)
            {
                for (int k = 0; k < Utility.maxColumnCount(this.rowClues); k++)
                {
                    output += "| ";
                }
                for (int j = 0; j < this.boardstate.Length; j++)
                {
                    output += "|";
                    if (this.colClues[j].Length > i) output += this.colClues[j][i];
                    else output += " ";
                }
                output += "|\n";
            }
            for (int i = 0; i < this.boardstate.Length; i++)
            {
                int[] row = this.boardstate[i];
                for (int j = 0; j < Utility.maxColumnCount(this.rowClues); j++)
                {
                    output += "|";
                    if (this.rowClues[i].Length > j) output += this.rowClues[i][j];
                    else output += " ";
                }
                foreach (int element in row)
                {
                    output += "|";
                    if (element == 0)
                        output += "?";
                    else if (element == 1)
                        output += " ";
                    else if (element == 2)
                        output += "\u25A0";
                    else
                    {
                        Console.WriteLine("Boardstate Corrupt");
                        return "Corrupt";
                    }
                }
                output += "|\n";
            }
            return output;
        }
        public bool isComplete()
        {
            foreach (int[] row in this.boardstate)
            {
                foreach (int element in row)
                {
                    if (element == 0) return false;
                }
            }
            return true;
        }
        public int countFilledCol(int colInd)
        {
            int count = 0;
            for (int i_row = 0; i_row < this.rows; i_row++)
            {
                if (this.boardstate[i_row][colInd] == 2) count++;
            }
            return count;
        }
        public int countFilledRow(int rowInd)
        {
            int count = 0;
            foreach (int element in this.boardstate[rowInd])
            {
                if (element == 2) count++;
            }
            return count;
        }
        public int countUnknownRow(int rowInd)
        {
            int count = 0;
            foreach (int element in this.boardstate[rowInd])
            {
                if (element == 0) count++;
            }
            return count;
        }
        public int countUnknownCol(int colInd)
        {
            int count = 0;
            for (int i_row = 0; i_row < this.rows; i_row++)
            {
                if (this.boardstate[i_row][colInd] == 0) count++;
            }
            return count;
        }
        public void fillTrivialRows()
        {
            for (int i_row = 0; i_row < this.rows; i_row++)
            {
                if ((this.rowClues[i_row].Sum() - this.countFilledRow(i_row)) == this.countUnknownRow(i_row))
                {
                    for (int j_col = 0; j_col < this.cols; j_col++)
                    {
                        if (this.boardstate[i_row][j_col] == 0) this.boardstate[i_row][j_col] = 2;
                    }
                }
            }
        }
        public void fillTrivialColumns()
        {
            for (int j_col = 0; j_col < this.cols; j_col++)
            {
                if ((this.colClues[j_col].Sum() - this.countFilledCol(j_col)) == this.countUnknownCol(j_col))
                {
                    for (int i_row = 0; i_row < this.rows; i_row++)
                    {
                        if (this.boardstate[i_row][j_col] == 0) this.boardstate[i_row][j_col] = 2;
                    }
                }
            }
        }
        public void completeRows()
        {
            for (int i_row = 0; i_row < this.rows; i_row++)
            {
                if (this.rowClues[i_row].Sum() == this.countFilledRow(i_row))
                {
                    for (int j_col = 0; j_col < this.cols; j_col++)
                    {
                        if (this.boardstate[i_row][j_col] == 0) this.boardstate[i_row][j_col] = 1;
                    }
                }
            }
        }
        public void completeColumns()
        {
            for (int i_col = 0; i_col < this.cols; i_col++)
            {
                if (this.colClues[i_col].Sum() == this.countFilledCol(i_col))
                {
                    for (int j_row = 0; j_row < this.rows; j_row++)
                    {
                        if (boardstate[j_row][i_col] == -1) boardstate[j_row][i_col] = 1;
                    }
                }
            }
        }
        public void fillFullRows()
        {
            for (int i_row = 0; i_row < this.rows; i_row++)
            {
                if ((this.rowClues[i_row].Sum() + this.rowClues[i_row].Length - 1) == this.cols)
                {
                    for (int clue = 0; clue < this.rowClues[i_row].Length; clue++)
                    {
                        for (int cell = 0; cell < this.rowClues[i_row][clue]; cell++)
                        {
                            if (clue == 0) this.boardstate[i_row][cell] = 2;
                            else
                            {
                                int index = cell + this.rowClues[i_row].Take(clue).Sum() + clue;
                                this.boardstate[i_row][index] = 2;
                            }
                        }
                        if (clue != this.rowClues[i_row].Length - 1)
                        {
                            this.boardstate[i_row][this.rowClues[i_row].Take(clue + 1).Sum() + clue] = 1;
                        }
                    }
                }
            }
        }

    }
}