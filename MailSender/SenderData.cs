using System.Text.Json;
using System.Text.Json.Serialization;

namespace MailSender;

public class SenderData
{
    [JsonPropertyName("senderMail")]
    public string SenderMail { get; set; } = "";

    [JsonPropertyName("senderName")]
    public string SenderName { get; set; } = "";

    [JsonPropertyName("key")]
    public string Key { get; set; } = "";

    public static SenderData FromJson(string path)
    {
        string json = File.ReadAllText(path);

        return JsonSerializer.Deserialize<SenderData>(json) ?? throw new Exception("Ошибка чтения файла конфигурации.");
    }
}
