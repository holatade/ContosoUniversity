using DataAccess.General.Implementation;
using DataAccess.General.Interface;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ISession _session;
        protected ITransaction _transaction;
        private ICourseRepository _courseRepository;
        private IStudentRepository _studentRepository;
        private IInstructorRepository _instructorRepository;

        public RepositoryWrapper(ISession session)
        {
            _session = session;
        }

        public IInstructorRepository Instructor
        {
            get
            {
                if (_instructorRepository == null)
                {
                    _instructorRepository = new InstructorRepository(_session);
                }
                return _instructorRepository;
            }
        }

        public ICourseRepository Course
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new CourseRepository(_session);
                }
                return _courseRepository;
            }
        }

        public IStudentRepository Student
        {
            get
            {
                if (_studentRepository == null)
                {
                    _studentRepository = new StudentRepository(_session);
                }
                return _studentRepository;
            }
        }

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
