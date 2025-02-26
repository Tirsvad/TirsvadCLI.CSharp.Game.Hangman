
namespace HangmanLibrary.Logic
{
    public interface IMenuText
    {
        List<string> Menu(string language);

        List<string> Settings(string language);
    }
}