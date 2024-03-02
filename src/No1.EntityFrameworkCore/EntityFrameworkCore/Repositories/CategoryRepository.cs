using No1.Entities.CategoryAggregate;
using No1.EntityFrameworkCore.Repositories.Common;
using No1.Interfaces;
using System.Linq;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace No1.EntityFrameworkCore.Repositories;

public class CategoryRepository : CrudRepository<Category>, ICategoryRepository, ITransientDependency
{
    private readonly IDbContextProvider<No1DbContext> _dbContextProvider;

    public CategoryRepository(IDbContextProvider<No1DbContext> dbContextProvider) : base(dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }

    protected override IQueryable<Category> WithDetails(IQueryable<Category> query)
    {
        return query;
    }
}