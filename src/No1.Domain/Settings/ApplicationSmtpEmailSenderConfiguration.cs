using Microsoft.Extensions.Options;
using No1.Options;
using System.Threading.Tasks;
using Volo.Abp.Emailing.Smtp;

namespace No1.Settings;

public class ApplicationSmtpEmailSenderConfiguration : ISmtpEmailSenderConfiguration
{
    private readonly SmtpOptions _smtpOptions;

    public ApplicationSmtpEmailSenderConfiguration(IOptions<SmtpOptions> smtpOptions)
    {
        _smtpOptions = smtpOptions.Value;
    }

    public Task<string> GetDefaultFromAddressAsync() => Task.FromResult(_smtpOptions.DefaultFromAddress);

    public Task<string> GetDefaultFromDisplayNameAsync() => Task.FromResult(_smtpOptions.DefaultFromDisplayName);

    public Task<string> GetDomainAsync() => Task.FromResult(_smtpOptions.Domain);

    public Task<bool> GetEnableSslAsync() => Task.FromResult(_smtpOptions.EnableSsl);

    public Task<string> GetHostAsync() => Task.FromResult(_smtpOptions.Host);

    public Task<string> GetPasswordAsync() => Task.FromResult(_smtpOptions.Password);

    public Task<int> GetPortAsync() => Task.FromResult(_smtpOptions.Port);

    public Task<bool> GetUseDefaultCredentialsAsync() => Task.FromResult(_smtpOptions.UseDefaultCredentials);

    public Task<string> GetUserNameAsync() => Task.FromResult(_smtpOptions.UserName);
}