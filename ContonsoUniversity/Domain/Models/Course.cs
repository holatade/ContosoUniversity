using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Course : Entity
    {
        public virtual int CourseCode { get; set; }
        public virtual string Title { get; set; }
        public virtual int Credits { get; set; }
        public virtual IList<Enrollment> Enrollments { get; set; }
        public virtual IList<Instructor> Instructors { get; set; }
        public virtual Department Department { get; set; }

        public Course()
        {
            Enrollments = new List<Enrollment>();
            Instructors = new List<Instructor>();
        }

    }
}
