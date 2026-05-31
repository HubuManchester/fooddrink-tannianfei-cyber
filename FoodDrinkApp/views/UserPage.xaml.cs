using FoodDrinkApp.Services;

namespace FoodDrinkApp.Views;

public partial class UserPage : ContentPage
{
    public UserPage()
    {
        InitializeComponent();
        ThemePicker.SelectedIndex = 0;
        LargeTextSwitch.IsToggled = FontScaler.LargeTextEnabled;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LargeTextSwitch.IsToggled = FontScaler.LargeTextEnabled;
        ApplyLargeTextState();
    }

    private void OnThemeChanged(object? sender, EventArgs e)
    {
        Application.Current!.UserAppTheme = ThemePicker.SelectedIndex switch
        {
            1 => AppTheme.Light,
            2 => AppTheme.Dark,
            _ => AppTheme.Unspecified
        };

        Announce("Theme preference updated.");
    }

    private void OnLargeTextToggled(object? sender, ToggledEventArgs e)
    {
        FontScaler.LargeTextEnabled = e.Value;
        ApplyLargeTextState();
        Announce(e.Value
            ? "Large text mode is on. Page text is now larger."
            : "Large text mode is off. Page text has returned to normal.");
    }

    private void ApplyLargeTextState()
    {
        FontScaler.ApplyFontScale(this);

        LargeTextPreviewTitle.Text = FontScaler.LargeTextEnabled
            ? "Large text preview: enlarged"
            : "Large text preview";
        LargeTextPreviewBody.Text = FontScaler.LargeTextEnabled
            ? "Text is now noticeably larger. The food and hardware pages will use the same setting."
            : "Turn on the switch to enlarge this preview and other page text.";
    }

    private void Announce(string message)
    {
        SettingsStatusLabel.Text = message;
        SemanticScreenReader.Announce(message);
    }
}