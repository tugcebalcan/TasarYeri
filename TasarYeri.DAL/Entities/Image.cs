using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("Image")]
    public class Image
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public Member Member { get; set; }
        public string ImageWay { get; set; }

    }
}
