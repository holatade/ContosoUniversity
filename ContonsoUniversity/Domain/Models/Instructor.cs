using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Instructor : Entity
    {
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual DateTime HireDate { get; set; }
        public virtual OfficeAssignment OfficeAssignment { get; set; }
        public virtual Department Department { get; set; }
        public virtual IList<Course> Courses { get; set; }

        public Instructor()
        {
            Courses = new List<Course>();
        }

        public virtual void AddCourse(Course course)
        {
            course.Instructors.Add(this);
            Courses.Add(course);
        }
    }
}
