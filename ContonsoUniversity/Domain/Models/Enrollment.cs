using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Enrollment : Entity
    {
        public virtual string Grade { get; set; }
        public virtual Course Course {get;set;}
        public virtual Student Student { get; set; }
    }
}
