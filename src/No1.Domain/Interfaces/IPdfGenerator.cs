using System.IO;
using System.Threading.Tasks;

namespace No1.Interfaces;

public interface IPdfGenerator
{
    public Task<MemoryStream> Generate(string html);
}