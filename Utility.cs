using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Challenge_Nonogram
{
    class Utility
    {
        public static int maxColumnCount(int[][] arr)
        {
            int count = 0;
            foreach (int[] row in arr)
            {
                if (row.Length > count) count = row.Length;
            }
            return count;
        }
        public static int countFilledRow(int[] row)
        {
            int count = 0;
            foreach (int element in row)
            {
                if (element == 2) count++;
            }
            return count;
        }
        public static int countFilledCol(int[][] boardstate,int colInd)
        {
            int count = 0;
            for (int i = 0; i < boardstate.Length; i++)
            {
                if (boardstate[i][colInd] == 2) count++;
            }
            return count;
        }
        public static int countUnknownRow(int[] row)
        {
            int count = 0;
            foreach (int element in row)
            {
                if (element == 0) count++;
            }
            return count;
        }
        public static int countUnknownCol(int[][] boardstate, int colInd)
        {
            int count = 0;
            for (int i = 0; i < boardstate.Length; i++)
            {
                if (boardstate[i][colInd] == 0) count++;
            }
            return count;
        }
        public static int[][] fillTrivialRows(int[][] boardstate, int[][] rowClues)
        {
            //check rows for newly revealed trivial rows
            for (int i = 0; i < boardstate.Length; i++)
            {
                if ((rowClues[i].Sum() - Utility.countFilledRow(boardstate[i])) == Utility.countUnknownRow(boardstate[i]))
                {
                    for (int j = 0; j < boardstate[0].Length; j++)
                    {
                        if (boardstate[i][j] == 0) boardstate[i][j] = 2;
                    }
                }
            }
            return boardstate;

        }
        public static int[][] fillTrivialColumns(int[][] boardstate, int[][] columnClues)
        {
            for (int j = 0; j < boardstate[0].Length; j++)
            {
                if((columnClues[j].Sum() - Utility.countFilledCol(boardstate,j)) == Utility.countUnknownCol(boardstate, j))
                {
                    for (int i = 0; i < boardstate.Length; i++)
                    {
                        if (boardstate[i][j] == 0) boardstate[i][j] = 2;
                    }
                }
            }
            return boardstate;
        }
        public static int[][] completeRow(int[][] boardstate, int[][] rowClues)
        {
            //complete finished rows by marking remaining cells as empty
            for (int i = 0; i < boardstate.Length; i++)
            {
                if (rowClues[i].Sum() == Utility.countFilledRow(boardstate[i]))
                {
                    for (int j = 0; j < boardstate[0].Length; j++)
                    {
                        if (boardstate[i][j] == 0) boardstate[i][j] = 1;
                    }
                }
            }
            return boardstate;

        }
        public static int[][] completeColumn(int[][] boardstate, int[][] columnClues)
        {
            //complete finished columns by marking remaining cells as empty
            for (int i = 0; i < boardstate[0].Length; i++)
            {
                if (columnClues[i].Sum() == Utility.countFilledCol(boardstate, i))
                {
                    for (int j = 0; j < boardstate.Length; j++)
                    {
                        if (boardstate[j][i] == 0) boardstate[j][i] = 1;
                    }
                }
            }
            return boardstate;
        }
        public static bool checkComplete(int[][] boardstate)
        {
            foreach (int[] row in boardstate)
            {
                foreach (int element in row)
                {
                    if (element == 0) return false;
                }
            }
            return true;
        }
    }
}
