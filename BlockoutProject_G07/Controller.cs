using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace BlockoutProject_G07
{
    public class Controller
    {
        // The board (part of the Model)
        private readonly Board board;
        // stating the variable of the game state
        private bool gameWon;
        /// <summary>
        /// Controller's constructor
        /// </summary>
        /// <param name="board"> Current Board </param>
        public Controller(Board board)
        {
            // Keep the board (part of the model)
            this.board = board;
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

            // Main menu loop
            do
            {
                // Show menu and get user option
                option = view.ShowMenu();

                // Determine the option specified by the user and act on it
                switch (option)
                {
                    case "Play":
                        StartGame(view, board);
                        break;
                    case "Difficulty":
                        ChangeDifficulty(view, board);
                        break;
                    case "Tutorial":
                        view.ShowTutorial();
                        break;
                    case "Quit":
                        view.ExitMessage();
                        break;
                    default:
                        view.ErrorMessage("Unknown option!");
                        break;
                }

                // Wait for user to press a key...  
                if (option != "Quit")
                {
                    view.WaitUser();
                }

                // Loop keeps going until players choses to starts the game or quit
            } while (option != "Play" && option != "Quit");
        }
        /// <summary>
        /// Game's main loop
        /// </summary>
        /// <param name="view"> Current view </param>
        /// <param name="board"> Current board </param>
        public void StartGame(IView view, Board board)
        {
            // Game's variables
            gameWon = false;
            string option;

            // Main game loop
            do
            {
                // Show board before asking inputs
                view.ShowBoard(board);

                // Shows game menu
                option = view.ShowGameMenu();

                // Determine the option specified by the user and act on it
                switch (option)
                {
                    case "Coordinates":
                        // Ask coordinates
                        (int row, int column) coordinates = view.AskCoordinates(board);
                        // Toggle tiles
                        ToggleTiles(board, coordinates.Item1, coordinates.Item2);
                        break;
                    case "Tutorial":
                        view.ShowTutorial();
                        break;
                    case "Quit":
                        view.ExitMessage();
                        break;
                    default:
                        view.ErrorMessage("Unknown option!");
                        break;
                }

                // Wait for user to press a key...
                if (option != "Quit")
                {
                    view.WaitUser();
                }

                // Loop keeps going game is won, or player decides to quit
            } while (!gameWon && option != "Quit");
        }
        public void ChangeDifficulty(IView view, Board board)
        {
            string option = view.ShowDifficultyMenu();
            switch (option)
                {
                    case "Easy":
                        Program.difficulty = Difficulty.Easy;
                        view.DifficultyMessage("Easy");
                        break;
                    case "Medium":
                        Program.difficulty = Difficulty.Medium;
                        view.DifficultyMessage("Medium");
                        break;
                    case "Hard":
                        Program.difficulty = Difficulty.Hard;
                        view.DifficultyMessage("Hard");
                        break;
                    default:
                        view.ErrorMessage("Invalid Difficulty!");
                        break;
                }
            
        }
        /// <summary>
        /// Checks if the board is clear and changes game state to "Won" = true
        /// </summary>
        /// <param name="board"> Board to check </param>
        public void CheckBoardWon(Board board)
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
            // Toggle the chosen tile
            board.GetTile(row, column).ToggleState();
            // Toggle adjacent tiles
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = column - 1; j <= column + 1; j++)
                {
                    if (board.IsValidCoord(i, j))
                    {
                        board.GetTile(i, j).ToggleState();
                    }
                }
            }
        }
    }
}