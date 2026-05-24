using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockoutProject_G07
{
    public interface IView
    {
        string Input();
        void ShowMenu();
        void WelcomeMessage();
        void ExitMessage();
        void ErrorMessage(string message);
        void WaitUser();
        void ShowTutorial();
        void ShowBoard(Board board);
    }
}