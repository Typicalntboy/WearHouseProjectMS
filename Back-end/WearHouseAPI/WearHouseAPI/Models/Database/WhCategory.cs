using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WearHouseAPI.Models.Database
{
    public class whCategory
    {
        [Key]
        public string? CatId { get; set; }

        public string? Category { get; set; }

        public DateTime? CtgIn {  get; set; }

        [ForeignKey("WhUser")]
        public string? UserID { get; set; }
    }
}
