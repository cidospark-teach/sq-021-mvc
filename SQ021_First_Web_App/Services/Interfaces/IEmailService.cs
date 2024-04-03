namespace SQ021_First_Web_App.Services.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendEmail(string senderEmail,  string subject, string body);
    }
}
