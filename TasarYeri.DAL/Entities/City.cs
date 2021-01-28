using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("City")]
    public class City

    {
        public int ID { get; set; }
        public string Name{ get; set; }
        public int CountryID { get; set; }
        public Country Country { get; set; }
        public virtual ICollection <Town> Towns{ get; set; }

    }
}
