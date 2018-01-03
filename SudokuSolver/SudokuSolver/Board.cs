using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    /// <summary>
    /// The static <c>Board</c> class is mainly responsible for containing and solving for the 81 <c>Cell</c> on A single board.
    /// Contains three methods.
    /// <list type="bullet">
    /// <item>
    /// <term>init</term>
    /// <description>initiializes sudoku board</description>
    /// </item>
    /// <item>
    /// <term>recurse</term>
    /// <description>The backtracking happens on this board</description>
    /// </item>
    /// <item>
    /// <term>display</term>
    /// <description>Display the sudoku</description>
    /// </item>
    /// </list>
    /// </summary>
    
    public static class Board
    {
        ///<summary>
        /// Contains 9x9 Cells which can be iterated.
        ///</summary>

        public static Cell[,] cells = new Cell[9, 9];

        ///<summary>
        /// The number of clues this sudoku puzzle contains. set during initialization. 
        ///</summary>

        public static int clues = 0;

        ///<summary>
        /// Initialize the sudoku board by parsing the puzzle text file and creating and initializing new instances of <c>Cell</c>. 
        ///</summary>
        ///<param name="filename">the name of the text file to be read</param>
        
        public static void init(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines("./" + filename);

            for (int i = 0; i < 9; i++)
            {
                lines[i] = lines[i].Replace(" ", "");

                for (int j = 0; j < 9; j++)
                {
                    cells[i, j] = new Cell(i, j, lines[i][j] - 48);
                }
            }

            //initialize cells
            foreach (Cell cell in cells)
            {
                cell.init();
            }
        }


        ///<summary>
        ///The function used for recursion. This function is mainly adapted from the original source code by Mitchell Johnson, the link is found in <see cref="Program"/>. 
        ///There are three tests done inside the function, the first test is end of board checking. The second is checking for existing clues/cells. The final step is empty cell validation.
        ///</summary>
        ///<param name="row">the row number to visit in the current step</param>
        ///<param name="column">the column number to visit in the current step</param>
        ///<returns>true if this recursion step is succesfull</returns>
        
        public static bool recurse(int row, int column)
        {
            int nextNumber = 1;

            //Last row has been visited, which means we have visited the entire board, exit recursion. 
            if (9 == row)
            {
                return true;
            }

            Cell cell = cells[row, column];

            //if this cell value is zero, we can proceed to validation, otherwise visit the neigbouring cells and recurse(another row or column).
            if (cell.value != 0)
            {
                if (column == 8)
                {
                    if (recurse(row + 1, 0)) return true;
                }
                else
                {
                    if (recurse(row, column + 1)) return true;
                }
                return false;
            }

            // test if on of the numbers 1 to 9 is valid on this cell, if so proceed to neighbouring cells and recurse(another row or column). 
            for (; nextNumber < 10; nextNumber++)
            {
                if (cell.validate(nextNumber))
                {
                    cell.value = nextNumber;
                    if (column == 8)
                    {
                        if (recurse(row + 1, 0)) return true;
                    }
                    else
                    {
                        if (recurse(row, column + 1)) return true;
                    }
                    cell.value = 0;
                }
            }
            return false;
        }

        ///<summary>
        /// Display the current sudoku board inside console. Called frequently by <see cref="Program.showProgress"/>.  
        ///</summary>
        ///<returns>The puzzle outputted as A string, similar to input format</returns>
        
        public static string display()
        {
            string[] stringPuzzle = new string[81];
            int count = 0;
            foreach (var cell in cells)
            {
                if (count % 9 != 0)
                {
                    stringPuzzle[count] = cell.value.ToString() + " ";
                }
                else
                {
                    stringPuzzle[count] = "\n" + cell.value.ToString() + " ";
                }
                count++;
            }

            string answer = string.Join("", stringPuzzle);
            return answer;
        }
    }
}
