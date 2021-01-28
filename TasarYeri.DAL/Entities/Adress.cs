using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TasarYeri.DAL.Entities
{
    [Table("Adress")]
    public class Address
    {
        [Key]
        public int ID { get; set; }

        [StringLength(10), Column(TypeName = "nchar(10)"), Required(ErrorMessage = "Adres Adı Boş Geçilemez"), Display(Name = "Adres Adı")]
        public string Name { get; set; } //Ev-İş-Okul

        [StringLength(100), Column(TypeName = "varchar(100)"), Required(ErrorMessage = "Adres Boş Geçilemez"), Display(Name = "Adres")]
        public string Adress { get; set; } //Adres alır
        public int TownID { get; set; }
        public  Town Town { get; set; }
        public int MemberID { get; set; }
        public Member Member { get; set; }

    }
}
