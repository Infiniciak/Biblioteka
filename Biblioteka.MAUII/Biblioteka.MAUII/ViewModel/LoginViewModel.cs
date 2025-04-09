using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Library;



public partial class LoginViewModel : ObservableObject
{
    private readonly DatabaseService _dbService;
    private readonly INavigation _navigation;

    [ObservableProperty]
    private string statusMessage;

    [ObservableProperty]
    private bool isConnected;

    [ObservableProperty]
    private List<CategoriesDto> categories;

    public LoginViewModel(DatabaseService dbService, INavigation navigation)
    {
        _dbService = dbService;
        _navigation = navigation;
        CheckConnection();
    }

    [RelayCommand]
    private async Task CheckConnection()
    {
        IsConnected = await _dbService.TestConnectionAsync();
        StatusMessage = IsConnected ? "Połączenie udane!" : "Błąd połączenia";

        if (IsConnected)
        {
            Categories = await _dbService.GetCategoriesAsync();
        }
    }
}