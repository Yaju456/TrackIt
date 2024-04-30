using Microsoft.AspNetCore.Identity.UI.Services;

namespace FYPV1.ViewModel
{
    public class EmailSender : IEmailSender
    {
        Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
