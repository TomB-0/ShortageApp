using ShortageApp.Helpers;
using ShortageApp.Models;

namespace ShortageAppUnitTests.ModelTests
{
    public class UserModelTests
    {
        [Fact]
        public void IsAdmin_Success()
        {
            User user = new User("Tomas", EnumTypes.RoleType.Admin);
            var expected = true;
            Assert.Equal(user.IsAdmin(), expected);
        }
        [Fact]
        public void Equal_CaseSensitive_NotEqual()
        {
            User user1 = new User("Tomas", EnumTypes.RoleType.User);
            User user2 = new User("tomas", EnumTypes.RoleType.Admin);

            Assert.NotEqual(user1, user2);
        }
    }
}
