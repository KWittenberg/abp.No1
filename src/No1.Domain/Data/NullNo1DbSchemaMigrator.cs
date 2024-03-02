using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace No1.Data;

/* This is used if database provider does't define
 * INo1DbSchemaMigrator implementation.
 */
public class NullNo1DbSchemaMigrator : INo1DbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
