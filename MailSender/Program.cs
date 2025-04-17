using System.Net;
using System.Net.Mail;

namespace MailSender;

public class Program
{
    public static void Main()
    {
        // READ CONFIG

        var senderData = SenderData.FromJson("uniq_key.json");

        // INPUT

        Console.WriteLine("Введите почту получателя:");
        string toAddress = Console.ReadLine() ?? "";

        Console.WriteLine("Введите тему письма:");
        string subject = Console.ReadLine() ?? "Без темы";

        Console.WriteLine("Введите текст:");
        string text = Console.ReadLine() ?? "Пустое письмо.";

        // MESSAGE SETTINGS

        var fromMail = new MailAddress(senderData.SenderMail, senderData.SenderName);
        var toMail = new MailAddress(toAddress);
        
        MailMessage message = new(fromMail, toMail)
        {
            Subject = subject,
            Body = text,
            IsBodyHtml = false
        };

        // SMTP CLIENT SETTINGS

        SmtpClient smtpClient = new(SmtpSettings.Host, SmtpSettings.Port)
        {
            Credentials = new NetworkCredential(senderData.SenderMail, senderData.Key),
            EnableSsl = true,
            Timeout = 10000
        };

        // MESSAGE SENDING

        try
        {
            smtpClient.Send(message);
            Console.WriteLine("Письмо успешно отправлено.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при отправке письма: {ex.Message}");
        }
    }
}