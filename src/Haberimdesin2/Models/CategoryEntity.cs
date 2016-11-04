using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Haberimdesin2.Models
{
    [Table("Category")]
    public class CategoryEntity
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        
        public string Name { get; set; }
    }
}
