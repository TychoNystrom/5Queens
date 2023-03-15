using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5Queens
{
    class Program
    {
        static int[,] board = new int[8, 8] {{0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0}};
        static int numberOfQueens = 5;

        static void Main(string[] args)
        {
            //string[] queens = new string[5] { "A1", "A2", "A3", "A4", "A5" };
            //string[] queens = new string[5] { "A1", "A2", "A3", "A4", "H8" };

            string[] queens = new string[5] { "A4", "B3", "C2", "D1", "G7" }; //Magnus Carlsen
            Evaluate(ref queens);
            ResetBoard(ref queens);
            while (NewSquare(ref queens))
            {
                Evaluate(ref queens);
                ResetBoard(ref queens);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queens"></param>
        /// <returns></returns>
        static bool Evaluate(ref string[] queens)
        {
            int queenNr;
            int line = 0, row = 0, coveredSquares = 0;
            for (queenNr = 0; queenNr < numberOfQueens; queenNr++)
            {

                //begin with the lines
                //string l = queens[0];
                              
                string l = string.Copy(queens[queenNr]);
                int currentLine = (int)l[0] - 65;
                int currentRow  = (int)l[1] - 49;
                int rowsAway = 0;

                Console.Write("TESTING POSITION : ");
                for (queenNr = 0; queenNr < numberOfQueens; queenNr++)
                    Console.Write(" {0} ", queens[queenNr]);
                Console.WriteLine();

                board[currentLine, currentRow] = 1;
                for (row = 0; row < 8; row++)
                {
                    board[currentLine,row] = 1;
                }

                //Next come the rows
                for (line = 0; line < 8; line++)
                {
                    board[line, currentRow] = 1;
                }

                row = currentRow;
                //And now for the hardest part - the diagonals
                //First go left

                //int difference = currentLine-0;
                for (line = currentLine; line >= 0; line--)
                {
                    if (currentRow - rowsAway >= 0)
                    {
                        board[line, currentRow - rowsAway] = 1; 
                    }

                    if (currentRow + rowsAway < 8)
                    {
                        board[line, currentRow + rowsAway] = 1;
                    }
                    rowsAway++;
                
                }

                //Then go right
                rowsAway = 0;
                for (line = currentLine; line < 8; line++)
                {
                    if (currentRow - rowsAway >= 0)
                    {
                        board[line, currentRow - rowsAway] = 1;
                    }

                    if (currentRow + rowsAway < 8)
                    {
                        board[line, currentRow + rowsAway] = 1;
                    }
                    rowsAway++;

                }
			}
           
            //Count covered squares
            for (line = 0;  line < 8; line++)
            {
                for (row = 0; row < 8; row++)
                {
                    if (board[row, line] == 1)
                        coveredSquares++;                    
                }
            }

            if (coveredSquares == 64)
            {
                Console.Write("SOLUTION FOUND : ");
                for (queenNr = 0; queenNr < numberOfQueens; queenNr++)
                    Console.Write(" {0} ",queens[queenNr]);
                Console.WriteLine();
            }
            return true;
        }

        /// <summary>
        /// Finds a new square for one of the queens
        /// </summary>
        /// <param name="queens"></param>
        static bool NewSquare(ref string[] queens)        
        {
            
            string[] queensCopy = new string[5] { "", "", "", "", "" };
            for (int j = 0; j <numberOfQueens; j++)
            {
                queensCopy[j] = queens[j];
            }

            for (int i = numberOfQueens-1 ; i >= 0; i--)
            {
                StringBuilder sb = new StringBuilder(queens[i]);
                if (String.Compare(queens[i],"H8") !=0 ) //If we did not reach the last square already - move this queen
                {
                    if (queens[i].Contains("8")) //If we are already on the last line then switch to the next line
                    {
                        sb.Replace("G", "H");
                        sb.Replace("F", "G");
                        sb.Replace("E", "F");
                        sb.Replace("D", "E");
                        sb.Replace("C", "D");
                        sb.Replace("B", "C");
                        sb.Replace("A", "B");
                        sb.Replace("8", "1");
                    }
                    else
                    {
                        sb.Replace("7", "8");
                        sb.Replace("6", "7");
                        sb.Replace("5", "6");
                        sb.Replace("4", "5");
                        sb.Replace("3", "4");
                        sb.Replace("2", "3");
                        sb.Replace("1", "2");
                    }
                }
                else
                { 
                }

                queens[i] = sb.ToString();
                if (string.Compare(queens[i], queensCopy[i]) != 0)
                    return true;
            }

            for (int k = 0; k < numberOfQueens; k++)
            {
                if (string.Compare(queens[k], queensCopy[k]) != 0)
                    return true;
            }

            return false;
            
        }

        /// <summary>
        /// Resets all squares
        /// </summary>
        /// <param name="queens"></param>
        static void ResetBoard(ref string[] queens)
        {
            int line, row;
            //Count covered squares
            for (line = 0;  line < 8; line++)
            {
                for (row = 0; row < 8; row++)
                {
                    board[row, line] = 0;                    
                }
            }
        }
    }
}
