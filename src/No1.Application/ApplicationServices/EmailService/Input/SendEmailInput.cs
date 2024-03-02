using System;
using System.ComponentModel;

namespace No1.ApplicationServices.EmailService.Input;

public class SendEmailInput
{
    [DefaultValue("Krešimir Wittenberg")]
    public string? FromName { get; set; }

    [DefaultValue("kejo@net.hr")]
    public string FromEmail { get; set; }

    [DefaultValue("Krešimir Vitenberg")]
    public string ToName { get; set; }

    [DefaultValue("kresimir.vitenberg@factory-x.hr")]
    public string ToEmail { get; set; }

    [DefaultValue("TEST Email")]
    public string Subject { get; set; }

    [DefaultValue("This is Basic TEST Email !")]
    public string Body { get; set; }

    public DateTime SendDate { get; set; }
}