using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockoutProject_G07
{
    /// <summary>
    /// Interface of views to handle the game
    /// </summary>
    public interface IView
    {
        string ShowMenu();
        void WelcomeMessage();
        void ExitMessage();
        void ErrorMessage(string message);
        void WaitUser();
        void ShowTutorial();
        void ShowBoard(Board board);
        string ShowGameMenu(Difficulty difficulty, int moves);
        (int, int) AskCoordinates(Board board);
        string ShowDifficultyMenu();
        bool GameWinMessage(int moves);
    }
}