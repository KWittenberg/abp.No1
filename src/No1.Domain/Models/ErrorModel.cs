namespace No1.Models;

public class ErrorModel
{
    public Error error { get; set; }
}



public class Error
{
    public string code { get; set; }
    
    public string message { get; set; }
    
    public Innererror innererror { get; set; }
}


public class Innererror
{
    public string message { get; set; }
    
    public string type { get; set; }
    
    public string stacktrace { get; set; }
}