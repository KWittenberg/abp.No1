using No1.Interfaces;
using Volo.Abp.DependencyInjection;

namespace No1.Test;

public class TestService() : ITestService, ITransientDependency
{
    public int Sum(int a, int b)
    {
        return a + b;
    }
}