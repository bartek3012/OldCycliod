using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Entity
{
   public class DataValue : BaseEntity
    {
        public DataValue(string content, EnumName nameId, string unit, string type) : base(content, type)
        {
            Content = content;
            Unit = unit;
            NameId = nameId;
        }
        public DataValue(string content, string type) : base(content, type)
        {
            Content = content;
            Unit = "";
        }
        public EnumName NameId { get; set; }
        public string Unit { get; set; }
    }
}
