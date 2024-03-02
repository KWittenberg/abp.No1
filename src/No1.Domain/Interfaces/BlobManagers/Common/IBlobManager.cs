using System;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace No1.Interfaces.BlobManagers.Common;

public interface IBlobManager
{
    Task<IRemoteStreamContent?> GetAsync(Guid id);
    
    Task<Uri> SaveAsync(Guid id, IRemoteStreamContent content);
    
    Task DeleteAsync(Guid id);
}