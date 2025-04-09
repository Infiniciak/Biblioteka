using System.Collections.ObjectModel;
using System.Diagnostics;
using Library;
using Microsoft.VisualStudio.PlatformUI;

public class MemberViewModel : ObservableObject
{
    private readonly MemberService _memberService;
    private ObservableCollection<MembersDto> _members = new();
    private bool _isLoading;

    public ObservableCollection<MembersDto> Members
    {
        get => _members;
        set => SetProperty(ref _members, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public MemberViewModel(MemberService memberService)
    {
        _memberService = memberService;
        _ = LoadMembersAsync(); // Changed to async suffix
    }

    private async Task LoadMembersAsync() // Added Async suffix
    {
        try
        {
            IsLoading = true;
            var members = await _memberService.GetAllMembersAsync();
            Members = new ObservableCollection<MembersDto>(members);
        }
        catch (Exception ex)
        {
            // Handle/log error appropriately
            Debug.WriteLine($"Error loading members: {ex.Message}");
            Members = new ObservableCollection<MembersDto>(); // Return empty collection
        }
        finally
        {
            IsLoading = false;
        }
    }
}