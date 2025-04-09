// MembersViewModel.cs
using System.Collections.ObjectModel;
using Library;
using Microsoft.VisualStudio.PlatformUI;

public class BorrowViewModel : ObservableObject
{
    private readonly BorrowService _borrowService;
    private ObservableCollection<BorrowsDto> _borrows = new();

    public ObservableCollection<BorrowsDto> Borrows
    {
        get => _borrows;
        set => SetProperty(ref _borrows, value);
    }

    public BorrowViewModel(BorrowService borrowService)
    {
        _borrowService = borrowService;
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        await LoadBorrows();
    }

    private async Task LoadBorrows()
    {
        var borrows = await _borrowService.GetAllBorrowsAsync();
        Borrows = new ObservableCollection<BorrowsDto>(borrows);
    }
}
        