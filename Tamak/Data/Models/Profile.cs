using Tamak.Data.Enum;

namespace Tamak.Data.Models
{
    public class Profile
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public City City { get; set; }

        public Campus Campus { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }
    }
}
