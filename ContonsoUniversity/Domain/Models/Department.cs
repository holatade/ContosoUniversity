using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Department : Entity
    {
        public virtual string Name { get; set; }
        public virtual decimal Budget { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual IList<Course> Courses { get; set; }
        public virtual IList<Instructor> Instructors { get; set; }

        public Department()
        {
            Courses = new List<Course>();
            Instructors = new List<Instructor>();
        }

        public virtual void AddCourse(Course course)
        {
            course.Department = this;
            Courses.Add(course);
        }
        public virtual void AddInstructor(Instructor instructor)
        {
            instructor.Department = this;
            Instructors.Add(instructor);
        }
    }
}
