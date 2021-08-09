using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.DTOs
{
    public class EnrollmentDTO
    {
        public Guid Id { get; set; }
        public string Grade { get; set; }
        public CourseDTO CourseDTO { get; set; }
        public StudentDTO StudentDTO { get; set; }
    }
}
