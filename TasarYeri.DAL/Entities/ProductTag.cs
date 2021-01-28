using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("ProductTag")]
    public class ProductTag
    {
        [Key]
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public int TagID { get; set; }
        public Tag Tag  { get; set; }
    }
}
