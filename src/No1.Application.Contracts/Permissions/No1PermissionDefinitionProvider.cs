using No1.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace No1.Permissions;

public class No1PermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(No1Permissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(No1Permissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<No1Resource>(name);
    }
}