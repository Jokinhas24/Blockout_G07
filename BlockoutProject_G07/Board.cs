using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockoutProject_G07
{
    public class Board
    {
        // Defining board's variables
        public Tile[,] board;
        public int Size => board.GetLength(0);
        /// <summary>
        /// Board's constructor
        /// </summary>
        /// <param name="difficulty"> Difficulty, which defines board's size (int x int) </param>
        public Board (Difficulty difficulty)
        {
            int size = (int)difficulty;
            board = new Tile[size, size];
        }
        /// <summary>
        /// Returns Tile's position based on coordinates
        /// </summary>
        /// <param name="row"> Tile's row </param>
        /// <param name="column"> Tile's Column </param>
        /// <returns></returns>
        public Tile GetTile(int row, int column)
        {
            return board[row, column];
        }
        /// <summary>
        /// Check if the coordinates given are valid
        /// </summary>
        /// <param name="row"> Coordinate's row </param>
        /// <param name="column"> Coordinate's column </param>
        /// <returns></returns>
        public bool IsValidCoord(int row, int column)
        {
            return row >= 0 && row < Size && column >= 0 && column < Size;
        }
    }
}