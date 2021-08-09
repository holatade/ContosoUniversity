using DataAccess.General.Interface;
using Domain.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.General.Implementation
{
    class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(ISession session) : base(session)
        {
        }

        public async Task<Student> StudentDetails(Guid studentId)
        {
            var student = await _session.Query<Student>().FirstOrDefaultAsync(x => x.Id == studentId);
            return student;
        }
    }
}
