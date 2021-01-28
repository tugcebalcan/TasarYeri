using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("ProductFavorite")]
   public class ProductFavorite
    {
        [Key]
        public int ProductID { get; set; } 
        public Product Product { get; set; } 
        public int FavoriteID { get; set; } 
        public Favorite Favorite { get; set; } 
    }
}
