using DataAccess.General.Interface;
using Domain;
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

        public async Task<PagedResponse<Student>> GetPaginatedStudentData(PaginationQuery paginationQuery)
        {
            var paginatedResponse = new PagedResponse<Student>();
            var queryable = _session.Query<Student>();

            if (!string.IsNullOrEmpty(paginationQuery.SearchText))
            {
                queryable = queryable.Where(x => x.FirstName.Contains(paginationQuery.SearchText) || x.LastName.Contains(paginationQuery.SearchText));
            }

            //Sort the users
            queryable = paginationQuery.SortBy == 1 ? queryable.OrderBy(s => s.FirstName) : paginationQuery.SortBy == 2 ? queryable.OrderBy(s => s.LastName) :
                queryable.OrderByDescending(s => s.EnrollmentDate);

            var skip = (paginationQuery.PageNumber - 1) * paginationQuery.PageSize;

            var newQueryable = queryable.Skip(skip).Take(paginationQuery.PageSize).AsQueryable();
            paginatedResponse.Data = await newQueryable.ToListAsync();
            var recordCount = await queryable.CountAsync();
            paginatedResponse.RecordCount = recordCount;
            paginatedResponse.PageNumber = paginationQuery.PageNumber >= 1 ? paginationQuery.PageNumber : (int?)null;
            paginatedResponse.PageSize = paginationQuery.PageSize >= 1 ? paginationQuery.PageSize : (int?)null;
            paginatedResponse.PageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / (double)paginationQuery.PageSize));
            return paginatedResponse;
        }

        public async Task<List<Student>> GetStudentsByCourseId(Guid courseId)
        {
            var students = await _session.Query<Course>().Where(x => x.Id == courseId).SelectMany(x => x.Enrollments).Select(x => x.Student).ToListAsync();
            return students;
        }
    }
}
