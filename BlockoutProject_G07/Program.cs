using System;

namespace BlockoutProject_G07
{
    public class Program
    {
        // Difficulty variable so it is accessible by other files
        public static Difficulty difficulty;
        /// <summary>
        /// Program begins here
        /// </summary>
        /// <param name="args"> Not used </param>
        private static void Main()
        {
            // Set default difficulty to easy
            difficulty = Difficulty.Easy;

            // Create the Board
            Board board = new Board(difficulty);

            // Instantiate View
            IView view = new SpectreView();

            // Instantiate Controller
            Controller controller = new Controller(board);

            // Start the program instance
            controller.Run(view);
        }
        
    }
}
