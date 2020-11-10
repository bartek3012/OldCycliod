using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Entity
{
    class DataValue : BaseEntity
    {
        public DataValue(string content, EnumName nameId, string unit, string type) : base(0, content, type)
        {
            Content = content;
            Unit = unit;
            NameId = nameId;
        }
        public EnumName NameId { get; set; }
        public string Unit { get; set; }
    }
}
