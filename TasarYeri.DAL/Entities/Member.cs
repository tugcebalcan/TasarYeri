using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
  [Table("Member")]
  
    public class Member
    {
        
        public int ID { get; set; }
        
        [StringLength(40), Column(TypeName = "varchar(40)"), Required(ErrorMessage = "İsim boş geçilemez"), Display(Name = "Üye Adı")]
        public string Name { get; set; }

        [StringLength(40), Column(TypeName ="varchar(40)"), Required(ErrorMessage ="Soyisim boş geçilemez"), Display(Name ="Üye Soyadı")]
        public string LastName { get; set; }

        [StringLength(5), Column(TypeName = "varchar(5)"), Display(Name = "Cinsiyet")]
        public string Gender { get; set; }

        [Column(TypeName = "date"), Required(ErrorMessage = "Doğum tarihi boş geçilemez"), Display(Name = "Doğum Tarihi")]
        public DateTime BirthDate { get; set; }

        [Column(TypeName = "varchar(50)"), Required(ErrorMessage = "Mail Adresi Boş Geçilemez"), Display(Name = "Mail Adresi")]
        public string Mail { get; set; }

        [StringLength(32), Column(TypeName = "varchar(32)"), Required(ErrorMessage = "Şifre Boş Geçilemez"), Display(Name = "Kullanıcı Şifresi"),DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(10), Column(TypeName = "char(10)"), Required(ErrorMessage = "Telefon Boş Geçilemez"), Display(Name = "Telefon Numarası")]
        public string Phone { get; set; }

        [StringLength(21), Column(TypeName = "char(21)"), Display(Name = "IP")]
        public string IP { get; set; }
        public int RoleNumber { get; set; }
        public string ImageWay { get; set; }

        [NotMapped]
        public string Image { get; set; }

        public ERole Role { get; set; }

        public virtual ICollection<Address> Addresses{ get; set; } 
        public virtual ICollection<Comment> Comments{ get; set; } 
        public virtual ICollection<Order> Orders{ get; set; } 
        public virtual ICollection<Favorite> Favorites{ get; set; } 

        public ICollection<ProductMember> ProductMembers { get; set; } 
        public ICollection<FavoriteMember> FavoriteMembers { get; set; } 


    }
}
