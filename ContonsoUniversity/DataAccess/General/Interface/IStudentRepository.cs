using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.General.Interface
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Task<Student> StudentDetails(Guid studentId);
    }
}
