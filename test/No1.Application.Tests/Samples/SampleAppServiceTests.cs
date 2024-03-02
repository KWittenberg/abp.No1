using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Xunit;

namespace No1.Samples;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services. */

public class SampleAppServiceTests : No1ApplicationTestBase
{
    private readonly IIdentityUserAppService _userAppService;
    private readonly IIdentityUserRepository _userRepository;

    public SampleAppServiceTests(IIdentityUserRepository userRepository)
    {
        _userRepository = userRepository;
        _userAppService = GetRequiredService<IIdentityUserAppService>();
    }

    [Fact]
    public async Task Initial_Data_Should_Contain_Admin_User()
    {
        //Act
        var result = await _userAppService.GetListAsync(new GetIdentityUsersInput());
        var result2 = await _userRepository.GetListAsync(includeDetails: true);

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
        // result.Items.ShouldContain(u => u.UserName == "admin");

        result2.Count.ShouldBeGreaterThan(0);
        // result.Items.ShouldContain(u => u.UserName == "admin");
    }


    [Fact]
    public async Task Is_Equal()
    {
        //Act
        var result = await _userAppService.GetListAsync(new GetIdentityUsersInput());
        var result2 = await _userRepository.GetListAsync(includeDetails: true);

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
        // result.Items.ShouldContain(u => u.UserName == "admin");

        result2.Count.ShouldBeGreaterThan(0);
        // result.Items.ShouldContain(u => u.UserName == "admin");
    }
}