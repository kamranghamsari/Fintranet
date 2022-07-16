using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fintranet.Entities.BaseModels.User
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [JsonIgnore]
        [Required]
        public string Password { get; set; }
    }
}
