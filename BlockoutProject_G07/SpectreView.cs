using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace BlockoutProject_G07
{
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
            AnsiConsole.MarkupLine("\n[blue]>>>> Welcome to Jokinhas24's Blockout Game! <<<<[/]\n");
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
    
        public void ShowBoard(Board board)
        {
            var table = new Table();
            // Make columns
            for(int i = 0; i < board.Size; i++)
            {
                table.AddColumn(i.ToString());
            }
            // Make rows
            for(int i = 0; i < board.Size; i++)
            {
                List<string> row = new();

                for(int j = 0; j < board.Size; j++)
                {
                    row.Add(board.GetTile(i, j).GetState()
                        ? "[green]X[/]"
                        : "[red]O[/]");
                }

                table.AddRow(row.ToArray());
            }
            // Show Board
            AnsiConsole.Write(table);
        }
        /// <summary>
        /// Shows the game menu
        /// </summary>
        public string ShowGameMenu()
        {
            // Title
            AnsiConsole.MarkupLine("\n[blue]In-Game Menu[/]\n");
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
                row = AnsiConsole.Ask<int>("Choose a Row:");
                column = AnsiConsole.Ask<int>("Choose a Column:");
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
            AnsiConsole.MarkupLine("\n[blue]Difficulty Menu[/]\n");
            // Option menu
            var difficulty = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose one:")
            .AddChoices("Easy", "Medium", "Hard"));
            // Showing what was chosen
            AnsiConsole.MarkupLine( $"\nOption chosen: [yellow]{difficulty}[/]");
            return difficulty;
        }
    }
}