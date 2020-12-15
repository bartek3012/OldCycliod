using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Entity
{
   public class DataValue : BaseEntity
    {
        public DataValue(string content, EnumName nameId, string unit, string type) : base(content, type, unit)
        {
            NameId = nameId;
        }
        public EnumName NameId { get; set; }
    }
}
