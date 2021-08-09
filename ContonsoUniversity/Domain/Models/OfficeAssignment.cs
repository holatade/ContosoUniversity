using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class OfficeAssignment :  Entity
    {
        public virtual string Location { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}
