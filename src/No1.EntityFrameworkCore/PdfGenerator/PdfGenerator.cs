using System.IO;
using System.Threading.Tasks;
using No1.Interfaces;
using PdfSharpCore;
using Volo.Abp.DependencyInjection;
using Renderer = HtmlRendererCore.PdfSharp;

namespace No1.PdfGenerator;

public class PdfGenerator : IPdfGenerator, ITransientDependency
{
    public Task<MemoryStream> Generate(string html)
    {
        var stream = new MemoryStream();

        var pdf = Renderer.PdfGenerator.GeneratePdf(html, PageSize.A4);

        pdf.Save(stream);

        return Task.FromResult(stream);
    }
}