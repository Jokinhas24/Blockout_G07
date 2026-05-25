using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockoutProject_G07
{
    public interface IView
    {
        string Input();
        string ShowMenu();
        void WelcomeMessage();
        void ExitMessage();
        void ErrorMessage(string message);
        void WaitUser();
        void ShowTutorial();
        void ShowBoard(Board board);
        string ShowGameMenu();
        (int, int) AskCoordinates(Board board);
        string ShowDifficultyMenu();
        void DifficultyMessage(string difficulty);
    }
}