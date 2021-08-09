using Domain.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappings
{
    class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.LastName).Not.Nullable();
            Map(x => x.EnrollmentDate).Nullable();
            HasMany(x => x.Enrollments)
                .Cascade.AllDeleteOrphan()
                .Fetch.Join()
                .Inverse().KeyColumn("StudentId");
        }
    }
}
