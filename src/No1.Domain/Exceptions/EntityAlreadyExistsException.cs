using System;
using Volo.Abp;

namespace No1.Exceptions;

public class EntityAlreadyExistsException : BusinessException, IUserFriendlyException
{
    public EntityAlreadyExistsException() : this("Entity already exists.")
    {
    }

    public EntityAlreadyExistsException(string message) : base(No1DomainErrorCodes.EntityAlreadyExists, message)
    {
    }

    public EntityAlreadyExistsException(Type entityType, object? id) : this(id == null
            ? $"{entityType.FullName} already exists."
            : $"{entityType.FullName} with key '{id}' already exists.")
    {
    }
}