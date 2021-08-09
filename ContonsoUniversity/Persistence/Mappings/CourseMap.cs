using Domain.Models;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappings
{
    class CourseMap : ClassMapping<Course>
    {
        public CourseMap()
        {
            Id(e => e.Id, mapper => mapper.Generator(Generators.Guid));
            Property(e => e.CourseCode, mapper => { mapper.NotNullable(true); mapper.Type(NHibernateUtil.Int32); });
            Property(e => e.Title, mapper => { mapper.NotNullable(true); mapper.Type(NHibernateUtil.String); });
            Property(e => e.Credits, mapper => { mapper.NotNullable(true); mapper.Type(NHibernateUtil.Int32); });
            Bag(e => e.Enrollments,
             mapper =>
             {
                 mapper.Key(k => k.Column("CourseId"));
                 mapper.Cascade(Cascade.All);
                 mapper.Inverse(true);
             },
             relation => relation.OneToMany(
             mapping => mapping.Class(typeof(Enrollment))));
            ManyToOne(b => b.Department, mapping => { mapping.Class(typeof(Department)); mapping.Column("DepartmentId"); });
            Bag(e => e.Instructors,
              mapper =>
              {
                  mapper.Key(k => k.Column("CourseId"));
                  mapper.Table("CourseAssignment");
                  mapper.Cascade(Cascade.All);
                  mapper.Inverse(true);
              },
              relation => relation.ManyToMany(mtm =>
              {
                  mtm.Class(typeof(Instructor));
                  mtm.Column("InstructorId");
              }));
        }
    }
}
