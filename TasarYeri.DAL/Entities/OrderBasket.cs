using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("OrderBasket)")]
    public class OrderBasket
    {
        [Key]
        public int OrderID { get; set; } 
        public Order Order { get; set; } 
        public int BasketID { get; set; } 
        public Basket Basket { get; set; } 
    }
}
