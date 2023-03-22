using Newtonsoft.Json;
using ShortageApp.Helpers;

namespace ShortageApp.Models
{
    public class User
    {
        [JsonProperty]
        public string UserName { get; set; }
        public Helpers.EnumTypes.RoleType Role { get; set; }

        public User()
        {

        }
        public User(string userName)
        {
            UserName = userName;
        }
        public User(string userName, Helpers.EnumTypes.RoleType role)
        {
            UserName = userName;
            Role = role;
        }

        public bool IsAdmin()
        {
            return this.Role == Helpers.EnumTypes.RoleType.Admin;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return this.UserName == ((User)obj).UserName;
        }
        public void ChangeRole(EnumTypes.RoleType newRole)
        {
            this.Role = newRole;  
        }
    }
}
