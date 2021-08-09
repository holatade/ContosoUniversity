using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.DTOs
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
        public  int CourseCode { get; set; }
        public  string Title { get; set; }
        public  int Credits { get; set; }
        public DepartmentDTO DepartmentDTO { get; set; }
    }

    public class DepartmentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
    }

}
