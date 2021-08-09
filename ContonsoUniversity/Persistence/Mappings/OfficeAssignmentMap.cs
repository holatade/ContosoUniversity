using Domain.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappings
{
    public class OfficeAssignmentMap : ClassMap<OfficeAssignment>
    {
        public OfficeAssignmentMap()
        {

            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Location).Not.Nullable();
            References(x => x.Instructor, "InstructorId").Unique();
        }
    }
}
