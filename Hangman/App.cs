using System.Globalization;
using System.Text.Json;
using Hangman.Models;
using HangmanLibrary.Logic;

namespace Hangman;

class App
{
    // Translations object
    private readonly IMessages _messages; //!< The messages for translation
    private readonly IMenuText _menuText; //!< The menu text for translation

    // Language object
    private CultureInfo[] Languages = new CultureInfo[]
    {
            new CultureInfo("en"),
            new CultureInfo("da"),
    }; //!< The languages that the application supports
    private CultureInfo? CultureInfo; //!< The culture of the application
    private string Lang { get; set; } = "en"; //!< The language of the application is set to English by default

    private string GameMode { get; set; } = "Normal"; //!< The game mode of the application
    private string GameType { get; set; } = "APCS"; //!< The game type of the application

    private GameEngine Engine { get; set; } //!< The game engine

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="messages"></param>
    /// <param name="menuText"></param>
    public App(IMessages messages, IMenuText menuText)
    {
        this._messages = messages;
        this._menuText = menuText;
    }

    /// <summary>
    /// Runs the application
    /// </summary>
    /// <param name="args"></param>
    public void Run(string[] args)
    {
        // Set console settings
        Console.OutputEncoding = System.Text.Encoding.UTF8; // Set console encoding to UTF-8
        Console.CursorVisible = false; // Hide the cursor

        Engine = new GameEngine(_messages, Lang);

        LoadSettings();
        ShowMenu(); // Loop through the menu until the user exits the game
        SaveSettings();
    }

    #region Settings file methods

    /// <summary>
    /// Loads the settings from the settings file
    /// </summary>
    private void LoadSettings()
    {
        if (File.Exists(Constants.SETTINGS_FILE))
        {
            Console.WriteLine(LookupMsgText("LoadingSettings"));
            string json = File.ReadAllText(Constants.SETTINGS_FILE);
            Settings? settings = JsonSerializer.Deserialize<Settings>(json);
            if (settings != null && !string.IsNullOrEmpty(settings.Culture))
            {
                SetLanguage(settings.Culture);
            }
        }
        else
        {
            SaveSettings();
        }
    }

    /// <summary>
    /// Saves the settings to the settings file
    /// </summary>
    private void SaveSettings()
    {
        Console.WriteLine(LookupMsgText("SavingSettings"));
        Settings settings = new Settings
        {
            Culture = Lang,
            GameMode = GameMode,
            GameType = GameType
        };
        string json = JsonSerializer.Serialize(settings);
        File.WriteAllText(Constants.SETTINGS_FILE, json);
    }

    #endregion Settings file methods

    #region Language methods

