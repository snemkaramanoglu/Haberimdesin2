using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Haberimdesin2.Models
{
    [Table("Comment")]
    public class CommentEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }
        [ForeignKey("HaberID")]
        public virtual HaberEntity haber { get; set; }
        public int HaberID { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser user { get; set; }
        public string UserID { get; set; }
        
        public string Content { get; set; }
        
        public DateTime TimeStamp { get; set; }
        
        public int Like { get; set; }
        
        public int Dislike { get; set; }
    }
}
