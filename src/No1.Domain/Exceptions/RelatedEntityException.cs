using System;
using Volo.Abp;

namespace No1.Exceptions;

public class RelatedEntityException : BusinessException, IUserFriendlyException
{
    public RelatedEntityException(string message) : base(No1DomainErrorCodes.RelatedEntity, message)
    {
    }

    public RelatedEntityException() : this("Related entity.")
    {
    }

    public RelatedEntityException(Type entityType, object? id = null) : this(id == null
        ? $"Entity {entityType.Name} is already used somewhere."
        : $"Entity {entityType.Name} is already used somewhere with {id}.")
    {
    }
}