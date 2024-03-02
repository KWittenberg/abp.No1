using No1.Entities.LanguageAggregate;
using No1.EntityFrameworkCore.Repositories.Common;
using No1.Interfaces;
using System.Linq;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace No1.EntityFrameworkCore.Repositories;

public class LanguageRepository : CrudRepository<Language>, ILanguageRepository, ITransientDependency
{
    public LanguageRepository(IDbContextProvider<No1DbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    protected override IQueryable<Language> WithDetails(IQueryable<Language> query)
    {
        return query;
    }
}