using Domain.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappings
{
    public class InstructorMap : ClassMap<Instructor>
    {
        public InstructorMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.LastName).Not.Nullable();
            Map(x => x.HireDate).Not.Nullable();
            HasOne(x => x.OfficeAssignment).PropertyRef(x => x.Instructor);
            HasManyToMany(x => x.Courses)
                //.Cascade.All()
                .Table("CourseAssignment");
            References(x => x.Department)
            .Column("DepartmentId");
        }
    }
}
