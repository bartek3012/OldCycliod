using Backend;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Entity
{
    public class WorkCondition : BaseEntity 
    {
        public WorkCondition() : base("Warunki pracy: ", "input", "")
        {
        }

        public EnumWorkCondition EnWorkCondition { get; set; }
    }
}
