using Domain.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mappings
{
    public class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Budget).Not.Nullable();
            Map(x => x.StartDate).Not.Nullable();
            HasMany(x => x.Courses);
            HasMany(x => x.Instructors);
        }
    }
}
