using HangmanLibrary.Logic;

namespace Hangman
{
    class GameEngine
    {
        private int Tries { get; set; } = 0;
        private int Lives { get; set; } = Constants.MAX_TRIES;

        private int Score { get; set; } = 0;
        private string GameMode { get; set; } = "Normal";
        private string GameType { get; set; } = "APCS";

        private string Word { get; set; }
        private string WordExplanation { get; set; }
        private string WordToGuess { get; set; }

        private string[] Words { get; set; }

        private string Lang { get; set; } = "en"; //!< The language of the application is set to English by default
        private IMessages Messages { get; set; }

        public GameEngine(IMessages Messages, string language)
        {
            Words = Array.Empty<string>();
            Word = string.Empty;
            WordExplanation = string.Empty;
            WordToGuess = string.Empty;
            this.Messages = Messages; // Initialize the Messages property
            Lang = language; // Set the language of the game engine
        }

        public void StartGame()
        {
            Console.WriteLine("Starting game...");
            LoadWords();
            SetWordToGuess();

            do
            {
                Console.Clear();
                Console.WriteLine("Hangman");
                Console.WriteLine();

                Console.WriteLine(Constants.HANGMAN_ASCII[Constants.MAX_TRIES - Lives]);

                Console.WriteLine();
                Console.WriteLine(WordToGuess);

                // Check if the word is guessed
                if (WordToGuess.ToLower() == Word.ToLower())
                {
                    Console.WriteLine();
                    Console.WriteLine(LookupMsgText("YouWon"));
                    Console.WriteLine(LookupMsgText("WordExplanation: ") + WordExplanation);
                    Console.WriteLine(LookupMsgText("Score: ") + Score);
                    Console.WriteLine();
                    Console.WriteLine(LookupMsgText("PlayAgain"));
                    Console.WriteLine(LookupMsgText("Exit"));

                    ConsoleKeyInfo continueGame = Console.ReadKey();
                    if (continueGame.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine(continueGame.ToString());
                        break;
                    }
                    else
                    {
                        Lives++;
                        SetWordToGuess();
                        Lives = Constants.MAX_TRIES;
                    }
                }
                else
                {
                    ConsoleKeyInfo guess = Console.ReadKey();
                    char guessedChar = char.ToLower(guess.KeyChar);
                    string word = Word.ToLower();
                    if (word.Contains(guessedChar))
                    {
                        Console.WriteLine();
                        Console.WriteLine(LookupMsgText("CorrectGuess"));
                        for (int i = 0; i < Word.Length; i++)
                        {
                            if (word[i] == guess.KeyChar)
                            {
                                WordToGuess = WordToGuess.Remove(i, 1).Insert(i, guess.KeyChar.ToString());
                            }
                        }
                    }
                    else
                    {
                        Lives--;
                    }
                }
            } while (Lives > 0);
            Console.WriteLine(LookupMsgText("YouLost"));
            Console.WriteLine($"{LookupMsgText("CorrectWordIs")} {Word}");
            Console.ReadKey();
            ResetGame();
        }

        private void LoadWords()
        {
            string file;
            if (GameType == "APCS")
            {
                file = Constants.WORDS_FILE_COMPUTER_SCIENCE;
            }
            else
            {
                file = Constants.WORDS_FILE;
            }
            Words = System.IO.File.ReadAllLines(file);
        }

        private void SetWordToGuess()
        {
            Random random = new Random();
            Word = Words[random.Next(Words.Length)];
            if (GameType == "APCS")
            {
                WordExplanation = Word.Split(';')[1];
                Word = Word.Split(';')[0];
            }
            WordToGuess = new string('-', Word.Length);
        }

        public void ResetGame()
        {
            Lives = Constants.MAX_TRIES;
            Score = 0;
            SetWordToGuess();
        }

        public bool ChangeGameMode(string s)
        {
            if (Constants.GAME_MODE.Contains(s))
            {
                GameMode = s;
                return true;
            }
            return false;
        }

        private string LookupMsgText(string msg)
        {
            return Messages.LookupMsgText(msg, Lang);
        }
    }
}
