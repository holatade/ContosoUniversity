using Domain.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappings
{
    class CourseMap : ClassMap<Course>
    {
        public CourseMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.CourseCode).Not.Nullable();
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Credits).Nullable();
            HasMany(x => x.Enrollments)
            //.Cascade.AllDeleteOrphan()
            .Cascade.All()
            .Inverse()
            .Fetch.Join().KeyColumn("CourseId");
            References(x => x.Department)
            .Column("DepartmentId");
            HasManyToMany(x => x.Instructors)
               .Cascade.All()
              .Inverse()
              .Table("CourseAssignment");
        }
    }
}
