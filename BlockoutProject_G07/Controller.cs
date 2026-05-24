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
                view.ShowMenu();
                option = view.Input();

                // Determine the option specified by the user and act on it
                switch (option)
                {
                    case "1":
                        StartGame(view, board);
                        break;
                    case "2":
                        ChangeDifficulty(view, board);
                        break;
                    case "3":
                        view.ShowTutorial();
                        break;
                    case "0":
                        view.ExitMessage();
                        break;
                    default:
                        view.ErrorMessage("Unknown option!");
                        break;
                }

                // Wait for user to press a key...  
                if (option != "0")
                {
                    view.WaitUser();
                }

                // Loop keeps going until players choses to quit (option 0)
            } while (option != "1" && option != "0");
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
            int row, column;

            // Main game loop
            do
            {
                // Show board before asking inputs
                view.ShowBoard(board);

                // Shows game menu
                view.ShowGameMenu();
                option = view.Input();

                // Determine the option specified by the user and act on it
                switch (option)
                {
                    case "1":
                        // Get user coordinates while not having valid coordinates
                        do
                        {
                            (row, column) = view.AskCoordinates();
                            if (!board.IsValidCoord(row, column))
                            {
                                view.ErrorMessage("Invalid Coordinate!");
                            }
                        } while (!board.IsValidCoord(row, column));
                        break;
                    case "2":
                        view.ShowTutorial();
                        break;
                    case "0":
                        view.ExitMessage();
                        break;
                    default:
                        view.ErrorMessage("Unknown option!");
                        break;
                }

                // Wait for user to press a key...
                if (option != "0")
                {
                    view.WaitUser();
                }

                // Loop keeps going game is won, or player decides to quit
            } while (!gameWon && option != "0");
        }
        public void ChangeDifficulty(IView view, Board board)
        {
            view.ShowDifficultyMenu();
            string option = view.Input();
            switch (option)
                {
                    case "1":
                        Program.difficulty = Difficulty.Easy;
                        view.DifficultyMessage("Easy");
                        break;
                    case "2":
                        Program.difficulty = Difficulty.Medium;
                        view.DifficultyMessage("Medium");
                        break;
                    case "3":
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
        
    }
}