using System.ComponentModel.DataAnnotations;

namespace MVC_auth_app.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

    }
}
