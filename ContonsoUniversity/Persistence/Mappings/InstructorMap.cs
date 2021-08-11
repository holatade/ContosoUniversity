using Domain.Models;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappings
{
    public class InstructorMap : ClassMapping<Instructor>
    {
        public InstructorMap()
        {
            Id(e => e.Id, mapper => mapper.Generator(Generators.Guid));
            Property(e => e.FirstName, mapper => { mapper.NotNullable(true); mapper.Type(NHibernateUtil.String); });
            Property(e => e.LastName, mapper => { mapper.NotNullable(true); mapper.Type(NHibernateUtil.String); });
            Property(e => e.HireDate, mapper => { mapper.NotNullable(true); mapper.Type(NHibernateUtil.DateTime); });
            OneToOne(e => e.OfficeAssignment,
              mapper =>
              {
                  mapper.Cascade(Cascade.Persist);
                  mapper.PropertyReference(a => a.Instructor);
              });
            ManyToOne(b => b.Department, mapping => { mapping.Class(typeof(Department));mapping.Column("DepartmentId"); });
            Bag(e => e.Courses,
              mapper =>
              {
                  mapper.Fetch(CollectionFetchMode.Join);
                  mapper.Key(k => k.Column("InstructorId"));
                  mapper.Table("CourseAssignment");
              },
              relation => relation.ManyToMany(mtm =>
              {
                  mtm.Class(typeof(Course));
                  mtm.Column("CourseId");
              }));
        }
    }
}
