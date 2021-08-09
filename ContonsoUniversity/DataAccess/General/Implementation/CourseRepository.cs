using DataAccess.General.Interface;
using Domain.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.General.Implementation
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(ISession session) : base(session)
        {
        }
    }
}
