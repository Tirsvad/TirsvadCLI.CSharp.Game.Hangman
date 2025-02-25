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
            new CultureInfo("da")
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

        private static void MenuLanguagesSelect()
        {
            int i = 1;
            foreach (CultureInfo Language in Languages)
            {
                Console.WriteLine(i + " " + Language.EnglishName);
                i++;
            }

            Console.WriteLine();
            Console.ReadLine();
        }

        internal static void Menu()
        {
            // Menu loop
            while (true)
            {
                Console.Clear();
                ShowMessage("MSG_WELCOME");
                Console.WriteLine();
                // Show the main menu
                foreach (var item in Constants.MENU_MAIN[cultureNameTwoLetter])
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine();
                ShowMessage("MSG_ENTER_CHOICE", false);
                //    // Get the user's choice
                ConsoleKeyInfo choice = Console.ReadKey();
                // Check the user's choice
                switch (choice.KeyChar)
                {
                    case '1':
                        //gameEngine.StartGame();
                        break;
                    case '2':
                        //gameEngine.ShowHighScores();
                        break;
                    case '3':
                        //gameEngine.MenuSettings();
                        break;
                    case '9':
                        Console.Clear();
                        //Console.WriteLine(Constants.MSG_WELCOME);
                        foreach (var item in Constants.MSG_HELP[cultureNameTwoLetter])
                        {
                            Console.WriteLine(item);
                        }
                        Console.WriteLine();
                        //Console.WriteLine(Constants.MSG_PRESS_ANY_KEY);
                        Console.ReadKey();
                        break;
                    case '0':
                        //gameEngine.ExitGame();
                        return;
                    default:
                        Console.WriteLine("\n");
                        ShowErrorMessage("ERR_INVALID_INPUT");
                        ShowMessage("MSG_PRESS_ANY_KEY");
                        //Console.WriteLine(Constants.ERR_INVALID_INPUT);
                        //Console.WriteLine(Constants.MSG_PRESS_ANY_KEY);
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
            }
        }

        internal static void ShowMessage(string message, bool newLine = true)
        {
            Console.Write(Constants.MESSAGES[key: cultureNameTwoLetter][message]);
            if (newLine)
            {
                Console.WriteLine();
            }
        }

        internal static void ShowErrorMessage(string message, bool newLine = true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Constants.ERROR_MESSAGES[key: cultureNameTwoLetter][message]);
            if (newLine)
            {
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
