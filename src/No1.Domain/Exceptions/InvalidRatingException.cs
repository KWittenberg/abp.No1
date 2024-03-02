using Volo.Abp;

namespace No1.Exceptions
{
    public class InvalidRatingException : BusinessException, IUserFriendlyException
    {
        public InvalidRatingException() : this("Invalid rating")
        {
        }

        public InvalidRatingException(string message) : base(No1DomainErrorCodes.InvalidRating, message)
        {
        }
    }
}