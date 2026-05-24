using System;

namespace BlockoutProject_G07
{
    public class Program
    {
        /// <summary>
        /// Program begins here.
        /// </summary>
        /// <param name="args">Not used.</param>
        private static void Main()
        {
            Console.WriteLine("Hello LP!");
            // Initialize the player list with two players using collection
            // initialization syntax
            //PlayerList playerList = new PlayerList() {
            //    new Player("Best player ever", 100),
            //    new Player("An even better player", 500),
            //    new Player("Freddy", 125),
            //    new Player("Chica", 200),
            //    new Player("Daniel", 150)
            //};

            // Intantiate View
            IView view = new SpectreView();

            // Instantiate Controller
            //Controller controller = new Controller(playerList);

            // Start the program instance
            //controller.Run(view);
        }
        
    }
}
