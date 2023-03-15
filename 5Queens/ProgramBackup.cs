using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5Queens
{
    class Program
    {

        const int numberOfLines=8;
        static int[,] board = new int[numberOfLines, numberOfLines] 
                                            {{0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0},
                                             {0,0,0,0,0,0,0,0}};
        const int numberOfQueens = 5;
        static Int64 positionsProcessed = 0;
        static Int64 solutionsFound = 0;
        static string[] latestSolution = new string[numberOfQueens];

        static HashSet<string> solutions = new HashSet<string> { };

        static void Main(string[] args)
        {
            //numberOfQueens = 10;
            string[] queens = new string[numberOfQueens];
            //string[] queens = new string[numberOfQueens] { "A4", "B3", "C2", "D1", "G7" }; //Magnus Carlsen
            //string[] queens = new string[numberOfQueens] { "A1", "C7", "D3", "G8", "H4" }; // http://mathworld.wolfram.com/QueensProblem.html
            //string[] queens = new string[numberOfQueens] { "A6", "B7", "C8", "G1", "H2" }; // http://www.redhotpawn.com/board/showthread.php?threadid=57494
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Writeheader();
            NewSquareTestAllSquares(ref queens, stopWatch);
            stopWatch.Stop();
        }

        /// <summary>
        /// Writes the header
        /// </summary>
        static void Writeheader()
        {
            Console.Clear();
            Console.WriteLine("--------------------SOLUTIONS FOR THE FIVE QUEENS PROBLEM ---------------------");
            Console.WriteLine("--------------------           BY TYCHO NYSTRÖM           ---------------------");
            Console.WriteLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queens"></param>
        /// <param name="currentSquare"></param>
        /// s<returns></returns>
        static bool QueenOnCurrentSquare(ref string[] queens,string currentSquare)
        {
            for (int queen = 0; queen < numberOfQueens; queen++)
            {
                if (queens[queen].Equals(currentSquare))
                    return true;                
            }
            return false;
        }

        /// <summary>
        /// Checks if there are queens on the current line
        /// </summary>
        /// <param name="queens"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        static void QueensOnCurrentLine(string[] latestSolution, ref StringBuilder queensCurrentRow, int row)
        {
            queensCurrentRow.Clear();
            string rowString = row.ToString();
            for (int queen = 0; queen < numberOfQueens; queen++)
            {
                if (latestSolution[queen].Contains(rowString))
                {
                    queensCurrentRow.Append(latestSolution[queen].Substring(0, 1));
                }
            }
            return; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queens"></param>
        static void UpdateStatus(ref string[] queens, Stopwatch stopWatch)
        {
            positionsProcessed++;
            if ((positionsProcessed % 100000) == 0)
            {
                

                Console.Clear();
                for (int j = 0; j < 80; j++)
                    Console.Write("\b"); //Backspace
                Console.Write("Positions processed:{0} ",positionsProcessed);
                //Console.Write("Solutions:{0} ", solutionsFound);
                Console.Write("Solutions:{0} ", solutions.Count());


                if (latestSolution[0] == null)
                    return;
                Console.Write("Latest solution : ");
                for (int q = 0; q < numberOfQueens; q++)
                {
                    Console.Write("{0} ",latestSolution[q]);
                }
                ///////////////////
                //Write top

                StringBuilder sb = new StringBuilder(30);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("   A B C D E F G H ");
                Console.WriteLine("  ╔═╦═╦═╦═╦═╦═╦═╦═╗");
                StringBuilder queenStringBuilder = new StringBuilder(numberOfQueens);
                //string queenString;
                queenStringBuilder.Append ("     ");

                for (int printRow = 8; printRow >=1; printRow--)
                {
                    QueensOnCurrentLine(latestSolution, ref queenStringBuilder, printRow);
                    sb.Append(" ");
                    sb.Append(printRow);

                    //String will be like this 
                    //0123456789012345678
                    //   A B C D E F G H
                    // 8║█║ ║█║ ║█║ ║█║ ║
                    //
                    if((printRow % 2)==0)
                        sb.Append("║█║ ║█║ ║█║ ║█║ ║");
                    else
                        sb.Append("║ ║█║ ║█║ ║█║ ║█║");

                    for (int q = 0; q < queenStringBuilder.Length; q++)
                    {
                        int l = (int)queenStringBuilder[q] - 64;
                        int pos = 1 + (2 * l);
                        sb[pos] = 'Q';
                        
                    }
                    Console.WriteLine(sb);
                    if(printRow >1)
                        Console.WriteLine("  ╠═╬═╬═╬═╬═╬═╬═╬═╣");
                    else
                        Console.WriteLine("  ╚═╩═╩═╩═╩═╩═╩═╩═╝");

                    sb.Clear();
                    queenStringBuilder.Clear();
                }
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value. 
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("RunTime " + elapsedTime);

            }
            return;
        }

        /// <summary>
        /// Evaluates the position by marking all the squares covered by the queens in their current positions and if all squares
        /// on the board are covered (then we have found a solution)
        /// </summary>
        /// <param name="queens"></param>
        /// <returns></returns>
        static bool Evaluate(ref string[] queens, Stopwatch stopWatch)
        {
            int queenNr;
            int line = 0, row = 0;

            if (!LegalPosition(queens))                
            {
                return false;
            }
            for (queenNr = 0; queenNr < numberOfQueens; queenNr++)
            {
                UpdateStatus(ref queens,stopWatch);
                //begin with the lines

                string l = string.Copy(queens[queenNr]);
                int currentLine = (int)l[0] - 65;
                int currentRow = (int)l[1] - 49;
                int rowsAway = 0;

                board[currentLine, currentRow] = 1;
                for (row = 0; row < 8; row++)
                {
                    board[currentLine, row] = 1;
                }

                //Next come the rows
                for (line = 0; line < 8; line++)
                {
                    board[line, currentRow] = 1;
                }

                row = currentRow;
                //And now for the hardest part - the diagonals
                //First go left

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

            if (CheckIfAllSquaresCovered())
                PrintSolution(ref queens,stopWatch);
            return true;
        }


        /// <summary>
        /// Returns false if two or more queens are on the same square, otherwise true
        /// </summary>
        /// <param name="queens"></param>
        /// <returns></returns>
        private static bool LegalPosition(string[] queens)
        {
            int q0,q1,q2,q3,q4;
            
            q0 = 0;
            for (q1 = 1; q1 < numberOfQueens; q1++)
               if (queens[q0] == queens[q1])
                   return false;

            q1 = 1;
            for (q2 = 2; q2 < numberOfQueens; q2++)
               if (queens[q1] == queens[q2])
                   return false;

            q2 = 2;
            for (q3 = 3; q3 < numberOfQueens; q3++)
               if (queens[q2] == queens[q3])
                   return false;

            q3 = 3;
            for (q4 = 4; q4 < numberOfQueens; q4++)
               if (queens[q3] == queens[q4])
                     return false;


            return true;

        }

        /// <summary>
        /// Prints the solution
        /// </summary>
        /// <param name="queens"></param>
        private static void PrintSolution(ref string[] queens, Stopwatch stopWatch)
        {
            
            solutionsFound++;
            for (int q = 0; q < numberOfQueens; q++)
			{
                latestSolution[q] = queens[q];                
			}
            AddSolutionToHashSet(ref queens);
            UpdateStatus(ref queens,stopWatch);
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queens"></param>
        static void AddSolutionToHashSet(ref string[] queens)
        {
            string[] queensCopy = (string[])queens.Clone();
            Array.Sort(queensCopy);
            StringBuilder sb = new StringBuilder();
            for (int queen = 0; queen < numberOfQueens; queen++)
            {
                sb.Append(queensCopy[queen]);
            }

            string solution = sb.ToString();
            solutions.Add(solution);
            return;

        }

        /// <summary>
        /// Returns true if all squares by the five queen in their current position, otherwise false.
        /// </summary>
        /// <param name="queens"></param>
        /// <returns></returns>
        private static bool CheckIfAllSquaresCovered()
        {
            int line, row, coveredSquares=0;
            //Count covered squares
            for (line = 0; line < 8; line++)
            {
                for (row = 0; row < 8; row++)
                {
                    if (board[row, line] == 1)
                        coveredSquares++;
                }
            }
            return (coveredSquares == 64);
        }

        /// <summary>
        /// Tests all possible solutions
        /// </summary>
        /// <param name="queens"></param>
        /// <returns></returns>
        static bool NewSquareTestAllSquares(ref string[] queens, Stopwatch stopWatch)
        {
            
            for (int q0 = 0; q0 < 64; q0++)
            {
                queens[0] = NumberToSquare(q0);
                for (int q1 = 0; q1 < 64; q1++)
                {
                    queens[1] = NumberToSquare(q1);
                    for (int q2 = 0; q2 < 64; q2++)
                    {
                        queens[2] = NumberToSquare(q2);
                        for (int q3 = 0; q3 < 64; q3++)
                        {
                            queens[3] = NumberToSquare(q3);
                            for (int q4 = 0; q4 < 64; q4++)
                            {
                                queens[4] = NumberToSquare(q4);
                                Evaluate(ref queens,stopWatch);
                                ResetBoard();
                            }
                            
                        }
                    }
                    
                }
                
            }
            return true;
        }

        /// <summary>
        /// Converts a given number to a square on the board, 0=A1, 1=B1....8=A2....63=H8 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        static string NumberToSquare(int number)
        {
            int remainder = number % 8;
            remainder += 64;
            int line = (number / 8);
            string square = string.Empty;
            byte[] bb = new byte[2];
            bb[0] = (byte)(remainder + 1) ;
            bb[1] = (byte)(line + 49);

            // From byte array to string
            square = Encoding.UTF8.GetString(bb, 0, bb.Length);
            return square;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="number"></param>
        ///// <returns></returns>
        //static string NumberToSquareLinesReversed(int number)
        //{
        //    int remainder = 8- (number % 8);
        //    remainder += 64;
        //    int row = (number / 8);            
        //    string square = string.Empty;
        //    byte[] bb = new byte[2];
        //    bb[0] = (byte)(remainder);
        //    bb[1] = (byte)(row + 49);

        //    // From byte array to string
        //    square = Encoding.UTF8.GetString(bb, 0, bb.Length);
        //    return square;
        //}


        /// <summary>
        /// Resets all squares
        /// </summary>
        /// <param name="queens"></param>
        static void ResetBoard()
        {
            int line, row;
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
