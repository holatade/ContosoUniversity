using Domain.Models;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappings
{
    public class OfficeAssignmentMap : ClassMapping<OfficeAssignment>
    {
        public OfficeAssignmentMap()
        {

            Id(e => e.Id, mapper => mapper.Generator(Generators.Guid));
            Property(e => e.Location, mapper => { mapper.NotNullable(true); mapper.Type(NHibernateUtil.String); });
            ManyToOne(a => a.Instructor,
              mapper =>
              {
                  mapper.Class(typeof(Instructor));
                  mapper.Column("InstructorId");
                  mapper.Unique(true);
              });
        }
    }
}
