using Volo.Abp;

namespace No1.Exceptions;

public class AppsettingsException : BusinessException, IUserFriendlyException
{
    public AppsettingsException() : this("Invalid Appsettings Data")
    {
    }

    public AppsettingsException(string message) : base(No1DomainErrorCodes.InvalidAppsettingsData, "Invalid Appsettings Data", message)
    {
    }

    public AppsettingsException(string message, string details) : base(No1DomainErrorCodes.InvalidAppsettingsData, message, details)
    {
    }
}