 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("Category")]
    public class Category
    {
        public int ID { get; set; }// ID id Id

        [Display(Name = "Üst Kategori")]
        public int? ParentID { get; set; }
        public Category ParentCategory { get; set; }

        [StringLength(50), Column(TypeName = "varchar(50)"), Required(ErrorMessage = "Kategori Adı Boş Geçilemez"), Display(Name = "Kategori Adı")]
        public string Name { get; set; }

        [Display(Name = "Alt Kategoriler")]
        public ICollection<Category> SubCategories { get; set; }

        [Display(Name = "Görüntülenme Sırası")]
        public int? DisplayIndex { get; set; }
        
        public ICollection<Product> Products { get; set; }



        // public  ICollection<Tag> Tags { get; set; }
        // public  ICollection<ProductCategory> ProductCategories { get; set; } 
        // public  ICollection<CategoryTag> CategoryTags { get; set; } 
    }
}



