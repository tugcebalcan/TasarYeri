using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TasarYeri.DAL.Entities
{
   public class Admin
    {
        public int ID { get; set; }

        [StringLength(40), Column(TypeName = "varchar(40)"), Required(ErrorMessage = "İsim boş geçilemez"), Display(Name = "Üye Adı")]
        public string Name { get; set; }

        [StringLength(40), Column(TypeName = "varchar(40)"), Required(ErrorMessage = "Soyisim boş geçilemez"), Display(Name = "Üye Soyadı")]
        public string LastName { get; set; }

        [StringLength(20), Column(TypeName = "Varchar(20)"), Required(ErrorMessage = "Mail boş geçilemez"), Display(Name = "Mail Adresi")]
        public string Mail { get; set; } 

        [StringLength(32), Column(TypeName = "Varchar(32)"), Display(Name = "Şifre"), Required(ErrorMessage = "Şifre boş geçilemez"), DataType(DataType.Password)]
        public string Password { get; set; }

        public ERole Role { get; set; }
    }
}
