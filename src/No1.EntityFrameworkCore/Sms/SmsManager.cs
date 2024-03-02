using Microsoft.Extensions.Options;
using No1.Interfaces;
using No1.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace No1.Sms;

public class SmsManager(IOptions<SmsOptions> smsOptions) : ISmsManager, ITransientDependency
{
    private readonly SmsOptions _smsOptions = smsOptions.Value;

    public async Task<string> SendSms(string recipient, string msg)
    {
        try
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _smsOptions.BaseUrl);

            //var smsRecipient = !string.IsNullOrEmpty(_testSmsRecipient) ? _testSmsRecipient: recipient;
            var smsRecipient = recipient;
            if (smsRecipient.StartsWith("+")) smsRecipient.Replace("+", "00");

            request.Content = new StringContent($"ACCOUNT={_smsOptions.Username}&PASSWORD={_smsOptions.Password}&NUMBER={smsRecipient}&CMD=SENDMESSAGE&message={msg}");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while sending Sms", ex);
        }
    }
}