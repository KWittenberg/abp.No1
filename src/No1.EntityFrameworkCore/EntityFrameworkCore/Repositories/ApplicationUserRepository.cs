using No1.Entities.ApplicationUserAggregate;
using No1.Interfaces;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace No1.EntityFrameworkCore.Repositories;

public class ApplicationUserRepository : 
    EfCoreRepository<No1DbContext, ApplicationUser, Guid>, IApplicationUserRepository, ITransientDependency
{
    public ApplicationUserRepository(IDbContextProvider<No1DbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}