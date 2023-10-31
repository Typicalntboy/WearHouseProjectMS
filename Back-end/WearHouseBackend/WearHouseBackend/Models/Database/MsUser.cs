using System.ComponentModel.DataAnnotations;

namespace WearHouseBackend.Models.Database
{
    public class MsUser
    {
        [Key]
        public string? MsUserID { get; set; }

        public string? MsName { get; set; }

        public string? MsEmail { get; set; }

        public string? MsPassword { get; set; }
    }
}