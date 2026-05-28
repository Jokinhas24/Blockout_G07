using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace BlockoutProject_G07
{
    /// <summary>
    /// View using the Spectre Console
    /// </summary>
    public class SpectreView : IView
    {
        /// <summary>
        /// Shows the main menu and returns option
        /// </summary>
        /// <returns> User's chosen option </returns>
        public string ShowMenu()
        {
            // Title
            AnsiConsole.MarkupLine("\n[blue]Main Menu[/]");
            // Option menu
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose one:")
            .AddChoices("Play", "Difficulty", "Tutorial", "Quit"));
            // Showing what was chosen
            AnsiConsole.MarkupLine($"\nOption chosen: [yellow]{option}[/]");
            return option;
        }
        /// <summary>
        /// Shows the welcome message
        /// </summary>
        public void WelcomeMessage()
        {
            AnsiConsole.MarkupLine("\n[blue]>>>> Welcome to Jokinhas24's Blockout Game! <<<<[/]");
        }
        /// <summary>
        /// Shows the exit message
        /// </summary>
        public void ExitMessage()
        {
            AnsiConsole.MarkupLine("\n[red]Quitting...[/]");
        }
        /// <summary>
        /// Shows an error message
        /// </summary>
        /// <param name="message"> String describing what type of error it is </param>
        public void ErrorMessage(string message)
        {
            AnsiConsole.MarkupLine($"\n[red]>>> {message} <<<[/]\n");
        }
        /// <summary>
        /// Waits for user to press a key
        /// </summary>
        public void WaitUser()
        {
            AnsiConsole.MarkupLine("\n[grey]Press any key to continue...[/]");
            Console.ReadKey(true);
        }
        /// <summary>
        /// Shows the tutorial
        /// </summary>
        public void ShowTutorial()
        {
            AnsiConsole.MarkupLine("\nTo play Blockout choose a tile.");
            AnsiConsole.MarkupLine("Then, the environment will react toggling all adjacent tiles.");
            AnsiConsole.MarkupLine("Meaning: All turned OFF tiles will become ON and vice versa.");
            AnsiConsole.MarkupLine("Turn all tiles OFF to win the game.");
            AnsiConsole.MarkupLine("\n[yellow]Good Luck![/]");
        }
        /// <summary>
        /// Shows board (per Line)
        /// </summary>
        /// <param name="board"> Current board to be shown </param>
        public void ShowBoard(Board board)
        {   
            //Space
            AnsiConsole.MarkupLine("");
            // Make Row
            for(int i = 0; i < board.Size; i++)
            {
                string line = " ";
                // Make Columns
                for(int j = 0; j < board.Size; j++)
                {
                    // Make Board with symbols, fill for ON and empty for OFF
                    line +=board.GetTile(i, j).GetState()
                        ? "[red]■[/] "
                        : "[green]□[/] ";
                }
                // Show Board (per Line)
                AnsiConsole.MarkupLine(line);
            }
        }
        /// <summary>
        /// Shows the game menu and highscore
        /// </summary>
        /// <returns> User's chosen option </returns>
        public string ShowGameMenu(Difficulty difficulty, int moves)
        {
            // Title
            AnsiConsole.MarkupLine("\n[blue]In-Game Menu[/]");
            // Defining default color
            Color color = Color.White;
            // Changing color accordingly with the difficulty
            if (difficulty == Difficulty.Easy) {color = Color.Green;}
            else if (difficulty == Difficulty.Medium) {color = Color.Yellow;}
            else if (difficulty == Difficulty.Hard) {color = Color.Red;}
            // Highscore
            AnsiConsole.MarkupLine($"\nHighscore in [{color}]{difficulty}[/] difficulty: [yellow]{moves} moves.[/]");
            // Option menu
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose one:")
            .AddChoices("Coordinates", "Tutorial", "Quit"));
            // Showing what was chosen
            AnsiConsole.MarkupLine($"\nOption chosen: [yellow]{option}[/]");
            return option;
        }
        /// <summary>
        /// Asks for coordinates
        /// </summary>
        /// <returns> User's coordinates </returns>
        public (int, int) AskCoordinates(Board board)
        {
            // Variables
            int row, column;
            // Get user coordinates while not having valid coordinates
            do
            {
                // If row or column is wrong reminds the board size
                row = -1 + AnsiConsole.Prompt(
                    new TextPrompt<int>($"Choose a Row:")
                        .ValidationErrorMessage($"[orange]Must be a number between 1 and {board.Size}![/]"));
                column = -1 + AnsiConsole.Prompt(
                    new TextPrompt<int>($"Choose a Column:")
                        .ValidationErrorMessage($"[orange]Must be a number between 1 and {board.Size}![/]"));
                // Check if is a valid coordinate
                if (!board.IsValidCoord(row, column))
                {
                    ErrorMessage("Invalid Coordinate!");
                }
            } while (!board.IsValidCoord(row, column));
            return (row, column);
        }
        /// <summary>
        /// Shows changing difficulty menu and returns new difficulty
        /// </summary>
        /// <returns> User's chosen difficulty </returns>
        public string ShowDifficultyMenu()
        {
            // Title
            AnsiConsole.MarkupLine("\n[blue]Difficulty Menu[/]");
            // Option menu, detailing what each option does
            var difficulty = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose one:")
            .AddChoices("Easy", "Medium", "Hard")
            .UseConverter(choice =>
                choice switch
                {
                    "Easy" => "[green]Easy (3x3 board)[/]",
                    "Medium" => "[yellow]Medium (5x5 board)[/]",
                    "Hard" => "[red]Hard (8x8 board)[/]",
                    _ => choice
                }
            ));
            // Defining default color
            Color color = Color.White;
            // Changing color accordingly with the difficulty
            if (difficulty == "Easy") {color = Color.Green;}
            else if (difficulty == "Medium") {color = Color.Yellow;}
            else if (difficulty == "Hard") {color = Color.Red;}
            // Showing what was chosen
            AnsiConsole.MarkupLine( $"\nOption chosen: [{color}]{difficulty}[/]");
            return difficulty;
        }
        /// <summary>
        /// Congratulates the user on winning and asks if he wants to continue playing
        /// </summary>
        /// <returns> Bool saying if the user wants to start again or not </returns>
        public bool GameWinMessage(int moves)
        {
            // Congrats message
            AnsiConsole.MarkupLine("\n[yellow]Congratulations! You Won![/]\n");
            // Telling how many moves the user used
            AnsiConsole.MarkupLine($"Your score: [yellow]{moves} moves.[/]");
            // Asks to continue playing
            var confirmed = AnsiConsole.Confirm("\nDo you wish to continue playing?");
            // Returns users response
            if (confirmed) { return true; }
            else { return false; }
        }
    }
}