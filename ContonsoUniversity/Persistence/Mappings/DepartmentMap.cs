using Domain.Models;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappings
{
    public class DepartmentMap : ClassMapping<Department>
    {
        public DepartmentMap()
        {
            Id(e => e.Id, mapper => mapper.Generator(Generators.Guid));
            Property(e => e.Name, mapper => { mapper.NotNullable(true); mapper.Type(NHibernateUtil.String);});
            Property(e => e.Budget, mapper => { mapper.NotNullable(true); mapper.Type(NHibernateUtil.Currency); });
            Property(e => e.StartDate, mapper => { mapper.NotNullable(true); mapper.Type(NHibernateUtil.DateTime); });
            Bag(e => e.Courses,
              mapper =>
              {
                  mapper.Key(k => k.Column("DepartmentId"));
                  mapper.Cascade(Cascade.All);
              },
              relation => relation.OneToMany(
              mapping => mapping.Class(typeof(Course))));
            Bag(e => e.Instructors,
              mapper =>
              {
                  mapper.Key(k => k.Column("DepartmentId"));
                  mapper.Cascade(Cascade.All);
              },
              relation => relation.OneToMany(
              mapping => mapping.Class(typeof(Instructor))));
        }
    }
}
