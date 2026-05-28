using System;

namespace BlockoutProject_G07
{
    public class Program
    {
        /// <summary>
        /// Program begins here
        /// </summary>
        /// <param name="args"> Not used </param>
        private static void Main()
        {
            // Instantiate View
            IView view = new SpectreView();
            // Instantiate Controller
            Controller controller = new Controller();
            // Start the program instance
            controller.Run(view);
        }
        
    }
}
