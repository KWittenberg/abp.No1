using Volo.Abp.Settings;

namespace No1.Settings;

public class No1SettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(No1Settings.MySetting1));
    }
}
