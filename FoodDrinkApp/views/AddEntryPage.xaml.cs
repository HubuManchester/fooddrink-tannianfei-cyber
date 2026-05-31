using FoodDrinkApp.Models;
using FoodDrinkApp.Services;
using System.Formats.Tar;

namespace FoodDrinkApp.Views;

public partial class AddEntryPage : ContentPage
{
    public AddEntryPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        FontScaler.ApplyFontScale(this);
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        try
        {
            var validationMessage = ValidateForm(out var calories, out var protein, out var carbs, out var fat, out var fiber, out var sugar, out var sodium, out var vitaminC);
            if (validationMessage is not null)
            {
                ShowValidation(validationMessage);
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(250));
                return;
            }

            var item = new FoodEntry
            {
                Name = NameEntry.Text!.Trim(),
                Category = CategoryPicker.SelectedItem?.ToString() ?? "Snack",
                Description = DescriptionEditor.Text!.Trim(),
                Calories = calories,
                Protein = protein,
                Carbs = carbs,
                Fat = fat,
                Fiber = fiber,
                Sugar = sugar,
                Sodium = sodium,
                VitaminC = vitaminC,
                AllergyNote = string.IsNullOrWhiteSpace(AllergyEntry.Text)
                    ? "No allergy note provided."
                    : AllergyEntry.Text.Trim(),
                Tags = $"{NameEntry.Text} {CategoryPicker.SelectedItem} {DescriptionEditor.Text}"
            };

            await DataManager.AddAsync(item);
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            SemanticScreenReader.Announce("Food record saved.");

            await DisplayAlert("Saved", "The record has been saved successfully.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            ShowValidation($"The record could not be saved: {ex.Message}");
        }
    }

    private string? ValidateForm(out int calories, out int protein, out int carbs, out int fat, out int fiber, out int sugar, out int sodium, out int vitaminC)
    {
        calories = protein = carbs = fat = fiber = sugar = sodium = vitaminC = 0;

        if (string.IsNullOrWhiteSpace(NameEntry.Text))
            return "Please enter a food or drink name.";

        if (CategoryPicker.SelectedIndex < 0)
            return "Please choose a category.";

        if (string.IsNullOrWhiteSpace(DescriptionEditor.Text))
            return "Please add a short description.";

        var error = TryReadNumber(CaloriesEntry.Text, "calories", out calories);
        if (error != null) return error;

        error = TryReadNumber(ProteinEntry.Text, "protein", out protein);
        if (error != null) return error;

        error = TryReadNumber(CarbsEntry.Text, "carbs", out carbs);
        if (error != null) return error;

        error = TryReadNumber(FatEntry.Text, "fat", out fat);
        if (error != null) return error;

        error = TryReadNumber(FiberEntry.Text, "fiber", out fiber);
        if (error != null) return error;

        error = TryReadNumber(SugarEntry.Text, "sugar", out sugar);
        if (error != null) return error;

        error = TryReadNumber(SodiumEntry.Text, "sodium", out sodium);
        if (error != null) return error;

        error = TryReadNumber(VitaminCEntry.Text, "vitamin C", out vitaminC);
        if (error != null) return error;

        return null;
    }

    private static string? TryReadNumber(string? value, string fieldName, out int number)
    {
        if (int.TryParse(value, out number) && number >= 0)
            return null;

        return $"Please enter a valid non-negative number for {fieldName}.";
    }

    private void ShowValidation(string message)
    {
        ValidationLabel.Text = message;
        ValidationPanel.IsVisible = true;
        SemanticScreenReader.Announce(message);
    }
}