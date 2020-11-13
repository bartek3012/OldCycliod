using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend
{
    public class BaseEntity
    {
        public BaseEntity(string content, string type, double value = 0)
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
