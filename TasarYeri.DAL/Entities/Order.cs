using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("Order")]
    public class Order
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public Member Member { get; set; }
        public int ProductID { get; set; }


        public ICollection<ProductOrder> ProductOrders { get; set; } 
        public ICollection<OrderBasket> OrderBaskets { get; set; } 
    }
}
