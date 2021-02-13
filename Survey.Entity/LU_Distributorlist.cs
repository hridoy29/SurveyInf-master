using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Entity
{
   public class LU_Distributorlist
    {
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public Int32 CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public Int32 ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
