using FoodDrinkApp.Models;
using FoodDrinkApp.Services;
using System.Globalization;
using System.Xml;

namespace FoodDrinkApp.Views;

public class ProteinToWidthConverter : IValueConverter
{
    public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        if (value is int protein)
            return Math.Min(protein * 2, 100).ToString();
        return "0";
    }
    public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture) => throw new NotImplementedException();
}

public class CarbsToWidthConverter : IValueConverter
{
    public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        if (value is int carbs)
            return Math.Min(carbs, 100).ToString();
        return "0";
    }
    public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture) => throw new NotImplementedException();
}

public class FatToWidthConverter : IValueConverter
{
    public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        if (value is int fat)
            return Math.Min(fat * 3, 100).ToString();
        return "0";
    }
    public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture) => throw new NotImplementedException();
}

public class FiberToWidthConverter : IValueConverter
{
    public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        if (value is int fiber)
            return Math.Min(fiber * 5, 100).ToString();
        return "0";
    }
    public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture) => throw new NotImplementedException();
}

[QueryProperty(nameof(ItemId), "id")]
public partial class EntryDetailPage : ContentPage
{
    private FoodEntry? currentItem;

    public EntryDetailPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        FontScaler.ApplyFontScale(this);
    }

    protected override void OnDisappearing()
    {
        TextSpeaker.Stop();
        base.OnDisappearing();
    }

    public string ItemId
    {
        set => _ = LoadItemAsync(value);
    }

    private async Task LoadItemAsync(string id)
    {
        currentItem = await DataManager.GetByIdAsync(id);
        BindingContext = currentItem;
        RenderItem();
    }

    private void RenderItem()
    {
        if (currentItem is null)
        {
            NameLabel.Text = "Not found";
            DescriptionLabel.Text = "Record could not be loaded.";
            return;
        }

        NameLabel.Text = currentItem.Name;
        CategoryLabel.Text = currentItem.Category;
        CaloriesLabel.Text = $"{currentItem.Calories} kcal";

        ProteinValueLabel.Text = $"{currentItem.Protein}g";
        CarbsValueLabel.Text = $"{currentItem.Carbs}g";
        FatValueLabel.Text = $"{currentItem.Fat}g";
        FiberValueLabel.Text = $"{currentItem.Fiber}g";
        SugarValueLabel.Text = $"{currentItem.Sugar}g";
        SodiumValueLabel.Text = $"{currentItem.Sodium}mg";
        VitaminCValueLabel.Text = $"{currentItem.VitaminC}mg";

        DescriptionLabel.Text = currentItem.Description;
        AllergyLabel.Text = currentItem.AllergyNote;

        SemanticProperties.SetDescription(NameLabel, currentItem.AccessibleSummary);
    }

    private async void OnSpeakClicked(object? sender, EventArgs e)
    {
        if (currentItem is null)
        {
            await DisplayAlert("Error", "No data to read.", "OK");
            return;
        }

        try
        {
            await TextSpeaker.SpeakAsync(currentItem.AccessibleSummary);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private void OnStopSpeechClicked(object? sender, EventArgs e)
    {
        TextSpeaker.Stop();
        SemanticScreenReader.Announce("Stopped");
    }

    private async void OnVibrateClicked(object? sender, EventArgs e)
    {
        try
        {
            Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(300));
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            await DisplayAlert("Reminder", "Vibration triggered", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}