    /// <summary>
    /// Checks if the language is valid
    /// </summary>
    /// <param name="lang"></param>
    /// <returns></returns>
    private bool IsLanguageValid(string lang)
    {
        return Languages.Any(l => l.Name.Equals(lang, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Sets the language
    /// </summary>
    /// <param name="lang"></param>
    /// <returns></returns>
    private void SetLanguage(string lang)
    {
        if (IsLanguageValid(lang))
        {
            CultureInfo = new CultureInfo(lang);
            Lang = CultureInfo.TwoLetterISOLanguageName;
        }
        else
        {
            CultureInfo = new CultureInfo("en");
            Lang = CultureInfo.TwoLetterISOLanguageName;
            Console.WriteLine("Invalid language. Defaulting to English.");
        }
    }

    /// <summary>
    /// Convert a list to a string with newlines
    /// </summary>
    /// <param name="l"></param>
    /// <returns></returns>
    private string ConvertListToString(List<string> l)
    {
        string result = "";
        foreach (var item in l)
        {
            result += item + "\n";
        }
        return result;
    }

    #endregion Language methods

    #region Menu methods

    /// <summary>
    /// Shows the menu
    /// </summary>
    private void ShowMenu()
    {
        const int pageSize = 10;
        while (true)
        {
            Console.Clear();
            List<string> menuItems = _menuText.Menu(Lang);
            int choice = PaganizesMenu(LookupMsgText("Greeting"), menuItems, pageSize);
            switch (choice)
            {
                case 0:
                    // Start game
                    Engine.StartGame();
                    break;
                case 1:
                    // High scores
                    break;
                case 2:
                    // Settings
                    ShowSettingMenu();
                    break;
                case 3:
                    // Help
                    break;
                case 4:
                    // Exit
                    return;
            }
        }
    }

    private void ShowSettingMenu()
    {
        const int pageSize = 10;

        Console.Clear();
        List<string> menuItems = _menuText.Settings(Lang);
        int choice = PaganizesMenu(LookupMsgText("SettingsMenu"), menuItems, pageSize);
        switch (choice)
        {
            case 0:
                // Change language
                ShowLanguageMenu();
                break;
            case 1:
                // Change Game mode
                break;
            case 2:
                // Change Game type
                break;
            case 3:
                // Back
                return;
        }
        return;
    }

    private void ShowLanguageMenu()
    {
        const int pageSize = 10;
        List<string> languages = new List<string>(); // Initialize the list
        Console.Clear();
        foreach (var item in Languages)
        {
            languages.Add(item.NativeName);
        }
        int choice = PaganizesMenu(LookupMsgText("LanguageMenu"), languages, pageSize); // Use 'languages' instead of 'menuItems'
        Lang = Languages[choice].TwoLetterISOLanguageName;
    }

    /// <summary>
    /// Paginates the menu
    /// </summary>
    /// <param name="title"></param>
    /// <param name="menuItems"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    private int PaganizesMenu(string title, List<string> menuItems, int pageSize)
    {
        int pageIndex = 0;
        string errorMessage = "";
        (int Left, int Top) Position;

        while (true)
        {
            Console.Clear();
            ShowTitle(title);
            Console.WriteLine();
            int totalPages = (int)Math.Ceiling(menuItems.Count / (double)pageSize);

            var pagedMenuItems = menuItems
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select((item, index) => $"F{index + 1}: {item}")
                .ToList();
            Console.WriteLine(ConvertListToString(pagedMenuItems));

            Console.WriteLine($"Page {pageIndex + 1} of {totalPages}");

            if (pageIndex > 0)
            {
                Console.WriteLine(LookupMsgText("F11PageBack"));
            }

            if (totalPages > pageIndex + 1)
            {
                Console.WriteLine(LookupMsgText("F12PageForward"));
            }

            if (errorMessage != "")
            {
                Position = Console.GetCursorPosition();
                Console.SetCursorPosition(0, Position.Top + 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errorMessage);
                Console.ResetColor();
                Console.SetCursorPosition(Position.Left, Position.Top);
                errorMessage = "";
            }

            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.F12 && pageIndex < totalPages - 1)
            {
                pageIndex++;
            }
            else if (key.Key == ConsoleKey.F11 && pageIndex > 0)
            {
                pageIndex--;
            }
            else if (key.Key >= ConsoleKey.F1 && key.Key <= ConsoleKey.F10)
            {
                int selectedIndex = key.Key - ConsoleKey.F1;
                if (selectedIndex < pagedMenuItems.Count)
                {
                    // Handle the selected menu item
                    Console.WriteLine($"Selected: {pagedMenuItems[selectedIndex]}  {selectedIndex + pageIndex * pageSize}");
                    return selectedIndex + pageIndex * pageSize;
                }
                else
                {
                    errorMessage = LookupMsgText("ErrorInvalidSelectionTryAgain");
                }
            }
            else
            {
                errorMessage = $"{LookupMsgText("ErrorInvalidKeyPart1")}. {LookupMsgText("ErrorInvalidKeyPart2")} F1-F{pagedMenuItems.Count} {LookupMsgText("ErrorInvalidKeyPart3")}.";
            }
        }
    }
    #endregion Menu methods

    #region String methods
    private void ShowTitle(string title)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(title);
        Console.ResetColor();
    }

    private string LookupMsgText(string msg)
    {
        return _messages.LookupMsgText(msg, Lang);
    }
    #endregion String methods
}
