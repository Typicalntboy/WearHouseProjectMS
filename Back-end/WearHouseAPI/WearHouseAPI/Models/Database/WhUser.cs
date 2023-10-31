using System.ComponentModel.DataAnnotations;

namespace WearHouseAPI.Models.Database
{
    public class WhUser
    {
        [Key]
        public string? UserID { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }

}
