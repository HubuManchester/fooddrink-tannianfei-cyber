using System.Net.Http.Json;
using System.Text.Json;
using FoodDrinkApp.Models;

namespace FoodDrinkApp.Services;

public static class FoodCatalogService
{
    private static readonly HttpClient HttpClient = new()
    {
        Timeout = TimeSpan.FromSeconds(12)
    };

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private static readonly List<FoodItem> LocalFallbackItems =
    [
        new()
        {
            Name = "Avocado Toast",
            Category = "Breakfast",
            Description = "Sourdough toast topped with mashed avocado, cherry tomatoes, and chia seeds.",
            Calories = 385,
            Protein = 12,
            Carbs = 38,
            Fat = 22,
            Fiber = 10,
            Sugar = 3,
            Sodium = 320,
            VitaminC = 8,
            AllergyNote = "Contains gluten.",
            Tags = "avocado toast breakfast healthy"
        },
        new()
        {
            Name = "Quinoa Buddha Bowl",
            Category = "Lunch",
            Description = "Quinoa base with roasted sweet potato, chickpeas, kale, and tahini dressing.",
            Calories = 455,
            Protein = 15,
            Carbs = 62,
            Fat = 16,
            Fiber = 12,
            Sugar = 8,
            Sodium = 480,
            VitaminC = 25,
            AllergyNote = "No common allergens. Contains sesame.",
            Tags = "quinoa buddha bowl vegan lunch"
        },
        new()
        {
            Name = "Salmon with Asparagus",
            Category = "Dinner",
            Description = "Grilled wild salmon served with roasted asparagus and lemon butter sauce.",
            Calories = 520,
            Protein = 42,
            Carbs = 18,
            Fat = 32,
            Fiber = 6,
            Sugar = 4,
            Sodium = 210,
            VitaminC = 15,
            AllergyNote = "Contains fish. Dairy in sauce.",
            Tags = "salmon asparagus dinner protein"
        },
        new()
        {
            Name = "Green Smoothie Bowl",
            Category = "Breakfast",
            Description = "Blended spinach, banana, mango, topped with granola and coconut flakes.",
            Calories = 310,
            Protein = 8,
            Carbs = 58,
            Fat = 9,
            Fiber = 11,
            Sugar = 28,
            Sodium = 95,
            VitaminC = 45,
            AllergyNote = "Contains coconut.",
            Tags = "smoothie bowl green breakfast"
        },
        new()
        {
            Name = "Mediterranean Wrap",
            Category = "Lunch",
            Description = "Whole wheat wrap with hummus, falafel, cucumber, tomato, and feta cheese.",
            Calories = 490,
            Protein = 18,
            Carbs = 64,
            Fat = 19,
            Fiber = 14,
            Sugar = 7,
            Sodium = 720,
            VitaminC = 12,
            AllergyNote = "Contains gluten and dairy.",
            Tags = "mediterranean wrap falafel lunch"
        },
        new()
        {
            Name = "Mushroom Risotto",
            Category = "Dinner",
            Description = "Creamy arborio rice with wild mushrooms, parmesan, and fresh parsley.",
            Calories = 580,
            Protein = 16,
            Carbs = 88,
            Fat = 18,
            Fiber = 6,
            Sugar = 5,
            Sodium = 650,
            VitaminC = 4,
            AllergyNote = "Contains dairy. Contains gluten.",
            Tags = "risotto mushroom dinner vegetarian"
        },
        new()
        {
            Name = "Matcha Protein Shake",
            Category = "Drink",
            Description = "Matcha green tea powder blended with oat milk and vanilla protein powder.",
            Calories = 210,
            Protein = 24,
            Carbs = 22,
            Fat = 5,
            Fiber = 4,
            Sugar = 12,
            Sodium = 180,
            VitaminC = 2,
            AllergyNote = "Contains soy in protein powder.",
            Tags = "matcha protein shake drink"
        },
        new()
        {
            Name = "Apple Cinnamon Oatmeal",
            Category = "Breakfast",
            Description = "Rolled oats cooked with diced apple, cinnamon, and a drizzle of maple syrup.",
            Calories = 290,
            Protein = 9,
            Carbs = 54,
            Fat = 5,
            Fiber = 8,
            Sugar = 18,
            Sodium = 85,
            VitaminC = 6,
            AllergyNote = "Naturally gluten-free if certified oats used.",
            Tags = "oatmeal apple cinnamon breakfast"
        },
        new()
        {
            Name = "Thai Coconut Curry",
            Category = "Dinner",
            Description = "Coconut milk based curry with tofu, bell peppers, bamboo shoots, and Thai basil.",
            Calories = 465,
            Protein = 14,
            Carbs = 38,
            Fat = 28,
            Fiber = 7,
            Sugar = 12,
            Sodium = 890,
            VitaminC = 35,
            AllergyNote = "Contains coconut. Contains soy.",
            Tags = "thai curry coconut dinner vegan"
        },
        new()
        {
            Name = "Tuna Salad Lettuce Wrap",
            Category = "Lunch",
            Description = "Albacore tuna mixed with Greek yogurt, celery, wrapped in butter lettuce.",
            Calories = 245,
            Protein = 28,
            Carbs = 8,
            Fat = 12,
            Fiber = 3,
            Sugar = 3,
            Sodium = 410,
            VitaminC = 5,
            AllergyNote = "Contains fish. Contains dairy.",
            Tags = "tuna salad lettuce wrap lowcarb lunch"
        },
        new()
        {
            Name = "Dark Chocolate Banana Smoothie",
            Category = "Drink",
            Description = "Frozen banana, cocoa powder, almond milk, and a touch of honey.",
            Calories = 260,
            Protein = 6,
            Carbs = 48,
            Fat = 7,
            Fiber = 9,
            Sugar = 28,
            Sodium = 55,
            VitaminC = 12,
            AllergyNote = "Contains tree nuts (almond milk).",
            Tags = "chocolate banana smoothie drink"
        },
        new()
        {
            Name = "Zucchini Noodle Pesto",
            Category = "Dinner",
            Description = "Spiralized zucchini tossed in basil pesto with cherry tomatoes and pine nuts.",
            Calories = 310,
            Protein = 8,
            Carbs = 18,
            Fat = 24,
            Fiber = 6,
            Sugar = 8,
            Sodium = 380,
            VitaminC = 28,
            AllergyNote = "Contains tree nuts (pine nuts).",
            Tags = "zoodles pesto lowcarb dinner"
        },
        new()
        {
            Name = "Blueberry Chia Pudding",
            Category = "Breakfast",
            Description = "Overnight chia seeds soaked in coconut milk, topped with fresh blueberries.",
            Calories = 270,
            Protein = 7,
            Carbs = 32,
            Fat = 14,
            Fiber = 14,
            Sugar = 16,
            Sodium = 45,
            VitaminC = 9,
            AllergyNote = "Contains coconut.",
            Tags = "chia pudding blueberry breakfast"
        },
        new()
        {
            Name = "Spicy Tuna Roll",
            Category = "Lunch",
            Description = "Sushi roll with tuna, avocado, cucumber, and spicy mayo.",
            Calories = 340,
            Protein = 20,
            Carbs = 42,
            Fat = 12,
            Fiber = 4,
            Sugar = 6,
            Sodium = 520,
            VitaminC = 3,
            AllergyNote = "Contains fish. Contains gluten (soy sauce).",
            Tags = "sushi tuna roll lunch"
        },
        new()
        {
            Name = "Kombucha",
            Category = "Drink",
            Description = "Fermented tea beverage with probiotics and natural fruit flavors.",
            Calories = 45,
            Protein = 0,
            Carbs = 11,
            Fat = 0,
            Fiber = 0,
            Sugar = 9,
            Sodium = 8,
            VitaminC = 1,
            AllergyNote = "No common allergens.",
            Tags = "kombucha probiotic drink"
        },
        new()
        {
            Name = "Sweet Potato Chickpea Bowl",
            Category = "Dinner",
            Description = "Roasted sweet potato, chickpeas, spinach, and tahini-maple dressing.",
            Calories = 425,
            Protein = 13,
            Carbs = 68,
            Fat = 14,
            Fiber = 16,
            Sugar = 18,
            Sodium = 350,
            VitaminC = 22,
            AllergyNote = "Contains sesame.",
            Tags = "sweet potato chickpea vegan dinner"
        }
    ];

