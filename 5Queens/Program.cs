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
        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            const int numberOfLines = 8;
            int numberOfQueens = 5;
            //numberOfQueens = 10;
            string[] queens = new string[numberOfQueens];
            string[] latestSolution = new string[numberOfQueens];    
            int[,] board = new int[numberOfLines, numberOfLines] 
                                                {{0,0,0,0,0,0,0,0},
                                                 {0,0,0,0,0,0,0,0},
                                                 {0,0,0,0,0,0,0,0},
                                                 {0,0,0,0,0,0,0,0},
                                                 {0,0,0,0,0,0,0,0},
                                                 {0,0,0,0,0,0,0,0},
                                                 {0,0,0,0,0,0,0,0},
                                                 {0,0,0,0,0,0,0,0}};
            Int64 positionsProcessed = 0;
            Int64 solutionsFound = 0;
            HashSet<string> solutions = new HashSet<string> { };
            //string[] queens = new string[numberOfQueens] { "A4", "B3", "C2", "D1", "G7" }; //Magnus Carlsen
            //string[] queens = new string[numberOfQueens] { "A1", "C7", "D3", "G8", "H4" }; // http://mathworld.wolfram.com/QueensProblem.html
            //string[] queens = new string[numberOfQueens] { "A6", "B7", "C8", "G1", "H2" }; // http://www.redhotpawn.com/board/showthread.php?threadid=57494
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Writeheader();
            NewSquareTestAllSquares(ref board,ref queens, numberOfQueens, numberOfLines,ref latestSolution, ref positionsProcessed,ref solutionsFound,ref solutions,stopWatch);
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
        /// Checks if there is a queen on the current square. NOT USED
        /// </summary>
        /// <param name="queens"></param>
        /// <param name="numberOfQueens"></param>
        /// <param name="currentSquare"></param>
        /// <returns></returns>
        static bool QueenOnCurrentSquare(ref string[] queens,int numberOfQueens,string currentSquare)
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
        /// <param name="latestSolution"></param>
        /// <param name="numberOfQueens"></param>
        /// <param name="queensCurrentRow"></param>
        /// <param name="row"></param>
        static void QueensOnCurrentLine(ref string[] latestSolution, int numberOfQueens, ref StringBuilder queensCurrentRow, int row)
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
        /// Decides whether to update information on the screen (positions processed,solutions found, latest solution)
        /// </summary>
        /// <param name="queens"></param>
        /// <param name="numberOfQueens"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="latestSolution"></param>
        /// <param name="positionsProcessed"></param>
        /// <param name="solutions"></param>
        /// <param name="stopWatch"></param>
        static void UpdateStatus(ref string[] queens, int numberOfQueens, int numberOfLines, ref string[] latestSolution,ref Int64 positionsProcessed,ref HashSet<string> solutions,Stopwatch stopWatch)
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

                for (int printRow = numberOfLines; printRow >= 1; printRow--)
                {
                    QueensOnCurrentLine(ref latestSolution, numberOfQueens, ref queenStringBuilder, printRow);
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
        /// 
        /// </summary>
        /// <param name="queens"></param>
        /// <param name="solutions"></param>
        /// <returns></returns>
        static bool CheckIfMirroredSolutionsExists(ref string[] queens, ref HashSet<string> solutions, int numberOfQueens,int numberOfLines)
        {
            string[] queensCopy = (string[])queens.Clone();
            
            for (int queen = 0; queen < numberOfQueens; queen++)
            {
                string row;
                row = queens[queen].Substring(1,1);
                string l = queens[queen].Substring(0,1);
                char c = l[0];
                //int b = (int)((char)(queens[queen].Substring(2,1)));
                int a = (int)c;

                int remainder = numberOfLines - (a % numberOfLines);
                //int remainder = 8 - (a % 8);
                remainder += 65;
                char newLine = (char)remainder;
                //int line = (number / numberOfLines);
                string newsquare = newLine + row;
            }
            Array.Sort(queensCopy);
            return false;
        }

        /// <summary>
        /// Evaluates the position by marking all the squares covered by the queens in their current positions and if all squares
        /// on the board are covered (then we have found a solution)
        /// </summary>
        /// <param name="queens"></param>
        /// <returns></returns>
        static bool Evaluate(int[,] board,ref string[] queens, int numberOfQueens, int numberOfLines, ref string[] latestSolution, ref Int64 positionsProcessed, ref Int64 solutionsFound,ref HashSet<string> solutions,Stopwatch stopWatch)
        {
            int queenNr;
            int line = 0, row = 0;

            if (!LegalPosition(queens,numberOfQueens))                
            {
                return false;
            }

            if(CheckIfMirroredSolutionsExists(ref queens,ref solutions,numberOfQueens,numberOfLines))
                return false;

            for (queenNr = 0; queenNr < numberOfQueens; queenNr++)
            {
                UpdateStatus(ref queens,numberOfQueens,numberOfLines,ref latestSolution,ref positionsProcessed,ref solutions,stopWatch);
                //begin with the lines

                string l = string.Copy(queens[queenNr]);
                int currentLine = (int)l[0] - 65;
                int currentRow = (int)l[1] - 49;
                int rowsAway = 0;

                board[currentLine, currentRow] = 1;
                for (row = 0; row < numberOfLines; row++)
                {
                    board[currentLine, row] = 1;
                }

                //Next come the rows
                for (line = 0; line < numberOfLines; line++)
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

                    if (currentRow + rowsAway < numberOfLines)
                    {
                        board[line, currentRow + rowsAway] = 1;
                    }
                    rowsAway++;

                }

                //Then go right
                rowsAway = 0;
                for (line = currentLine; line < numberOfLines; line++)
                {
                    if (currentRow - rowsAway >= 0)
                    {
                        board[line, currentRow - rowsAway] = 1;
                    }

                    if (currentRow + rowsAway < numberOfLines)
                    {
                        board[line, currentRow + rowsAway] = 1;
                    }
                    rowsAway++;

                }
            }

            if (CheckIfAllSquaresCovered(board,numberOfLines))
                PrintSolution(ref queens,numberOfQueens,numberOfLines,ref latestSolution,ref positionsProcessed,ref solutionsFound,solutions,stopWatch);
            return true;
        }


        /// <summary>
        /// Returns false if two or more queens are on the same square, otherwise true
        /// </summary>
        /// <param name="queens"></param>
        /// <returns></returns>
        private static bool LegalPosition(string[] queens, int numberOfQueens)
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
        private static void PrintSolution(ref string[] queens, int numberOfQueens, int numberOfLines, ref string[] latestSolution,ref Int64 positionsProcessed,ref Int64 solutionsFound,HashSet<string> solutions,Stopwatch stopWatch)
        {
            
            solutionsFound++;
            for (int q = 0; q < numberOfQueens; q++)
			{
                latestSolution[q] = queens[q];                
			}
            AddSolutionToHashSet(ref queens,numberOfQueens,ref solutions);
            UpdateStatus(ref queens,numberOfQueens,numberOfLines, ref latestSolution,ref positionsProcessed,ref solutions,stopWatch);
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queens"></param>
        static void AddSolutionToHashSet(ref string[] queens, int numberOfQueens, ref HashSet<string> solutions)
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
        private static bool CheckIfAllSquaresCovered(int[,] board,int numberOfLines)
        {
            int line, row, coveredSquares=0;
            //Count covered squares
            for (line = 0; line < numberOfLines; line++)
            {
                for (row = 0; row < numberOfLines; row++)
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
        static bool NewSquareTestAllSquares(ref int[,] board,ref string[] queens, int numberOfQueens, int numberOfLines,ref string[] latestSolution,ref Int64 positionsProcessed,ref Int64 solutionsFound,ref HashSet<string> solutions,Stopwatch stopWatch)
        {
            
            for (int q0 = 0; q0 < 64; q0++)
            {
                queens[0] = NumberToSquare(q0,numberOfLines);
                for (int q1 = 0; q1 < 64; q1++)
                {
                    queens[1] = NumberToSquare(q1,numberOfLines);
                    for (int q2 = 0; q2 < 64; q2++)
                    {
                        queens[2] = NumberToSquare(q2,numberOfLines);
                        for (int q3 = 0; q3 < 64; q3++)
                        {
                            queens[3] = NumberToSquare(q3,numberOfLines);
                            for (int q4 = 0; q4 < 64; q4++)
                            {
                                queens[4] = NumberToSquare(q4,numberOfLines);
                                Evaluate(board,ref queens,numberOfQueens,numberOfLines,ref latestSolution,ref positionsProcessed,ref solutionsFound,ref solutions,stopWatch);
                                ResetBoard(ref board,numberOfLines);
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
        static string NumberToSquare(int number, int numberOfLines)
        {
            int remainder = number % numberOfLines;
            remainder += 64;
            int line = (number / numberOfLines);
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
        static void ResetBoard(ref int[,] board, int numberOfLines)
        {
            int line, row;
            for (line = 0; line < numberOfLines; line++)
            {
                for (row = 0; row < numberOfLines; row++)
                {
                    board[row, line] = 0;                    
                }
            }
        }
    }
}
