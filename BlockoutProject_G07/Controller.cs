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
        /// <summary>
        /// Controller of the Controller
        /// </summary>
        /// <param name="board"> Current Board </param>
        public Controller(Board board)
        {
            // Keep the player list (part of the model)
            this.board = board;
        }
        /// <summary>
        /// Starts the main menu loop
        /// </summary>
        /// <param name="view"> Current View </param>
        public void Run(IView view)
        {
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
            } while (option != "0");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public void StartGame(IView view, Board board)
        {
            // We keep the user's option here
            string option;

            // Main game loop
            do
            {
                // Show menu and get user option
                view.ShowMenu();
                option = view.Input();



                // Wait for user to press a key...  
                if (option != "0")
                {
                    view.WaitUser();
                }

                // Loop keeps going until players choses to quit (option 0)
            } while (option != "0");
        }
        public void ChangeDifficulty(IView view, Board board)
        {
            
        }
    }
}