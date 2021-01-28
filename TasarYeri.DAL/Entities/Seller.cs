using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasarYeri.DAL.Entities
{
    public class Seller
    {
        public int ID { get; set; } 
        public int MemberID { get; set; } 

        [StringLength(40), Column(TypeName = "varchar(40)"), Required(ErrorMessage = "İsim boş geçilemez"), Display(Name = "Üye Adı")]
        public string Name { get; set; }

        [StringLength(40), Column(TypeName = "varchar(40)"), Required(ErrorMessage = "Soyisim boş geçilemez"), Display(Name = "Üye Soyadı")]
        public string LastName { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Required(ErrorMessage = "Mail Adresi boş geçilemez"), Display(Name = "Mail Adresi")]
        public string Mail { get; set; }

        [StringLength(32), Column(TypeName = "varchar(32)"), Required(ErrorMessage = "Şifre Boş Geçilemez"), Display(Name = "Kullanıcı Şifresi")]
        public string Password { get; set; }
        public string Hakkımda { get; set; }
   



    }
}
