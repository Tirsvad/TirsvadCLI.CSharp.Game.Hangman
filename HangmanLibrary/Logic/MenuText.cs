using System.Text.Json;
using HangmanLibrary.Models;
using Microsoft.Extensions.Logging;

namespace HangmanLibrary.Logic;

public class MenuText : IMenuText
{
    private readonly ILogger<MenuText> _log;
    private readonly JsonSerializerOptions options;

    public MenuText(ILogger<MenuText> log)
    {
        _log = log;

        // CamelCase is the default for System.Text.Json
        // PascalCase is the default for CSharp
        // This is a workaround for the JSON file
        options = new()
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    public List<string> Menu(string language)
    {
        return LookupMenu("Menu", language);
    }

    public List<string> Settings(string language)
    {
        return LookupMenu("Settings", language);
    }

    private List<string> LookupMenu(string key, string language)
    {
        try
        {
            List<MenuMsg>? messageSets = JsonSerializer
                .Deserialize<List<Models.MenuMsg>>
                (
                    File.ReadAllText("MenuText.json"), options
                );

            MenuMsg? menuText = messageSets?.Where(x => x.Language == language).First();

            if (menuText is null)
            {
                throw new NullReferenceException("No message set found for language");
            }

            if (menuText.Translations[key].ValueKind == JsonValueKind.Array)
            {
                List<string> menuList = new();
                foreach (var item in menuText.Translations[key].EnumerateArray())
                {
                    if (item.ValueKind == JsonValueKind.String)
                    {
                        menuList.Add(item.GetString());
                    }
                    else
                    {
                        throw new InvalidOperationException("Expected a string value in the array");
                    }
                }
                return menuList;
            }
            else
            {
                throw new InvalidOperationException("Expected an array value");
            }
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Error looking up menu list");
            throw;
        }
    }
}
