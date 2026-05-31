using System.Text.Json.Serialization;

namespace FoodDrinkApp.Models;

public sealed class FoodItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("calories")]
    public int Calories { get; set; }

    [JsonPropertyName("protein")]
    public int Protein { get; set; }

    [JsonPropertyName("carbs")]
    public int Carbs { get; set; }

    [JsonPropertyName("fat")]
    public int Fat { get; set; }

    [JsonPropertyName("fiber")]
    public int Fiber { get; set; }

    [JsonPropertyName("sugar")]
    public int Sugar { get; set; }

    [JsonPropertyName("sodium")]
    public int Sodium { get; set; }

    [JsonPropertyName("vitaminC")]
    public int VitaminC { get; set; }

    [JsonPropertyName("allergyNote")]
    public string AllergyNote { get; set; } = string.Empty;

    [JsonPropertyName("tags")]
    public string Tags { get; set; } = string.Empty;

    [JsonIgnore]
    public string CaloriesLabel => $"{Calories} kcal";

    [JsonIgnore]
    public string MacroSummary => $"P {Protein}g ¡¤ C {Carbs}g ¡¤ F {Fat}g ¡¤ Fib {Fiber}g";

    [JsonIgnore]
    public string FullNutritionSummary => $"Protein {Protein}g, Carbs {Carbs}g, Fat {Fat}g, Fiber {Fiber}g, Sugar {Sugar}g, Sodium {Sodium}mg, Vitamin C {VitaminC}mg";

    [JsonIgnore]
    public string AccessibleSummary => $"{Name}. {Category}. {Calories} calories. {FullNutritionSummary}. {AllergyNote}";
}