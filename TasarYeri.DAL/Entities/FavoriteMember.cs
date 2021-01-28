using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("FavoriteMember")]
  public  class FavoriteMember
    {
        [Key]
        public int FavoriteID { get; set; } 
        public Favorite Favorite { get; set; } 
        public int MemberID { get; set; } 
        public Member Member { get; set; } 
    }
}
