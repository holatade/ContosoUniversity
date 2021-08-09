using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Student : Entity
    {
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual DateTime EnrollmentDate { get; set; }
        public virtual IList<Enrollment> Enrollments { get; set; }

        public Student()
        {
            EnrollmentDate = DateTime.Now;
            Enrollments = new List<Enrollment>();
        }
    }
}
