namespace Hangman
{
    class GameEngine
    {
        string[] words;
        string word;
        string wordExplanation;
        string wordToGuess;
        bool normalGame = false;
        bool gameRunning = true;
        int lives = Constants.MAX_TRIES;

        public GameEngine() { }

        internal string ExitGame()
        {
            //return Constants.MESSAGES["EXIT_GAME"];
            return "";
        }

        internal void ShowHighScores()
        {
            throw new NotImplementedException();
        }

        internal void StartGame(bool normalGame = false)
        {
            SetWords();
            SetWordToGuess();
            this.normalGame = normalGame;
            do
            {
                Console.Clear();
                Program.ShowMessage("MSG_TITLE");
                Console.WriteLine();
                Program.ShowMessage("MSG_GUESS_A_WORD");
                Console.WriteLine();

                //Console.WriteLine(word);
                Console.WriteLine(Constants.HANGMAN_ASCII[Constants.MAX_TRIES - lives]);

                Console.WriteLine();
                Console.WriteLine(wordToGuess);

                // Check if the word is guessed
                if (wordToGuess == word)
                {
                    Console.WriteLine();
                    Program.ShowMessage("MSG_WIN");
                    Program.ShowMessage("MSG_PRESS_A_KEY");
                    Program.ShowMessage("MSG_PRESS_ESC_FOR_MENU");
                    ConsoleKeyInfo continueGame = Console.ReadKey();
                    if (continueGame.Key == ConsoleKey.Escape)
                    {
                        word = string.Empty;
                        return;
                    }
                    else
                    {
                        SetWordToGuess();
                    }
                }
                else
                {
                    ConsoleKeyInfo guess = Console.ReadKey();
                    char guessedChar = char.ToLower(guess.KeyChar);

                    if (word.Contains(guessedChar))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Correct guess");
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (word[i] == guess.KeyChar)
                            {
                                wordToGuess = wordToGuess.Remove(i, 1).Insert(i, guess.KeyChar.ToString());
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Wrong guess");
                        lives--;
                        Console.WriteLine("Lives: " + lives);
                        if (lives == 0)
                        {
                            Console.Clear();
                            Program.ShowMessage("MSG_TITLE");
                            Console.WriteLine("\n\n");
                            Console.WriteLine(Constants.HANGMAN_ASCII[Constants.MAX_TRIES - lives]);
                            Console.WriteLine();
                            Program.ShowMessage("MSG_LOSE");
                            Console.WriteLine(word);
                            Program.ShowMessage("MSG_PRESS_A_KEY");
                            Program.ShowMessage("MSG_PRESS_ESC_FOR_MENU");
                            ConsoleKeyInfo continueGame = Console.ReadKey();
                            lives = Constants.MAX_TRIES;
                            if (continueGame.Key == ConsoleKey.Escape)
                            {
                                word = string.Empty;
                                return;
                            }
                            else
                            {
                                SetWordToGuess();
                            }
                        }
                    }

                }

            } while (gameRunning);
        }

        internal void SetWords()
        {
            string file;
            if (normalGame)
            {
                file = Constants.WORDS_FILE;
            }
            else
            {
                file = Constants.WORDS_FILE_COMPUTER_SCIENCE;
            }

            words = System.IO.File.ReadAllLines(file);
        }

        internal void SetWordToGuess()
        {
            Random random = new Random();
            word = words[random.Next(words.Length)];
            if (!normalGame)
            {
                wordExplanation = word.Split(';')[1];
                word = word.Split(';')[0];
            }
            wordToGuess = new string('-', word.Length);
        }
    }
}
