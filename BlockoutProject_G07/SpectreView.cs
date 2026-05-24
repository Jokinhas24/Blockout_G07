using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockoutProject_G07
{
    public class SpectreView : IView
    {
        /// <summary>
        /// Asks for user input
        /// </summary>
        /// <returns> String written by user </returns>
        public string Input()
        {
            return Console.ReadLine();
        }
        /// <summary>
        /// Shows the main menu
        /// </summary>
        public void ShowMenu()
        {
            Console.WriteLine("Choose one:\n");
            Console.WriteLine("Press '1' to Start the Game;");
            Console.WriteLine("Press '2' to Change Difficulty;");
            Console.WriteLine("Press '3' to Read the Tutorial;");
            Console.WriteLine("Press '0' to Quit.\n");
            Console.Write("Your option: ");
        }
        /// <summary>
        /// Shows the welcome message
        /// </summary>
        public void WelcomeMessage()
        {
            Console.WriteLine("\n>>>> Welcome to Jokinhas24's Blockout Game! <<<<\n");
        }
        /// <summary>
        /// Shows the exit message
        /// </summary>
        public void ExitMessage()
        {
            Console.WriteLine("\nQuitting...");
        }
        /// <summary>
        /// Shows an error message
        /// </summary>
        /// <param name="message"> String describing what type of error it is </param>
        public void ErrorMessage(string message)
        {
            Console.Error.WriteLine($"\n>>> {message} <<<\n");
        }
        /// <summary>
        /// Waits for user to press a key
        /// </summary>
        public void WaitUser()
        {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine("\n");
        }
        /// <summary>
        /// Shows the tutorial
        /// </summary>
        public void ShowTutorial()
        {
            Console.WriteLine("\nLoading Tutorial...\n");
            Console.WriteLine("To play Blockout choose a tile.");
            Console.WriteLine("Then, the environment will react toggling all adjacent tiles.");
            Console.WriteLine("Meaning: All turned OFF tiles will become ON and vice versa.");
            Console.WriteLine("Turn all tiles OFF to win the game.");
            Console.WriteLine("\nGood Luck!");
        }
    
        public void ShowBoard(Board board)
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (j == board.Size - 1)
                    {
                        if (board.GetTile(i, j) == null)
                        {
                            Console.WriteLine("  ");
                        }
                        else
                        {
                            Console.WriteLine(board.GetTile(i, j).GetState());
                        }
                    }
                    else
                    {
                        if (board.GetTile(i, j) == null)
                        {
                            Console.Write("  |");
                        }
                        else
                        {
                            Console.Write($"{board.GetTile(i, j).GetState()}|");
                        }
                    }
                }
                if (i < board.Size - 1)
                {
                    Console.WriteLine("-----------");
                }
            }
        }
        public void ShowGameMenu()
        {
            Console.WriteLine("Choose one:\n");
            Console.WriteLine("Press '1' to Choose Coordinates;");
            Console.WriteLine("Press '2' to Read the Tutorial;");
            Console.WriteLine("Press '0' to Quit to Main Menu.\n");
            Console.Write("Your option: ");
        }
        public (int, int) AskCoordinates()
        {
            Console.WriteLine("Choose a row:\n");
            int row = int.Parse(Input());
            Console.WriteLine("Choose a column:\n");
            int column = int.Parse(Input());

            return (row, column);
        }
        public void ShowDifficultyMenu()
        {
            Console.WriteLine("Choose one:\n");
            Console.WriteLine("Press '1' for Easy;");
            Console.WriteLine("Press '2' for Medium;");
            Console.WriteLine("Press '3' for Hard.\n");
            Console.Write("Your option: ");
        }
        
    }
}