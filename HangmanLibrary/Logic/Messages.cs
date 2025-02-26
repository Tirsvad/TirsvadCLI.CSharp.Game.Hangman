using System.Text.Json;
using HangmanLibrary.Models;
using Microsoft.Extensions.Logging;

namespace HangmanLibrary.Logic;

public class Messages : IMessages
{
    private readonly ILogger<Messages> _log;
    private readonly JsonSerializerOptions options;

    public Messages(ILogger<Messages> log)
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

    public string LookupMsgText(string key, string language)
    {
        try
        {
            List<TextMsg>? messageSets = JsonSerializer
                .Deserialize<List<TextMsg>>
                (
                    File.ReadAllText("MsgText.json"), options
                );

            // Lambda expression (x => x.Language == language)
            // x is a parameter representing each element in the collection messageSets
            // x.Language accesses the Language property of the current TextMsg object.
            // x.Language == language is a condition that checks if the Language property
            // of the current TextMsg object matches the value of the language variable.
            TextMsg? messages = messageSets?.Where(x => x.Language == language).First();

            if (messages is null)
            {
                throw new NullReferenceException("No message set found for language");
            }

            return messages.Translations[key];
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Error looking up message text");
            throw;
        }
    }
}
