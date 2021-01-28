using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("Picture")]
   public class Picture
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; } 
        public string PictureWay { get; set; } 
  
    }
}
