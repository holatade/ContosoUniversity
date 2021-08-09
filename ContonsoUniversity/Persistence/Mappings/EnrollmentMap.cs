using Domain.Models;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappings
{
    public class EnrollmentMap : ClassMapping<Enrollment>
    {
        public EnrollmentMap()
        {
            Id(e => e.Id, mapper => mapper.Generator(Generators.Guid));
            Property(e => e.Grade, mapper => { mapper.NotNullable(false); mapper.Type(NHibernateUtil.String); });
            ManyToOne(b => b.Course, mapping => { mapping.Class(typeof(Course)); mapping.Column("CourseId"); });
            ManyToOne(b => b.Student, mapping => { mapping.Class(typeof(Student)); mapping.Column("StudentId"); });
        }
    }
}
