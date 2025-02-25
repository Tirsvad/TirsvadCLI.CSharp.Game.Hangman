using System.Globalization;
using System.Text.Json;

namespace Hangman
{
    internal class Program
    {
        // Create a new game engine
        static GameEngine? gameEngine; //!> This is a new instance of the GameEngine class

        static CultureInfo[] Languages = new CultureInfo[]
        {
            new CultureInfo("en"),
            new CultureInfo("da"),
            new CultureInfo("de"),
            new CultureInfo("es"),
            new CultureInfo("fr"),
            new CultureInfo("it"),
            new CultureInfo("nl"),
            new CultureInfo("pl"),
            new CultureInfo("pt"),
            new CultureInfo("ru"),
            new CultureInfo("sv"),
        };

        static CultureInfo? cultureInfo;
        static string cultureNameTwoLetter = "en";

        static void Main(string[] args)
        {
            // Set console settings
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Set console encoding to UTF-8 so we can use special char to show fruits
            Console.CursorVisible = false; // Hide the cursor

            // Load settings from file
            LoadSettings();

            gameEngine = new GameEngine();

            // Show the main menu
            Menu();
        }

        private static void LoadSettings()
        {
            string settingsFilePath = "settings.json"; // Replace with your actual settings file path

            if (File.Exists(settingsFilePath))
            {
                Console.WriteLine("Settings file exists.");
                string json = File.ReadAllText(settingsFilePath);
                Settings? settings = JsonSerializer.Deserialize<Settings>(json);
                if (settings != null && !string.IsNullOrEmpty(settings.Culture))
                {
                    cultureInfo = new CultureInfo(settings.Culture);
                    cultureNameTwoLetter = cultureInfo.TwoLetterISOLanguageName;
                }
            }
            else
            {
                // Create file with default settings
                Console.WriteLine("Settings file does not exist. Creating a new one with default settings.");
                cultureInfo = new CultureInfo("en");
                cultureNameTwoLetter = cultureInfo.TwoLetterISOLanguageName;
                SaveSettings();
            }
        }

        private static void SaveSettings()
        {
            string settingsFilePath = "settings.json"; // Replace with your actual settings file path
            Settings settings = new Settings
            {
                Culture = cultureInfo?.Name
            };
            string json = JsonSerializer.Serialize(settings);
            File.WriteAllText(settingsFilePath, json);
        }

        internal static void Menu()
        {
            // Menu loop
            while (true)
            {
                int elementCounter = 0;

                Console.Clear();
                ShowMessage("MSG_WELCOME");
                Console.WriteLine();
                // Show the main menu

                ShowMenuItem("F1", "MAIN_MENU_START_GAME");
                ShowMenuItem("F2", "MAIN_MENU_HIGH_SCCORE");
                ShowMenuItem("F3", "MAIN_MENU_SEETINGS");
                ShowMenuItem("F9", "HELP");
                ShowMenuItem("ESC", "EXIT");

                Console.WriteLine();
                ShowMessage("MSG_ENTER_CHOICE", false);
                //    // Get the user's choice
                ConsoleKeyInfo choice = Console.ReadKey();
                // Check the user's choice
                switch (choice.Key)
                {
                    case ConsoleKey.F1:
                        //gameEngine.StartGame();
                        break;
                    case ConsoleKey.F2:
                        //gameEngine.ShowHighScores();
                        break;
                    case ConsoleKey.F3:
                        MenuSettings();
                        break;
                    case ConsoleKey.F9:
                        Console.Clear();
                        ShowMessage("MSG_WELCOME");
                        foreach (var item in Constants.MSG_HELP[cultureNameTwoLetter])
                        {
                            Console.WriteLine(item);
                        }
                        Console.WriteLine();
                        ShowMessage("MSG_PRESS_ANY_KEY");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.Escape:
                        //gameEngine.ExitGame();
                        return;
                    default:
                        Console.WriteLine("\n");
                        ShowErrorMessage("ERR_INVALID_INPUT");
                        ShowMessage("MSG_PRESS_ANY_KEY");
                        Console.ReadKey();
                        break;
                }
            }
        }

