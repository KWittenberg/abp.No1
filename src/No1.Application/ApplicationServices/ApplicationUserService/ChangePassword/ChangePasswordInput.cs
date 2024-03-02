namespace No1.ApplicationServices.ApplicationUserService.ChangePassword;

public class ChangePasswordInput
{
    public string CurrentPassword { get; set; }
    
    public string NewPassword { get; set; }
}