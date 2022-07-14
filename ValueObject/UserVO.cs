using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRestaurant.ValueObject
{
    public class UserVO
    {
        public int Id { get; }
        public string UserName { get; }
        public string Role { get; }
        public string Password { get; }

        public UserVO(int id, string name, string role, string password)
        {
            this.Id = id;
            this.UserName = name;
            this.Role = role;
            this.Password = password;
        }
    }
}
