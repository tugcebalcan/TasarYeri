using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("Tag")]
    public class Tag
    {
        public int ID { get; set; } 
        public string  Name { get; set; } 
       
        public  ICollection<ProductTag> ProductTags { get; set; } 
        public  ICollection<CategoryTag> CategoryTags { get; set; } 

    }
}
