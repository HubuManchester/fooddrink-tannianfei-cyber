using FoodDrinkApp.Views;

namespace FoodDrinkApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(AddEntryPage), typeof(AddEntryPage));
        Routing.RegisterRoute(nameof(EntryDetailPage), typeof(EntryDetailPage));
    }
}