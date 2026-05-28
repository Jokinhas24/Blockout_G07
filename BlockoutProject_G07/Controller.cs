using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlockoutProject_G07
{
    public class Controller
    {
        // Keep the difficulty here so it can be changed later, set to Easy (Default)
        private Difficulty difficulty = Difficulty.Easy;
        // The board (part of the Model)
        private Board board;
        // Stating the variable of the game state (Won or not)
        private bool gameWon;
        // Current highScore
        private int highScore;
        /// <summary>
        /// Controller's constructor
        /// </summary>
        public Controller()
        {
            // Create the board (part of the model)
            board = new Board (difficulty);
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
            // Game result set to continue, so it continues this loop
            GameResult result = GameResult.Continue;
            // Main menu loop (Loop keeps going until players choses quit)
            while (true)
            {
                // If the player choose to end after winning, end app
                if (result == GameResult.Exit)
                { 
                    view.ExitMessage();
                    break;
                }
                // If the player choose to restart after winning, show menu again
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
                        // return so it's stops the loop
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
            }
        }
        /// <summary>
        /// Game's main loop
        /// </summary>
        /// <param name="view"> Current view </param>
        private GameResult StartGame(IView view)
        {
            // Game's variables (set game state to false)
            gameWon = false;
            string option;
            // Variable of the amount of moves performed
            int moves = 0;
            // Shuffles board before anything (outside loop)
            ShuffleBoard();
            // Gets current highScore with the chosen difficulty before starting the game
            try
            {
                highScore = GetHighScore(difficulty);
            } catch {highScore = 0;}

            // Main game loop
            while (true)
            {
                // Show and checks board before asking input
                view.ShowBoard(board);
                CheckBoardWon();
                // If Game is won show winning message and
                // send the user back to main menu or end the app
                if (gameWon)
                {
                    // Saves highScore in file before doing anything else
                    SaveHighScore(difficulty, moves);
                    return view.GameWinMessage(moves) ? GameResult.Restart : GameResult.Exit;
                }
                else // Continuing while the game is not won nor quitted
                {
                    // Shows game menu
                    option = view.ShowGameMenu(difficulty, highScore);
                    // Determine the option specified by the user and act on it
                    switch (option)
                    {
                        case "Coordinates":
                            // Ask coordinates
                            (int row, int column) coordinates = view.AskCoordinates(board);
                            // Toggle tiles
                            ToggleTiles(coordinates.row, coordinates.column);
                            // Add moves
                            moves++;
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
                }
            }
        }
        /// <summary>
        /// Changes the difficulty of the game, changing the size of the board
        /// </summary>
        /// <param name="view"> Current View </param>
        private void ChangeDifficulty(IView view)
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
        private void CheckBoardWon()
        {
            //Number of tiles cleared
            int n = 0;
            //Checks all tiles and adds to tiles cleared (if cleared)
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
            //Changes game state "Won" to true when all Tiles (size x size) are cleared
            if (n == board.Size * board.Size)
            {
                gameWon = true;
            }
        }
        /// <summary>
        /// Toggles the center tiles and all adjacent to it (in a cross '+')
        /// </summary>
        /// <param name="row"> Center tile's row </param>
        /// <param name="column"> Center tile's column </param>
        private void ToggleTiles(int row, int column)
        {
            // Toggle chosen
            board.GetTile(row, column).ToggleState();
            // Toggle adjacent tiles and diagonals (if valid, important because they could be inexistent)
            // 4 Directions
            if (board.IsValidCoord(row - 1, column)) {board.GetTile(row - 1, column).ToggleState();}
            if (board.IsValidCoord(row + 1, column)) {board.GetTile(row + 1, column).ToggleState();}
            if (board.IsValidCoord(row, column - 1)) {board.GetTile(row, column - 1).ToggleState();}
            if (board.IsValidCoord(row, column + 1)) {board.GetTile(row, column + 1).ToggleState();}
            // 4 Diagonals
            if (board.IsValidCoord(row - 1, column - 1)) {board.GetTile(row - 1, column - 1).ToggleState();}
            if (board.IsValidCoord(row + 1, column - 1)) {board.GetTile(row + 1, column - 1).ToggleState();}
            if (board.IsValidCoord(row - 1, column + 1)) {board.GetTile(row - 1, column + 1).ToggleState();}
            if (board.IsValidCoord(row + 1, column + 1)) {board.GetTile(row + 1, column + 1).ToggleState();}
        }
        /// <summary>
        /// Creates a new board with the current difficulty (done because board is created too many times)
        /// </summary>
        private void CreateBoard()
        {
            board = new Board(difficulty);
        }
        /// <summary>
        /// Turns ON 1 * Size of the board to start the game
        /// </summary>
        private void ShuffleBoard()
        {
            // Creates a new seed
            Random random = new Random();
            // Variable of the max amount of toggles
            int count = 0;
            while (count < board.Size)
            {
                // Get random coordinates
                int row = random.Next(0, board.Size);
                int column = random.Next(0, board.Size);

                // Check if the tile is already toggled (already true)
                if (!board.GetTile(row, column).GetState())
                {
                    // Toggles the tile then add to the max amount of toggles possible
                    board.GetTile(row, column).ToggleState();
                    count++;
                }
            }
        }
        /// <summary>
        /// Saves the highScore in a file with he name of the difficulty
        /// </summary>
        /// <param name="difficulty"> Difficulty, which determines the name of the file </param>
        /// <param name="moves"> HighScore (in moves)</param>
        private void SaveHighScore(Difficulty difficulty, int moves)
        {
            File.AppendAllText($"{difficulty}.txt", $"{moves}");
        }
        /// <summary>
        /// Opens a file and return the lowest amount of moves in a specific difficulty
        /// </summary>
        /// <param name="difficulty"> Difficulty wanted </param>
        /// <returns> Lowest number of moves stored there </returns>
        private int GetHighScore(Difficulty difficulty)
        {
            // Variable to use for fire reading
            string s;
            // Create new list to compare
            List<int> scores = new List<int>();
            // Open the file and create a reader
            StreamReader sr = new StreamReader($"{difficulty}.txt");
            // Read file
            while ((s = sr.ReadLine()) != null)
            {
                // Add score to list while converting to int
                scores.Add(int.Parse(s));
            }
            // Close file
            sr.Close();
            // Sort the score
            scores.Sort();
            // Return the higher score
            return scores[0];
        }
    }
}