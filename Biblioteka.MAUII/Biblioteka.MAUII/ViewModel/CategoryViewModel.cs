// MembersViewModel.cs
using System.Collections.ObjectModel;
using Library;
using Microsoft.VisualStudio.PlatformUI;

public class CategoryViewModel : ObservableObject
{
    private readonly CategoryService _categoryService;
    private ObservableCollection<CategoriesDto> _categories = new();

    public ObservableCollection<CategoriesDto> Categories
    {
        get => _categories;
        set => SetProperty(ref _categories, value);
    }

    public CategoryViewModel(CategoryService categoryService)
    {
        _categoryService = categoryService;
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        await LoadMembers();
    }

    private async Task LoadMembers()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        Categories = new ObservableCollection<CategoriesDto>(categories);
    }
}