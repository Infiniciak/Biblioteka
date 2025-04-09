using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Library;
using System.Collections.ObjectModel;

public partial class MainViewModel : ObservableObject
{
    // Regiony dla lepszej organizacji kodu
    #region Properties

    [ObservableProperty]
    private bool _isBooksVisible = true;

    [ObservableProperty]
    private bool _isCategoriesVisible;

    [ObservableProperty]
    private bool _isMembersVisible;

    [ObservableProperty]
    private bool _isBorrowsVisible;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _statusMessage = "Wybierz sekcję";

    // Kolekcje danych
    [ObservableProperty]
    private ObservableCollection<BooksDto> _books = new();

    [ObservableProperty]
    private ObservableCollection<CategoriesDto> _categories = new();

    [ObservableProperty]
    private ObservableCollection<BorrowsDto> _borrows = new();

    [ObservableProperty]
    private ObservableCollection<MembersDto> _members = new();

    #endregion

    #region Commands

    [RelayCommand]
    private void ShowBooks()
    {
        SetAllSectionsInvisible();
        IsBooksVisible = true;
        StatusMessage = "Przeglądasz listę książek";
    }

    [RelayCommand]
    private void ShowCategories()
    {
        SetAllSectionsInvisible();
        IsCategoriesVisible = true;
        StatusMessage = "Przeglądasz kategorie";
    }

    [RelayCommand]
    private void ShowMembers()
    {
        SetAllSectionsInvisible();
        IsMembersVisible = true;
        StatusMessage = "Przeglądasz członków";
    }

    [RelayCommand]
    private void ShowBorrows()
    {
        SetAllSectionsInvisible();
        IsBorrowsVisible = true;
        StatusMessage = "Przeglądasz wypożyczenia";
    }

    [RelayCommand]
    private async Task LoadData()
    {
        if (IsLoading) return;

        IsLoading = true;
        StatusMessage = "Ładowanie danych...";

        try
        {
            await Task.WhenAll(
                LoadBooksAsync(),
                LoadCategories(),
                LoadMembers(),
                LoadBorrows()
            );
            StatusMessage = "Dane załadowane pomyślnie";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Błąd ładowania danych: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    #endregion

    #region Private Methods

    private void SetAllSectionsInvisible()
    {
        IsBooksVisible = false;
        IsCategoriesVisible = false;
        IsMembersVisible = false;
        IsBorrowsVisible = false;
    }

    private async Task LoadBooksAsync()
    {
        // Tutaj dodaj rzeczywiste ładowanie książek z bazy/usługi
        await Task.Delay(500); // Symulacja ładowania
        Books = new ObservableCollection<BooksDto>
        {
            new() { Id = 1, Title = "Diuna", Author = "Frank Herbert" },
            new() { Id = 2, Title = "Hobbit", Author = "J.R.R. Tolkien" }
        };
    }

    private async Task LoadCategories()
    {
        await Task.Delay(300);
        Categories = new ObservableCollection<CategoriesDto>
        {
            new() { Id = 1, Name = "Fantastyka" },
            new() { Id = 2, Name = "Science Fiction" }
        };
    }

    private async Task LoadMembers()
    {
        await Task.Delay(400);
        Members = new ObservableCollection<MembersDto>
        {
            new() { Id = 1, Name = "Jan", Surname = "Kowalski" },
            new() { Id = 2, Name = "Anna", Surname = "Nowak" }
        };
    }

    private async Task LoadBorrows()
    {
        await Task.Delay(600);
        Borrows = new ObservableCollection<BorrowsDto>
        {
            new() { Id = 1, BookId = 1, MemberId = 1 },
            new() { Id = 2, BookId = 2, MemberId = 2 }
        };
    }

    #endregion
}