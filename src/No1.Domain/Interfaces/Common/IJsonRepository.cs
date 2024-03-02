using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace No1.Interfaces.Common;

public interface IJsonRepository<TEntity> where TEntity : Entity
{
    Task<TEntity> SeedEntities(string jsonPath);
}






//public interface IJsonRepository<TEntity> where TEntity : Entity
//{
//    Task<TEntity> SeedEntities(string jsonPath, ICrudRepository<TEntity> repository);
//}