using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("Comment")]
    public class Comment
    {
        public int ID { get; set; }

        [StringLength(100), Column(TypeName = "varchar(100)")]
        public string Comments { get; set; } 
        public string UserName { get; set; } 
        public int MemberID { get; set; } 
        public Member Member { get; set; } 
        public int ProductID { get; set; } 
        public Product Product { get; set; } 
        public DateTime Date { get; set; } 
    }
}
