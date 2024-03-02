using No1.Entities.ApplicationUserAggregate;
using Volo.Abp.Domain.Repositories;

namespace No1.Interfaces;

public interface IApplicationUserRepository : IBasicRepository<ApplicationUser>
{
}