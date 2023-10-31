using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WearHouseBackend.Models.Database
{
    public class MsCategory
    {
        [Key]
        public string? MsCtgID { get; set; }

        public string? MsCtg { get; set; }

        public DateTime? CtgIn { get; set; }

        [ForeignKey("MsUser")]
        public string? MsUserID { get; set; }
    }
}
