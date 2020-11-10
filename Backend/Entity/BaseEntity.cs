using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend
{
    public class BaseEntity
    {
        public BaseEntity() { }

        public BaseEntity(double value, string content, string type)
        {
            Value = value;
            Content = content;
            Type = type;
        }

        public double Value { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
    }


}
