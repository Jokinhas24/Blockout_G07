using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockoutProject_G07
{
    public class Board
    {
        // Defining board's variables
        public Tile[,] tiles;
        public int Size {get;}
        /// <summary>
        /// Board's constructor
        /// </summary>
        /// <param name="difficulty"> Difficulty, which defines board's size (int x int) </param>
        public Board (Difficulty difficulty)
        {
            // Turning difficulty into size
            Size = (int)difficulty;
            // Creating tiles
            tiles = new Tile[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    tiles[i, j] = new Tile(); // Default state
                }   
        }
        }
        /// <summary>
        /// Returns Tile's position based on coordinates
        /// </summary>
        /// <param name="row"> Tile's row </param>
        /// <param name="column"> Tile's Column </param>
        /// <returns></returns>
        public Tile GetTile(int row, int column)
        {
            return tiles[row, column];
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