using DataAccess.General.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepositoryWrapper
    {
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        ICourseRepository Course { get; }
        IStudentRepository Student { get; }
        IInstructorRepository Instructor { get; }
    }
}
