using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("Favorite")]
    public class Favorite
    {
        public int ID { get; set; } 
    

        public ICollection<FavoriteMember> FavoriteMembers { get; set; } 
        public ICollection<ProductFavorite> ProductFavorites { get; set; } 
    }
}
