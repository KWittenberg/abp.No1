namespace No1.Options;

public class SmtpOptions
{
    public const string Section = "Smtp";

    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public string Domain { get; set; } = string.Empty;
    public bool EnableSsl { get; set; } = false;
    public bool UseDefaultCredentials { get; set; } = false;
    public string DefaultFromAddress { get; set; } = string.Empty;
    public string DefaultFromDisplayName { get; set; } = string.Empty;
}