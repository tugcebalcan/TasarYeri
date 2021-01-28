using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("ProductMember")]
   public class ProductMember
    {
        [Key]
        public int MemberID { get; set; } 
        public Member Member { get; set; } 
        public int ProductID { get; set; } 
        public Product Product { get; set; } 
    }
}
