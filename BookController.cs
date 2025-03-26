using Spectre.Console;

namespace TCSA.OOP.LibraryManagementSystem;

internal class BookController
{
    internal void ViewBooks()
    {
        AnsiConsole.MarkupLine("[yellow]List of Books: [/]");

        foreach (var book in MockDatabase.books)
        {
            AnsiConsole.MarkupLine($" - [cyan]{book.Name}[/] - [yellow]{book.Pages} pages[/]");
        }

        AnsiConsole.MarkupLine("Press any key to continue.");
        Console.ReadKey();
    }

    internal void AddBook()
    {
        var title = AnsiConsole.Ask<string>("Enter the [green]title[/] of the book to add:");
        var pages = AnsiConsole.Ask<int>("Enter the [green]number of pages[/] in the book:");

        if (MockDatabase.books.Exists(b => b.Name.Equals(title, StringComparison.OrdinalIgnoreCase)))
        {
            AnsiConsole.MarkupLine("[red]This book already exists.[/]");
        }
        else
        {
            var newBook = new Book(title, pages);
            MockDatabase.books.Add(newBook);
            AnsiConsole.MarkupLine("[green]Book added successfully.[/]");

        }

        AnsiConsole.MarkupLine("Press any key to continue.");
        Console.ReadKey();

    }

    internal void DeleteBook()
    {   
        // checking if there are any books to delete and letting the user know
        if (MockDatabase.books.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No books available to delete.[/]");
            Console.ReadKey();
            return;
        }

        /* showing a list of books and letting the user choose with arrows 
         using SelectionPrompt, similarly to what we do with the menu */
        var bookToDelete = AnsiConsole.Prompt(
            new SelectionPrompt<Book>()
            .Title("Select a [red]book[/] to delete:")
            .AddChoices(MockDatabase.books)
            .UseConverter(b => $"{b.Name}"));

        /* Using the Remove method to delete a book. This method returns a boolean
          that we can use to present a message in case of success or failure.*/
        if (MockDatabase.books.Remove(bookToDelete))
        {
            AnsiConsole.MarkupLine("[red]Book deleted successfully![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Book not found.[/]");
        }

        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey();

    }

}