        internal static void MenuSettings()
        {
            while (true)
            {
                Console.Clear();

                // Show the settings menu
                ShowMessage("MSG_MENU_SETTINGS");
                Console.WriteLine();

                ShowMenuItem("F1", "SETTINGS_MENU_CHANGE_LAMGUAGE");
                ShowMenuItem("F2", "SETTINGS_MENU_NORMAL_GAME");
                ShowMenuItem("F3", "SETTINGS_MENU_SPECIAL_AP_COMPUTER_SCIENCE_EDITION");
                ShowMenuItem("F10", "BACK");
                Console.WriteLine();

                ShowMessage("MSG_ENTER_CHOICE", false);

                // Get the user's choice
                ConsoleKeyInfo choice = Console.ReadKey();
                // Check the user's choice
                switch (choice.Key)
                {
                    case ConsoleKey.F1:
                        MenuLanguagesSelect();
                        break;
                    case ConsoleKey.F2:
                        //NormalGame();
                        break;
                    case ConsoleKey.F3:
                        //ComputerScienceGame();
                        break;
                    case ConsoleKey.F10:
                        return;
                    default:
                        Console.WriteLine("\n");
                        ShowErrorMessage("ERR_INVALID_INPUT");
                        ShowMessage("MSG_PRESS_ANY_KEY");
                        Console.ReadKey();
                        break;
                }

            }
        }

        internal static void MenuLanguagesSelect()
        {
            int elementCounter = 0; //!< This is the element counter
            int i; //!< This is the i counter
            string errorMessage = string.Empty; //!< This is for the error message

            do
            {
                Console.Clear();

                // Show the settings menu
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ShowErrorMessage(errorMessage);
                    errorMessage = string.Empty;
                }

                // Show the language selection menu
                for (i = 1 + elementCounter; i < Languages.Length + 1; i++)
                {
                    if (i > elementCounter + 10)
                    {
                        break;
                    }

                    int ii = i - 1;

                    string option = $"F{(i - elementCounter)}";
                    Console.WriteLine($"{option} {Languages[ii].EnglishName}");
                }

                Console.WriteLine();

                // Show the navigation options
                if (elementCounter > 0)
                {
                    Console.WriteLine("F11: Previous page");
                }
                else if (i - 1 + elementCounter < Languages.Length)
                {
                    Console.WriteLine("F12: Next page");
                }

                Console.WriteLine();

                ShowMessage("MSG_ENTER_CHOICE", false);
                ConsoleKeyInfo choice = Console.ReadKey();

                // Check the user's choice
                if (choice.Key >= ConsoleKey.F1 && choice.Key <= ConsoleKey.F10)
                {
                    int selectedIndex = elementCounter + (choice.Key - ConsoleKey.F1);
                    if (selectedIndex < Languages.Length)
                    {
                        cultureInfo = Languages[selectedIndex];
                        cultureNameTwoLetter = cultureInfo.TwoLetterISOLanguageName;
                        SaveSettings();
                        return;
                    }

                    errorMessage = "ERR_INVALID_INPUT";
                }
                else
                {
                    switch (choice.Key)
                    {
                        case ConsoleKey.F11:
                            if (elementCounter > 0)
                            {
                                elementCounter -= 10;
                            }
                            break;
                        case ConsoleKey.F12:
                            if (elementCounter + 10 < Languages.Length)
                            {
                                elementCounter += 10;
                            }
                            break;
                        case ConsoleKey.Escape:
                            return;
                        default:
                            errorMessage = "ERR_INVALID_INPUT";
                            break;
                    }
                }
            } while (true);
        }

        internal static void ShowMessage(string message, bool newLine = true)
        {
            Console.Write(Constants.MESSAGES[key: cultureNameTwoLetter][message]);
            if (newLine)
            {
                Console.WriteLine();
            }
        }

        internal static void ShowMenuItem(string key, string message)
        {
            Console.WriteLine($"{key.PadRight(6)} {Constants.MENU_MESSAGES[key: cultureNameTwoLetter][message]}");
        }

        internal static void ShowErrorMessage(string message, bool newLine = true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string msg = Constants.ERROR_MESSAGES[key: cultureNameTwoLetter][message];
            Console.Write(msg);
            if (newLine)
            {
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
