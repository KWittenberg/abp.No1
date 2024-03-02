using System.Threading.Tasks;

namespace No1.Data;

public interface INo1DbSchemaMigrator
{
    Task MigrateAsync();
}