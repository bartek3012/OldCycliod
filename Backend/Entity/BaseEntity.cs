using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    public class BaseEntity
    {
        public BaseEntity(string content, string type, string unit, double value = 0)
        {
            Value = value;
            Content = content;
            Type = type;
            Unit = unit;
        }

        public BaseEntity(string content, string type, double value)
        {
            Value = value;
            Content = content;
            Type = type;
            Unit = "MPa";
        }

        public BaseEntity()
        {

        }

        public double Value { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
    }


}
