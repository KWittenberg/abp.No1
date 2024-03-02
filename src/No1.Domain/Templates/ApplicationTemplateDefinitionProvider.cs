using No1.Localization;
using Volo.Abp.TextTemplating;
using Volo.Abp.TextTemplating.Razor;

namespace No1.Templates;

public class ApplicationTemplateDefinitionProvider : TemplateDefinitionProvider
{
    public override void Define(ITemplateDefinitionContext context)
    {
        context.Add(new TemplateDefinition("ConfirmRegistration", typeof(No1Resource))
                .WithRazorEngine()
                .WithVirtualFilePath("/Templates/Email/ConfirmRegistration/ConfirmRegistration.cshtml", true));

        context.Add(new TemplateDefinition("PasswordResetCode", typeof(No1Resource))
                .WithRazorEngine()
                .WithVirtualFilePath("/Templates/Email/PasswordResetCode/PasswordResetCode.cshtml", true));


        context.Add(new TemplateDefinition("TestEmail")
                .WithRazorEngine()
                .WithVirtualFilePath("/Templates/Email/TestEmail/TestEmailTemplate.cshtml", true));

        context.Add(new TemplateDefinition("TestPdf")
                .WithRazorEngine()
                .WithVirtualFilePath("/Templates/Pdf/TestPdf/TestPdfTemplate.cshtml", true));
        
        context.Add(new TemplateDefinition("YellowShark", typeof(No1Resource))
                .WithRazorEngine()
                .WithVirtualFilePath("/Templates/Email/YellowShark/YellowShark.cshtml", true));
    }
}