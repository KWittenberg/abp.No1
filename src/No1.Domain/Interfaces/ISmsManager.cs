using System.Threading.Tasks;

namespace No1.Interfaces;

public interface ISmsManager
{
    Task<string> SendSms(string recipient, string msg);
}