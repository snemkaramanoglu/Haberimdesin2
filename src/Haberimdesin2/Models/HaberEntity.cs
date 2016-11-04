using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Haberimdesin2.Models
{
    [Table("Haber")]
    public class HaberEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int HaberID { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser user { get; set; }
        public string UserId { get; set; }

        public string Title { get; set; }
        //
        public string HeadLine { get; set; }
        //
        public string Detail { get; set; }
        //
        public string PrimaryImgURL { get; set; }
        //
        public int Like { get; set; }
        //
        public int Dislike { get; set; }

       
        //
        public DateTime TimeStamp { get; set; }
        //
        public float Latitude { get; set; }
        //
        public float Longitude { get; set; }
        [ForeignKey("CategoryID")]
        public virtual CategoryEntity category { get; set; }
        public int CategoryID { get; set; }
        
    }
}
