using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Challenge_Nonogram
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] columnClues = new int[][]
            {
                new int[] {3,1 },
                new int[] {1,1,1 },
                new int[] {1,1,1 },
                new int[] {1,1,1 },
                new int[] {1,3 }
            };
            int[][] rowClues = new int[][]
            {
                new int[] {5 },
                new int[] {1 },
                new int[] {5 },
                new int[] {1 },
                new int[] {5 }
            };
            // int[][] rowClues = new int[][]
            // {
            //     new int[] {2 },
            //     new int[] {3 },
            //     new int[] {3,1 },
            //     new int[] {1,2},
            //     new int[] {1 }
            // };
            // int[][] columnClues = new int[][]
            // {
            //     new int[] {3 },
            //     new int[] {3 },
            //     new int[] {3 },
            //     new int[] {1 },
            //     new int[] {3 }
            // };
            // int[][] columnClues = new int[][]
            // {
            //     new int[] {3 },
            //     new int[] {2,2 },
            //     new int[] {1,1 },
            //     new int[] {2,2 },
            //     new int[] {3 }
            // };
            // int[][] rowClues = new int[][]
            // {
            //     new int[] {3},
            //     new int[] {2,2},
            //     new int[] {1,1},
            //     new int[] {2,2},
            //     new int[] {3}
            // };
            Board b = new Board(5, 5,rowClues,columnClues);
            // Console.WriteLine("\u25A0");
            Console.WriteLine(b);

            //get frequently used data values from Board object
            int[][] boardstate = b.getBoardstate();
            int numRows = b.getNumRows();
            int numCols = b.getNumCols();

            // //iterate through row clues looking for trivial rows
            // for (int i = 0; i < numRows; i++)
            // {
            //     //check for fully filled rows
            //     if (rowClues[i].Length == 1 && rowClues[i][0] == numCols)
            //     {
            //         for (int j = 0; j < numCols; j++)
            //         {
            //             boardstate[i][j] = 2;
            //         }
            //     }

            //     //check for trivial non-full rows
            //     else if ((rowClues[i].Sum() + rowClues[i].Length - 1) == numCols)
            //     {
            //         //run through each clue for the trivial row
            //         for (int k = 0; k < rowClues[i].Length; k++)
            //         {
            //             //fill cells
            //             for (int a = 0; a < rowClues[i][k]; a++)
            //             {
            //                 if (k == 0) boardstate[i][a] = 2;
            //                 else
            //                 {
            //                     //sum all previous clues and spaces
            //                     int index = a + rowClues[i].Take(k).Sum() + k;
            //                     boardstate[i][index] = 2;
            //                 }
            //             }
            //             //add spaces after each clue except the last one
            //             if (k != rowClues[i].Length - 1) boardstate[i][rowClues[i].Take(k+1).Sum() + k] = 1;
            //         }
            //     }
            // }
            // //update the boardstate
            // b.updateBoardstate(boardstate);
            b.fillFullRows();
            b.completeRows();
            Console.WriteLine(b);

            //iterate through column clues looking for trivial columns
            for (int i = 0; i < numCols; i++)
            {
                //check for fully filled rows
                if (columnClues[i].Length == 1 && columnClues[i][0] == numRows)
                {

                    for (int j = 0; j < numRows; j++)
                    {
                        boardstate[j][i] = 2;
                    }
                }

                //check for trivial non-full columns
                else if ((columnClues[i].Sum() + columnClues[i].Length - 1) == numRows)
                {
                    //run through each clue for the trivial column
                    for (int k = 0; k < columnClues[i].Length; k++)
                    {
                        //fill cells
                        for (int a = 0; a < columnClues[i][k]; a++)
                        {
                            if (k == 0) boardstate[a][i] = 2;
                            else
                            {
                                //sum all previous clues and spaces
                                int index = a + columnClues[i].Take(k).Sum() + k;
                                boardstate[index][i] = 2;
                            }
                        }
                        //add spaces after each clue except the last one
                        if (k != columnClues[i].Length - 1) boardstate[columnClues[i].Take(k + 1).Sum() + k][i] = 1;
                    }
                }
            }
            b.updateBoardstate(boardstate);
            Console.WriteLine(b);

            //check left edge for filled squares to continue
            for (int i = 0; i < numRows; i++)
            {
                if (boardstate[i][0] == 2 && rowClues[i][0] > 1)
                {
                    for (int k = 0; k < rowClues[i][0]; k++)
                    {
                        if ((k + 1) != numCols) {
                            if (k == rowClues[i][0] - 1) boardstate[i][k + 1] = 1;
                            else boardstate[i][k + 1] = 2;
                        }
                    }
                }
            }
            b.updateBoardstate(boardstate);
            Console.WriteLine(b);

            b.completeRows();
            Console.WriteLine(b);

            b.completeColumns();
            Console.WriteLine(b);

            b.fillTrivialRows();
            Console.WriteLine(b);

            b.fillTrivialColumns();
            Console.WriteLine(b);

            b.completeRows();
            b.completeColumns();
            Console.WriteLine(b);

            Console.WriteLine(b.isComplete());

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
