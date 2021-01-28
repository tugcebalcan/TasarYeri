using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("ProductBasket")]
   public class ProductBasket
    {
        [Key]
        public int ProductID { get; set; } 
        public Product Product { get; set; } 
        public int BasketID { get; set; } 
        public Basket Basket { get; set; } 

    }
}
