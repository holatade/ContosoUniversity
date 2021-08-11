using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.DTOs
{
    public class StudentDTO 
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public  DateTime EnrollmentDate { get; set; }
        public ICollection<StudentEnrollment> StudentEnrollments { get; set; }
    }

    public class StudentEnrollment
    {
        public Guid Id { get; set; }
        public string Grade { get; set; }
        public StudentCourse StudentCourse { get; set; }
    }

    public class StudentCourse
    {
        public Guid Id { get; set; }
        public int CourseCode { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}
