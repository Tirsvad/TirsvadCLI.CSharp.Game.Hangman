namespace Hangman
{
    static class Constants
    {
        #region Configuration
        public const int MAX_TRIES = 6;
        public const string WORDS_FILE = "words.txt";
        public const string WORDS_FILE_COMPUTER_SCIENCE = "words_computer_science.txt";
        #endregion

        #region Messages
        public static readonly Dictionary<string, Dictionary<string, string>> MESSAGES = new Dictionary<string, Dictionary<string, string>>
        {
            { "en", new Dictionary<string, string>
                {
                    { "MSG_WELCOME", "Welcome to Hangman!" },
                    { "MSG_ENTER_CHOICE", "Please enter your choice: " },
                    { "MSG_ENTER_NAME", "Please enter your name: " },
                    { "MSG_WIN", "Congratulations! You won!" },
                    { "MSG_LOSE", "You lost! The word was: " },
                    { "MSG_EXIT", "Thank you for playing.\nSee you soon again." },
                    { "MSG_PRESS_ANY_KEY", "Press any key to continue..." },
                }
            },
            { "da", new Dictionary<string, string>
                {
                    { "MSG_WELCOME", "Velkommen til Hangman!" },
                    { "MSG_ENTER_CHOICE", "Indtast dit valg: " },
                    { "MSG_ENTER_NAME", "Indtast dit navn: " },
                    { "MSG_WIN", "Tillykke! Du vandt!" },
                    { "MSG_LOSE", "Du tabte! Ordet var: " },
                    { "MSG_EXIT", "Tak for at spille.\nVi ses snart igen." },
                    { "MSG_PRESS_ANY_KEY", "Tryk på en vilkårlig tast for at fortsætte..." },
                }
            }
        };

        public static readonly Dictionary<string, string[]> MENU_MAIN = new Dictionary<string, string[]>
        {
            { "en", new string[]
                {
                    "1. Start Game",
                    "2. High Scores",
                    "3. Settings",
                    "9. Help",
                    "0. Exit"
                }
            },
            { "da", new string[]
                {
                    "1. Start spil",
                    "2. Høje scorer",
                    "3. Indstillinger",
                    "9. Hjælp",
                    "0. Afslut"
                }
            }
        };

        public static readonly Dictionary<string, string[]> MSG_HELP = new Dictionary<string, string[]>
        {
            { "en", new string[]
                {
                    "The goal of the game is to guess the word by suggesting letters.",
                    $"You have {MAX_TRIES} tries to guess the word.",
                    "Good luck!"
                }
            },
            { "da", new string[]
                {
                    "Målet med spillet er at gætte ordet ved at foreslå bogstaver.",
                    $"Du har {MAX_TRIES} forsøg til at gætte ordet.",
                    "Held og lykke!"
                }
            }
        };
        #endregion

        #region Error messages
        public static readonly Dictionary<string, Dictionary<string, string>> ERROR_MESSAGES = new Dictionary<string, Dictionary<string, string>>
        {
            { "en", new Dictionary<string, string>
                {
                    { "ERR_INVALID_INPUT", "Invalid input. Please try again." }
                }
            },
            { "da", new Dictionary<string, string>
                {
                    { "ERR_INVALID_INPUT", "Ugyldigt input. Prøv venligst igen." }
                }
            }
        };
        #endregion

        #region Hangman ASCII art
        public static readonly string[] HANGMAN_ASCII = new string[]
        {
            "  +---+",
            "  |   |",
            "      |",
            "      |",
            "      |",
            "      |",
            "=========",
            "  +---+",
            "  |   |",
            "  O   |",
            "      |",
            "      |",
            "      |",
            "=========",
            "  +---+",
            "  |   |",
            "  O   |",
            "  |   |",
            "      |",
            "      |",
            "=========",
            "  +---+",
            "  |   |",
            "  O   |",
            " /|   |",
            "      |",
            "      |",
            "=========",
             "  +---+",
             "  |   |",
             "  O   |",
            @" /|\  |",
             "      |",
             "      |",
             "=========",
             "  +---+",
             "  |   |",
             "  O   |",
            @" /|\  |",
             " /    |",
             "      |",
             "=========",
             "  +---+",
             "  |   |",
             "  O   |",
            @" /|\  |",
            @" / \  |",
             "      |",
             "========="
        };
        #endregion

    }
}
