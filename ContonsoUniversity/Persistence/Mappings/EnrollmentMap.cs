using Domain.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappings
{
    public class EnrollmentMap : ClassMap<Enrollment>
    {
        public EnrollmentMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Grade).Nullable();
            References(x => x.Course)
                .Not.Nullable()
                //.Cascade.SaveUpdate()
                .Cascade.All()
                .Column("CourseId");
            References(x => x.Student)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("StudentId");
        }
    }
}
