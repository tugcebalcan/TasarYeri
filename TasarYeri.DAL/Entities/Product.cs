using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TasarYeri.DAL.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public int CategoryID { get; set; } 
        public int SellerID { get; set; } 
        
        [StringLength(40), Column(TypeName = "varchar(50)"), Required(ErrorMessage = "Ürün Adı Boş Geçilemez"), Display(Name = "Ürün Adı")]
        public string Name { get; set; }

        [StringLength(100), Column(TypeName = "varchar(100)"), Required(ErrorMessage = "Ürün Detayı Boş Geçilemez"), Display(Name = "Ürün Detayı")]
        public string Detail { get; set; } 
        public string PictureWay { get; set; }  

        [NotMapped]
        public  string Picture { get; set; }
        public double Price { get; set; }
       public Category Category { get; set; }
       public Seller Seller { get; set; }

        public  virtual ICollection<Picture> Pictures { get; set; } 
        public  virtual ICollection<Comment> Comments  { get; set; } 
        public  virtual ICollection<Favorite> Favorites { get; set; } 
        public  virtual ICollection<Order> Orders { get; set; } 
        public  virtual ICollection<Basket> Baskets { get; set; } 
        

        public ICollection<ProductCategory> ProductCategories { get; set; } 
        public ICollection<ProductTag> ProductTags { get; set; } 
        public ICollection<ProductOrder> ProductOrders { get; set; } 
        public ICollection<ProductBasket> ProductBaskets { get; set; } 
        public ICollection<ProductFavorite> ProductFavorites { get; set; } 
        public ICollection<ProductMember> ProductMembers { get; set; } 



    }
}
