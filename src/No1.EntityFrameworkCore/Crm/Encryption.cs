using System;
using System.Linq;
using No1.Interfaces;
using Volo.Abp.DependencyInjection;

namespace No1.Crm;

public class Encryption : IEncryption, ITransientDependency
{
    public string Decrypt(string input)
    {
        var temp = Convert.FromBase64String(input);
        var plainTextBytes = System.Text.Encoding.UTF8.GetChars(temp);
        return plainTextBytes.Select(chr => Convert.ToInt32(chr) - 2).Aggregate("", (current, val) => current + (char)val);
    }

    public string Encrypt(string input)
    {
        var result = input.Select(chr => Convert.ToInt32(chr) + 2).Aggregate("", (current, val) => current + (char)val);
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(result);
        return Convert.ToBase64String(plainTextBytes);
    }
}