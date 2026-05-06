namespace Easy_Learn.UI.Contracts
{
    public interface IMessageSender
    {
        Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false);
    }
}
