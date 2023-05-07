using System.Data;
using Tamak.Data.Enum;

namespace Tamak.Data.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public City City { get; set; }

        public Campus Campus { get; set; }

        public Role Role { get; set; }

        public Profile Profile { get; set; }
    }
}
