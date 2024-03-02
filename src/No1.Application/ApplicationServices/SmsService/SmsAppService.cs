using No1.Interfaces;
using System.Threading.Tasks;

namespace No1.ApplicationServices.SmsService;

public class SmsAppService(ISmsManager smsManager) : No1AppService
{
    public async Task<string> SendSms(string recipient = "0038598870888", string msg = "This is Test Sms Message.")
    {
        return await smsManager.SendSms(recipient, msg);
    }
}