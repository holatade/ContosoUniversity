using Domain.Models;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappings
{
    class StudentMap : ClassMapping<Student>
    {
        public StudentMap()
        {
            Id(e => e.Id, mapper => mapper.Generator(Generators.Guid));
            Property(e => e.FirstName, mapper => { mapper.NotNullable(true); mapper.Type(NHibernateUtil.String); });
            Property(e => e.LastName, mapper => { mapper.NotNullable(true); mapper.Type(NHibernateUtil.String); });
            Property(e => e.EnrollmentDate, mapper => { mapper.NotNullable(true); mapper.Type(NHibernateUtil.DateTime); });
            Bag(e => e.Enrollments,
             mapper =>
             {
                 mapper.Key(k => k.Column("StudentId"));
                 mapper.Cascade(Cascade.All);
             },
             relation => relation.OneToMany(
             mapping => mapping.Class(typeof(Enrollment))));
        }
    }
}
