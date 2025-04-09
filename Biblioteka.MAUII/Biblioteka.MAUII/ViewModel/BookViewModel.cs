using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel; // Use CommunityToolkit instead of PlatformUI
using CommunityToolkit.Mvvm.Input;
using Library;

public partial class BookViewModel : ObservableObject
{
    private readonly BookService _service;

    [ObservableProperty]
    private ObservableCollection<BooksDto> _books = new();

    public BookViewModel(BookService service)
    {
        _service = service;
        LoadBooksCommand.Execute(null); 
    }

    [RelayCommand]
    private async Task LoadBooks()
    {
        try
        {
            var books = await _service.GetAllBooksAsync();
            Books = new ObservableCollection<BooksDto>(books);
        }
        catch (Exception ex)
        {
            // Handle exceptions properly
            await Shell.Current.DisplayAlert("Error", $"Failed to load books: {ex.Message}", "OK");
        }
    }
}