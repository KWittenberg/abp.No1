using No1.Templates.Email.TestEmail;
using System.Threading.Tasks;

namespace No1.Interfaces;

public interface IEmailManager
{
    Task SendTestEmail(string from, string to, string? subject, string? body);

    Task SendTestEmail(TestEmailModel input);

    Task SendTestEmailWithName(TestEmailModel input);

    Task SendTestEmailWithNameAndTemplate(TestEmailModel input);

    Task SendTestEmailWithNameAndTemplateV2(TestEmailModel input);

    //Task SendConfirmRegistration(IdentityUser user, IdentityRole adminRole, string confirmationToken);

    //Task SendUserPasswordResetCode(IdentityUser user, IdentityRole adminRole, string passwordResetToken);

    //Task SendEmailWithCalendarEvent(SendMailModel input, string bodyHtml, string calendarEvent);
}