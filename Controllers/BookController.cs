using Spectre.Console;
using TCSA.OOP.LibraryManagementSystem.Models;

namespace TCSA.OOP.LibraryManagementSystem.Controllers;

internal class BookController : BaseController, IBaseController
{
    public void ViewItems()
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);

        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[yellow]Title[/]");
        table.AddColumn("[yellow]Author[/]");
        table.AddColumn("[yellow]Category[/]");
        table.AddColumn("[yellow]Location[/]");
        table.AddColumn("[yellow]Pages[/]");

        // Filtering only items of the book type
        var books = MockDatabase.LibraryItems.OfType<Book>();

        foreach (var book in books)
        {
            table.AddRow(
                book.Id.ToString(),
                $"[cyan]{book.Name}[/]",
                $"[cyan]{book.Author}[/]",
                $"[green]{book.Category}[/]",
                $"[blue]{book.Location}[/]",
                book.Pages.ToString()
                );
        }

        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey();
    }

    public void AddItem()
    {
        var title = AnsiConsole.Ask<string>("Enter the [green]title[/] of the book to add:");
        var author = AnsiConsole.Ask<string>("Enter the [green]author[/] of the book:");
        var category = AnsiConsole.Ask<string>("Enter the [green]category[/] of the book:");
        var location = AnsiConsole.Ask<string>("Enter the [green]location[/] of the book:");
        var pages = AnsiConsole.Ask<int>("Enter the [green]number of pages[/] in the book:");

        if (MockDatabase.LibraryItems.OfType<Book>().Any(b => b.Name.Equals(title, StringComparison.OrdinalIgnoreCase)))
        {
            DisplayMessage("Book already exists!", "green");
        }
        else
        {
            var newBook = new Book(MockDatabase.LibraryItems.Count + 1, title, author, category, location, pages);
            MockDatabase.LibraryItems.Add(newBook);
            DisplayMessage("Book added succesfully!", "green");
        }


        DisplayMessage("Press any key to continue.");
        Console.ReadKey();

    }

    public void DeleteItem()
    {
        var books = MockDatabase.LibraryItems.OfType<Book>().ToList();
        // checking if there are any books to delete and letting the user know
        if (books.Count() == 0)
        {
            DisplayMessage("No books available to delete.", "red");
            Console.ReadKey();
            return;
        }

        /* showing a list of books and letting the user choose with arrows 
         using SelectionPrompt, similarly to what we do with the menu */
        Book bookToDelete = AnsiConsole.Prompt(
            new SelectionPrompt<Book>()
            .Title("Select a [red]book[/] to delete:")
            .AddChoices(books)
            .UseConverter(b => $"{b.Name} by {b.Author}"));

        /* Using the Remove method to delete a book. This method returns a boolean
          that we can use to present a message in case of success or failure.*/

        if (ConfirmDeletion(bookToDelete.Name))
        {


            if (MockDatabase.LibraryItems.Remove(bookToDelete))
            {
                DisplayMessage("Book deleted successfully!", "red");

            }
            else
            {
                AnsiConsole.MarkupLine("[red]Book not found.[/]");

            }
        }
        else
        {
            DisplayMessage("Deletion canceled.", "yellow"); 
        }


        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey();

    }

}
