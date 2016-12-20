using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Haberimdesin2.Models
{
    [Table("LikeComment")]
    public class LikeCommentEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int likeCommentID { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser user { get; set; }
        public string UserID { get; set; }
        [ForeignKey("CommentID")]
        public virtual CommentEntity comment { get; set; }
        public int CommentID { get; set; }
    }
}
