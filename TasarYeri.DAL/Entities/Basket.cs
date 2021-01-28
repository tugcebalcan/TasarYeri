using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("Basket")]
    public class Basket
    {
        public Basket()
        {
            Count = 1;
        }

        [Key]
        public int ID { get; set; }
        [ForeignKey("MemberID")]
        public Member Member { get; set; }
        public int MemberID { get; set; }
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
        [DataType("date")]
        public DateTime Date { get; set; }
        [Range(1, 1)]
        public int Count { get; set; }
        [NotMapped]
        public double Price { get; set; }

        public ICollection<ProductBasket> ProductBaskets { get; set; }
        public ICollection<OrderBasket> OrderBaskets { get; set; }

    }
}
