﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TasarYeri.DAL.Entities
{
    [Table("Town")]
    public class Town
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CityID { get; set; }
        public City City { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }

    }
}
