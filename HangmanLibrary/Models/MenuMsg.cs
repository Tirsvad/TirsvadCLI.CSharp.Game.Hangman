using System.Text.Json;

namespace HangmanLibrary.Models;

public class MenuMsg
{
    public required string Language { get; set; }
    public required Dictionary<string, JsonElement> Translations { get; set; }
}
