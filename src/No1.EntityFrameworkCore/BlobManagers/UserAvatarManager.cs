using Microsoft.Extensions.Configuration;
using No1.BlobManagers.Common;
using No1.BlobManagers.Containers;
using No1.Interfaces.BlobManagers;
using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace No1.BlobManagers;

public class UserAvatarManager : BlobManager, IUserAvatarManager, ITransientDependency
{
    private readonly string _selfUrl;

    public UserAvatarManager(IBlobContainer<UserAvatarContainer> userAvatarContainer, IConfiguration configuration) : base(userAvatarContainer)
    {
        _selfUrl = configuration["App:SelfUrl"] ?? throw new ArgumentNullException("App:SelfUrl");
    }

    public override string Url => $"{_selfUrl}/api/app/account/current-user-avatar";
}