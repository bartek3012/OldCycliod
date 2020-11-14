﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Entity
{
   public class DataValue : BaseEntity
    {
        public DataValue(string content, EnumName nameId, string unit, string type) : base(content, type, unit)
        {
            Content = content;
            NameId = nameId;
        }

        public EnumName NameId { get; set; }
    }
}
