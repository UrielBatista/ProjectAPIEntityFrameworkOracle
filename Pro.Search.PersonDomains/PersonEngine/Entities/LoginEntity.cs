using System.ComponentModel.DataAnnotations;

namespace Pro.Search.PersonDomains.PersonEngine.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
