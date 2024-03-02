using System.Threading.Tasks;
using No1.Interfaces;
using No1.Models;

namespace No1.ApplicationServices.TestService;

public class TestAppService(ITestService testService) : No1AppService
{
    public Task<TestModel> GetSum(int a, int b)
    {
        var sum = testService.Sum(a, b);
        
        return Task.FromResult(new TestModel { Sum = sum });
    }
}