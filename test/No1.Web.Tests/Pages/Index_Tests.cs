using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace No1.Pages;

public class Index_Tests : No1WebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
