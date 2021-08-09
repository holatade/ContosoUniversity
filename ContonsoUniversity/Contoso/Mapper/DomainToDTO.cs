using AutoMapper;
using Contoso.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Mapper
{
    public class DomainToDTO : Profile
    {
        public DomainToDTO()
        {
            CreateMap<Student, StudentDTO>()
                 .ForMember(dest => dest.StudentEnrollments, opt => opt.MapFrom(x => x.Enrollments));

            CreateMap<Enrollment, StudentEnrollment>()
                .ForMember(dest => dest.StudentCourse, opt => opt.MapFrom(x => x.Course));

            CreateMap<Course, StudentCourse>();

            CreateMap<Course, CourseDTO>()
                 .ForMember(dest => dest.DepartmentDTO, opt => opt.MapFrom(x => x.Department));

            CreateMap<Department, DepartmentDTO>();
        }
    }
}
