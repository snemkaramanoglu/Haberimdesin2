using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Haberimdesin2.Models
{
    [Table("Image")]
    public class ImageEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ImageID { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser user { get; set; }
        public string UserID { get; set; }
        [ForeignKey("HaberID")]
        public virtual HaberEntity haber { get; set; }
        public int HaberID { get; set; }
        
        public string ImageURL { get; set; }
    }
}
