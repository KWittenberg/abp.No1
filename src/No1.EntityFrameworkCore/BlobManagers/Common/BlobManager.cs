using System;
using System.IO;
using System.Threading.Tasks;
using No1.Interfaces.BlobManagers.Common;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;

namespace No1.BlobManagers.Common;

public abstract class BlobManager(IBlobContainer fileContainer) : IBlobManager
{
    public abstract string Url { get; }

    public async Task<IRemoteStreamContent?> GetAsync(Guid id)
    {
        var fileContent = await fileContainer.GetAllBytesOrNullAsync(id.ToString());

        return fileContent is null ? null : new RemoteStreamContent(new MemoryStream(fileContent));
    }

    public async Task<Uri> SaveAsync(Guid id, IRemoteStreamContent content)
    {
        await fileContainer.SaveAsync(id.ToString(), content.GetStream(), true);

        var urlBuilder = new UriBuilder(Url.TrimEnd('/'));

        urlBuilder.Path += $"/{id}";

        return urlBuilder.Uri;
    }

    public Task DeleteAsync(Guid id)
    {
        return fileContainer.DeleteAsync(id.ToString());
    }
}