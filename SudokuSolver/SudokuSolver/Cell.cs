using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{

    /// <summary>
    /// The <c>Cell</c> class is responsible for defining each of the the 81 square/cell instances.
    /// Contains two methods. One for initialization, one for validation of the sudoku rules.
    /// <list type="bullet">
    /// <item>
    /// <term>Cell</term>
    /// <description>the default constructor</description>
    /// </item>
    /// <item>
    /// <term>init</term>
    /// <description>Mainly used to set additional properties of A cell after creation, such as its neighbouring cells</description>
    /// </item>
    /// <item>
    /// <term>validate</term>
    /// <description>validates the number placed in this cell according to sudoku rules</description>
    /// </item>
    /// </list>
    /// </summary>
    public class Cell
    {
        ///<summary>
        /// The value or number of this <c>Cell</c>. Changes throughout the sudoku game and accessed by <see cref="Board"/>class.
        /// </summary>
        
        public int value;

        ///<summary>
        /// The row number this <c>Cell</c> lies in.
        /// </summary>
        
        int row;

        ///<summary>
        /// The column number this <c>Cell</c> lies in.
        /// </summary>
        
        int column;

        int modRow;
        int modCol;
        int row1;
        int row2;
        int col1;
        int col2;

        ///<summary>
        /// The neigbouring cells. Used as an optimisation.
        /// </summary>
        
        Cell UpperLeft;

        ///<summary>
        /// The neigbouring cells. Used as an optimisation.
        /// </summary>

        Cell UpperRight;

        ///<summary>
        /// The neigbouring cells. Used as an optimisation.
        /// </summary>

        Cell LowerLeft;

        ///<summary>
        /// The neigbouring cells. Used as an optimisation.
        /// </summary>

        Cell LowerRight;

        ///<summary>
        ///The default constructor.
        ///</summary>
        ///<param name="r">row number of this <c>Cell</c></param>
        ///<param name="c">column number of this <c>Cell</c></param>
        ///<param name="number">number placed on this <c>Cell</c></param>

        public Cell(int r, int c, int number)
        {
            this.value = number;
            this.row = r;
            this.column = c;
            this.modRow = 3 * (r / 3);
            this.modCol = 3 * (c / 3);
            this.row1 = (r + 2) % 3;
            this.row2 = (r + 4) % 3;
            this.col1 = (c + 2) % 3;
            this.col2 = (c + 4) % 3;
        }

        ///<summary>
        ///Each <c>Cell</c> is initialized and called by <see cref="Board"/>.
        ///</summary>

        public void init()
        {
            if (value != 0)
            {
                Board.clues++;
            }
            UpperLeft = Board.cells[row1 + modRow, col1 + modCol];
            UpperRight = Board.cells[row2 + modRow, col1 + modCol];
            LowerLeft = Board.cells[row1 + modRow, col2 + modCol];
            LowerRight = Board.cells[row2 + modRow, col2 + modCol];
        }

        ///<summary>
        ///validate each <c>Cell</c> using sudoku rules, also called via <see cref="Board"/>Board. 
        ///</summary>
        ///<param name="number">number to validate.</param>
        ///<returns>
        ///true if valid, false if not A valid number.
        ///</returns>
        
        public bool validate(int number)
        {
            int i = 0;

            //check rows and columns
            for (i = 0; i < 9; i++)
            {
                if (Board.cells[i, column].value == number) return false;
                if (Board.cells[row, i].value == number) return false;
            }

            /* Check the remaining four spaces of this cell */
            if (UpperLeft.value == number) return false;
            if (UpperRight.value == number) return false;
            if (LowerLeft.value == number) return false;
            if (LowerRight.value == number) return false;
            return true;
        }
    }
}
