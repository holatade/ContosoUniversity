using DataAccess.General.Interface;
using Domain.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.General.Implementation
{
    public class InstructorRepository : BaseRepository<Instructor>, IInstructorRepository
    {
        public InstructorRepository(ISession session) : base(session)
        {
        }
    }
}
