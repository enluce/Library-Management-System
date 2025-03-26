using Spectre.Console;
using static TCSA.OOP.LibraryManagementSystem.Enums;

namespace TCSA.OOP.LibraryManagementSystem;

internal class UserInterface
{
    private BookController bookController = new BookController();
    internal void MainMenu()
    {
        while (true)
        {  
            Console.Clear();

            var choice = AnsiConsole.Prompt(new SelectionPrompt<MenuOption>().Title("What do you want to do next?").AddChoices(Enum.GetValues<MenuOption>()));

            switch (choice)
            {
                case MenuOption.ViewBooks:

                    bookController.ViewBooks();
                    break;
                case MenuOption.AddBook:
                    bookController.AddBook();
                    break;

                case MenuOption.DeleteBook:
                    bookController.DeleteBook();
                    break;

            }
        }
    }
}
