using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Haberimdesin2.Models
{
    [Table("DislikeHaber")]
    public class DislikeHaberEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int dislikeHaberID { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser user { get; set; }
        public string UserID { get; set; }
        [ForeignKey("HaberID")]
        public virtual HaberEntity haber { get; set; }
        public int HaberID { get; set; }
    }
}