    private static List<FoodItem> cachedItems = new(LocalFallbackItems);

    public static bool LastLoadUsedMockApi { get; private set; }

    public static async Task<IReadOnlyList<FoodItem>> SearchAsync(string? query)
    {
        var items = await GetAllAsync();

        if (string.IsNullOrWhiteSpace(query))
        {
            return items.OrderBy(item => item.Name).ToList();
        }

        var normalised = query.Trim();
        return items
            .Where(item =>
                item.Name.Contains(normalised, StringComparison.OrdinalIgnoreCase) ||
                item.Category.Contains(normalised, StringComparison.OrdinalIgnoreCase) ||
                item.Description.Contains(normalised, StringComparison.OrdinalIgnoreCase) ||
                item.Tags.Contains(normalised, StringComparison.OrdinalIgnoreCase))
            .OrderBy(item => item.Name)
            .ToList();
    }

    public static async Task<FoodItem?> GetByIdAsync(string id)
    {
        if (MockApiConfig.IsConfigured)
        {
            try
            {
                var item = await HttpClient.GetFromJsonAsync<FoodItem>(
                    $"{MockApiConfig.EndpointUrl.TrimEnd('/')}/{Uri.EscapeDataString(id)}",
                    JsonOptions);

                if (item is not null)
                {
                    return item;
                }
            }
            catch
            {
            }
        }

        return cachedItems.FirstOrDefault(item => item.Id == id);
    }

    public static async Task<FoodItem> AddAsync(FoodItem item)
    {
        if (MockApiConfig.IsConfigured)
        {
            var response = await HttpClient.PostAsJsonAsync(MockApiConfig.EndpointUrl, item, JsonOptions);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<FoodItem>(JsonOptions);
            if (created is not null)
            {
                cachedItems.Add(created);
                return created;
            }
        }

        cachedItems.Add(item);
        return item;
    }

    private static async Task<IReadOnlyList<FoodItem>> GetAllAsync()
    {
        if (!MockApiConfig.IsConfigured)
        {
            LastLoadUsedMockApi = false;
            return cachedItems;
        }

        try
        {
            var items = await HttpClient.GetFromJsonAsync<List<FoodItem>>(MockApiConfig.EndpointUrl, JsonOptions);
            if (items is { Count: > 0 })
            {
                cachedItems = items;
                LastLoadUsedMockApi = true;
                return cachedItems;
            }
        }
        catch
        {
        }

        LastLoadUsedMockApi = false;
        return cachedItems;
    }
}