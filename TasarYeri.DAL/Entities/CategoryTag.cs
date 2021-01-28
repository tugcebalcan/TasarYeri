using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("CategoryTag")]
  public  class CategoryTag
    {
        [Key]
        public int CategoryID { get; set; } 
        public Category Category { get; set; } 
        public int TagID { get; set; } 
        public Tag Tag { get; set; } 
    }
}
