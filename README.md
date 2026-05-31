# VitaLog - Smart Diet Tracker

VitaLog is a cross-platform mobile application built with .NET MAUI for recording daily meals, querying nutritional information, and leveraging mobile device hardware features. This project is the final assignment for the "Mobile Computing" course, with the theme "Food and Drink".

## Features

- Meal entry management: browse, search, add, and view detailed nutritional information
- Nutrition data display: calories, protein, carbs, fat, fiber, sugar, sodium, vitamin C
- Mobile hardware support:
  - Camera (capture food photos)
  - Location / geolocation (record meal places)
  - Text-to-speech (read nutrition summaries and help content)
  - Vibration and haptic feedback
- Accessibility support:
  - Light / dark theme switching
  - Large text mode
  - Screen reader semantic labels and announcements
- Data persistence: local database storage with optional mockapi.io integration

## Project Structure
FoodDrinkApp/
├── Models/ # Data models (FoodEntry)
├── Services/ # Service layer (DataManager, TextSpeaker, FontScaler, ApiConnector)
├── Views/ # Pages (HomePage, EntryDetailPage, AddEntryPage, DevicePage, UserPage)
├── AppShell.xaml # App navigation shell
├── App.xaml # App entry and global resources
└── MauiProgram.cs # MAUI program configuration

text

## Development Environment

- Visual Studio 2022 (with .NET MAUI workload installed)
- .NET 9.0
- Target platforms: Android, Windows

## How to Run and Debug

### 1. Clone the Repository

```bash
git clone https://github.com/HubuManchester/fooddrink-tannianfei-cyber.git
cd fooddrink-tannianfei-cyber
2. Open the Project
Open FoodDrinkApp/FoodDrinkApp.csproj or the solution file using Visual Studio 2022.

3. Configure mockapi.io (Optional)
To use an online data source, edit Services/ApiConnector.cs and add your mockapi.io endpoint URL:

csharp
public const string EndpointUrl = "https://xxxxx.mockapi.io/api/v1/foods";
If not configured, the app will use 16 built-in sample food entries.

4. Run the Project
Connect your Android device via USB and run the project directly from Visual Studio.

App Name: VitaLog
Author: Nianfei Tan
Course: Mobile Computing (6G6Z0014)
Submission Date: June 1, 2026