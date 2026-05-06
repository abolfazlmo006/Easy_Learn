namespace Easy_learn.WebApi.Contracts.Services
{
    public interface IMessageSender
    {
        Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false);
    }
}
