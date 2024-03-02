using No1.Interfaces;
using No1.Templates.Email.TestEmail;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.TextTemplating;

namespace No1.Email;

public class EmailManager(IEmailSender emailSender, ITemplateRenderer templateRenderer) : IEmailManager, ITransientDependency
{
    public async Task SendTestEmail(string from, string to, string? subject, string? body)
    {
        await emailSender.SendAsync(from, to, subject, body, true);
    }

    public async Task SendTestEmail(TestEmailModel input)
    {
        await emailSender.SendAsync(input.FromEmail, input.ToEmail, input.Subject, input.Body, true);
    }

    public async Task SendTestEmailWithName(TestEmailModel input)
    {
        try
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(input.FromEmail, input.FromName);
            mail.To.Add(new MailAddress(input.ToEmail, input.ToName));
            mail.Subject = input.Subject;
            mail.Body = input.Body;
            mail.IsBodyHtml = true;

            await emailSender.SendAsync(mail);
        }
        catch (Exception ex)
        {
            throw new Exception("Error while sending Email", ex);
        }
    }

    public async Task SendTestEmailWithNameAndTemplate(TestEmailModel input)
    {
        try
        {
            var html = await templateRenderer.RenderAsync("TestEmail", new TestEmailModel
            {
                FromName = input.FromName,
                FromEmail = input.FromEmail,
                ToName = input.ToName,
                ToEmail = input.ToEmail,
                Subject = input.Subject,
                Body = input.Body
            });

            await emailSender.SendAsync(input.ToEmail, "TestEmail", html);
        }
        catch (Exception ex)
        {
            throw new Exception("Error while sending Email", ex);
        }
    }

    public async Task SendTestEmailWithNameAndTemplateV2(TestEmailModel input)
    {
        try
        {
            var bodyHtml = await templateRenderer.RenderAsync("TestEmail", input);

            var mail = new MailMessage();
            mail.From = new MailAddress(input.FromEmail, input.FromName);
            mail.To.Add(new MailAddress(input.ToEmail, input.ToName));
            mail.Subject = input.Subject;
            mail.Body = bodyHtml;
            mail.IsBodyHtml = true;

            await emailSender.SendAsync(mail);
        }
        catch (Exception ex)
        {
            throw new Exception("Error while sending Email", ex);
        }
    }

    public async Task SendTestEmailWithNameAndTemplateV3(TestEmailModel input)
    {
        try
        {
            var bodyHtml = await templateRenderer.RenderAsync("TestEmail", input);
            //var avHtml = CreateAlternateView(bodyHtml);

            var mail = new MailMessage();
            //mail.AlternateViews.Add(avHtml);
            mail.From = new MailAddress(input.FromEmail, input.FromName);
            mail.To.Add(new MailAddress(input.ToEmail, input.ToName));
            mail.Subject = input.Subject;
            mail.Body = bodyHtml;
            mail.IsBodyHtml = true;

            await emailSender.SendAsync(mail);

            //var calendarItem = CreateIcs(
            //    model.CustomerCompanyName,
            //    recipient,
            //    model.JobTitle,
            //    model.CandidateName,
            //    "",
            //    DateTime.ParseExact($"{model.Date} {model.Time}", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
            //    model.MeetingLocation,
            //    $"Vorstellungsgespräch - {model.JobTitle} mit {model.CandidateName}");

            //var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(calendarItem));
            //var attachment = new Attachment(memoryStream, "Vorstellungsgespraech.ics", "text/calendar");

            //mail.Attachments.Add(attachment);

        }
        catch (Exception ex)
        {
            throw new Exception("Error while sending Email", ex);
        }
    }







    public async Task SendTestEmailSimple(TestEmailModel input)
    {
        try
        {
            var mail = new MailMessage();

            mail.From = new MailAddress("customerservice@yellowshark.com");
            mail.To.Add("kresimir.vitenberg@factory-x.hr");
            mail.Subject = "Testna poruka ABP.IO";
            mail.Body = "Ovo je testna poruka poslana putem customerservice@yellowshark.com";

            await emailSender.SendAsync(mail);
        }
        catch (Exception ex)
        {
            throw new Exception("Error while sending Email", ex);
        }
    }


    //public async Task SendConfirmRegistration(IdentityUser user, IdentityRole adminRole, string confirmationToken)
    //{
    //    var confirmationUrl = new UriBuilder($"{_authenticationApplicationUrl}/identity/confirm-registration");

    //    var query = HttpUtility.ParseQueryString(confirmationUrl.Query);

    //    query["email"] = user.Email;
    //    query["confirmationToken"] = confirmationToken;
    //    query["returnUrl"] = user.IsInRole(adminRole.Id) ? _administrationApplicationUrl : _userApplicationUrl;

    //    confirmationUrl.Query = query.ToString();

    //    var confirmRegistrationHtml = await _templateRenderer.RenderAsync("ConfirmRegistration",
    //        new ConfirmRegistrationModel
    //        {
    //            ConfirmationUrl = confirmationUrl.ToString(),
    //            Name = user.Name,
    //            Surname = user.Surname
    //        });

    //    await _emailSender.SendAsync(user.Email, "Confirm Your Registration", confirmRegistrationHtml);
    //}

    //public async Task SendUserPasswordResetCode(IdentityUser user, IdentityRole adminRole, string passwordResetToken)
    //{
    //    var forgottenPasswordUrl = new UriBuilder($"{_authenticationApplicationUrl}/identity/reset-password");

    //    var query = HttpUtility.ParseQueryString(forgottenPasswordUrl.Query);

    //    query["email"] = user.Email;
    //    query["passwordResetToken"] = passwordResetToken;
    //    query["returnUrl"] = user.IsInRole(adminRole.Id) ? _administrationApplicationUrl : _userApplicationUrl;

    //    forgottenPasswordUrl.Query = query.ToString();

    //    var forgotPasswordHtml = await _templateRenderer.RenderAsync("PasswordResetCode", new PasswordResetCodeModel
    //    {
    //        ForgottenPasswordUrl = forgottenPasswordUrl.ToString()
    //    });

    //    await _emailSender.SendAsync(user.Email, "Password Reset Code", forgotPasswordHtml);
    //}

    //public async Task SendEmailWithCalendarEvent(SendMailModel input, string bodyHtml, string calendarEvent)
    //{
    //    try
    //    {
    //        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(calendarEvent));
    //        var attachment = new Attachment(memoryStream, input.AttachmentName, "text/calendar");

    //        var mail = new MailMessage();
    //        mail.From = new MailAddress(input.OrganizerEmail, input.OrganizerName);
    //        mail.To.Add(new MailAddress(input.AttendeeEmail, input.AttendeeName));
    //        mail.Subject = input.Summary;
    //        mail.IsBodyHtml = true;
    //        mail.Priority = MailPriority.High;
    //        mail.Body = bodyHtml;
    //        mail.Attachments.Add(attachment);

    //        await _emailSender.SendAsync(mail);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception("Error while sending EmailWithCalendarEvent", ex);
    //    }
    //}
}