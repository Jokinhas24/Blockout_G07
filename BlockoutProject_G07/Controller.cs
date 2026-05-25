using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Pipelines;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlockoutProject_G07
{
    public class Controller
    {
        // Keep the difficulty here so it can be changed later set to Easy (Default)
        private Difficulty difficulty = Difficulty.Easy;
        // The board (part of the Model)
        private Board board;
        // stating the variable of the game state
        private bool gameWon;
        /// <summary>
        /// Controller's constructor
        /// </summary>
        /// <param name="board"> Current Board </param>
        public Controller()
        {
            // Create the board (part of the model)
            this.board = new Board (difficulty);
        }
        /// <summary>
        /// Starts the main menu loop
        /// </summary>
        /// <param name="view"> Current View </param>
        public void Run(IView view)
        {
            // Greet user
            view.WelcomeMessage();

            // We keep the user's option here
            string option;
            GameResult result = GameResult.Continue;
            difficulty = Difficulty.Easy;

            // Main menu loop
            while (true)
            {
                // If the player choose to end, end app
                if (result == GameResult.Exit)
                { 
                    view.ExitMessage();
                    break;
                }
                // If the player choose to restart, show menu
                if (result == GameResult.Restart)
                {
                    CreateBoard();
                    result = GameResult.Continue;
                    continue;
                }

                // Show menu and get user option
                option = view.ShowMenu();

                // Determine the option specified by the user and act on it
                switch (option)
                {
                    case "Play":
                        result = StartGame(view);
                        break;
                    case "Difficulty":
                        ChangeDifficulty(view);
                        break;
                    case "Tutorial":
                        view.ShowTutorial();
                        break;
                    case "Quit":
                        view.ExitMessage();
                        return;
                    default:
                        view.ErrorMessage("Unknown option!");
                        break;
                }

                // Wait for user to press a key...  
                if (option != "Play" && option != "Quit")
                {
                    view.WaitUser();
                }
                // Loop keeps going until players choses to starts the game or quit
            }
        }
        /// <summary>
        /// Game's main loop
        /// </summary>
        /// <param name="view"> Current view </param>
        public GameResult StartGame(IView view)
        {
            // Game's variables
            gameWon = false;
            string option;

            // Shuffles board before anything
            ShuffleBoard();

            // Main game loop
            while (true)
            {
                // Show board before asking inputs
                view.ShowBoard(board);

                // Checks board before asking for input
                CheckBoardWon();

                // If Game is won show winning message and
                // send the user back to main menu or end the app
                // while continuing if the game is not won nor quitted
                if (gameWon)
                {
                    return view.GameWinMessage() ? GameResult.Restart : GameResult.Exit;
                }
                else // Play the game while game isn't won
                {
                    // Shows game menu
                    option = view.ShowGameMenu();

                    // Determine the option specified by the user and act on it
                    switch (option)
                    {
                        case "Coordinates":
                            // Ask coordinates
                            (int row, int column) coordinates = view.AskCoordinates(board);
                            // Toggle tiles
                            ToggleTiles(board, coordinates.row, coordinates.column);
                            break;
                        case "Tutorial":
                            view.ShowTutorial();
                            break;
                            // Return to main menu with exit result to leave
                        case "Quit":
                            return GameResult.Exit;
                        default:
                            view.ErrorMessage("Unknown option!");
                            break;
                    }
                    // Wait for user to press a key...  
                    if (option != "Quit")
                    {
                        view.WaitUser();
                    }
                } // Loop keeps going game is won, or player decides to quit
            }
        }
        /// <summary>
        /// Changes the difficulty of the game, meaning the size of the board
        /// </summary>
        /// <param name="view"> Current View</param>
        public void ChangeDifficulty(IView view)
        {
            string option = view.ShowDifficultyMenu();
            switch (option)
                {
                    case "Easy":
                        difficulty = Difficulty.Easy;
                        CreateBoard();
                        break;
                    case "Medium":
                        difficulty = Difficulty.Medium;
                        CreateBoard();
                        break;
                    case "Hard":
                        difficulty = Difficulty.Hard;
                        CreateBoard();
                        break;
                    default:
                        view.ErrorMessage("Invalid Difficulty!");
                        break;
                }
            
        }
        /// <summary>
        /// Checks if the board is clear and changes game state to "Won" = true
        /// </summary>
        public void CheckBoardWon()
        {
            //Number of tiles cleared
            int n = 0;
            //Checks all tiles and adds to tiles cleared
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (!board.GetTile(i, j).GetState())
                    {
                        n++;
                    }
                }
            }
            //Changes game state "Won" to true when all Tiles are cleared
            if (n == board.Size * board.Size)
            {
                gameWon = true;
            }
        }
        public void ToggleTiles(Board board, int row, int column)
        {
            // Toggle chosen
            board.GetTile(row, column).ToggleState();
            // Toggle adjacent tiles (if valid)
            if (board.IsValidCoord(row - 1, column)) {board.GetTile(row - 1, column).ToggleState();}
            if (board.IsValidCoord(row + 1, column)) {board.GetTile(row + 1, column).ToggleState();}
            if (board.IsValidCoord(row, column - 1)) {board.GetTile(row, column - 1).ToggleState();}
            if (board.IsValidCoord(row, column + 1)) {board.GetTile(row, column + 1).ToggleState();}
        }
        public void CreateBoard()
        {
            board = new Board(difficulty);
        }
        public void ShuffleBoard()
        {
            Random random = new Random();

            // Variable of the amount of toggles
            int count = 0;

            while (count < board.Size)
            {
                // Get random coordinates
                int row = random.Next(0, board.Size);
                int column = random.Next(0, board.Size);

                // Check if the tile is already toggled (already true)
                if (!board.GetTile(row, column).GetState())
                {
                    board.GetTile(row, column).ToggleState();
                    count++;
                }
            }
        }
    }
}