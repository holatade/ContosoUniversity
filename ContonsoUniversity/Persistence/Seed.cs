using Domain.Enums;
using Domain.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(ISession session)
        {
            using (var transaction = session.BeginTransaction())
            {
                var carson = new Student { FirstName = "Carson", LastName = "Alexander", EnrollmentDate = DateTime.Parse("2005-09-01") };
                var meredith = new Student { FirstName = "Meredith", LastName = "Alonso", EnrollmentDate = DateTime.Parse("2002-09-01") };
                var arturo = new Student { FirstName = "Arturo", LastName = "Anand", EnrollmentDate = DateTime.Parse("2003-09-01") };
                var gytis = new Student { FirstName = "Gytis", LastName = "Barzdukas", EnrollmentDate = DateTime.Parse("2002-09-01") };
                var yan = new Student { FirstName = "Yan", LastName = "Li", EnrollmentDate = DateTime.Parse("2002-09-01") };
                var peggy = new Student { FirstName = "Peggy", LastName = "Justice", EnrollmentDate = DateTime.Parse("2001-09-01") };
                var laura = new Student { FirstName = "Laura", LastName = "Norman", EnrollmentDate = DateTime.Parse("2003-09-01") };
                var nino = new Student { FirstName = "Nino", LastName = "Olivetto", EnrollmentDate = DateTime.Parse("2005-09-01") };

                var chemistry = new Course { CourseCode = 1050, Title = "Chemistry", Credits = 3 };
                var micoreconomics = new Course { CourseCode = 4022, Title = "Microeconomics", Credits = 3 };
                var macroeconomics = new Course { CourseCode = 4041, Title = "Macroeconomics", Credits = 3 };
                var calculus = new Course { CourseCode = 1045, Title = "Calculus", Credits = 4 };
                var trigonometry = new Course { CourseCode = 3141, Title = "Trigonometry", Credits = 4 };
                var composition = new Course { CourseCode = 2021, Title = "Composition", Credits = 3 };
                var literature = new Course { CourseCode = 2042, Title = "Literature", Credits = 4 };

                var enrollments = new Enrollment[]
                {
                new Enrollment {Student=carson,Course=chemistry,Grade=Grade.A.ToString()},
                new Enrollment { Student = carson, Course = micoreconomics, Grade = Grade.C.ToString() },
                new Enrollment { Student = carson, Course = macroeconomics, Grade = Grade.B.ToString() },
                new Enrollment { Student = meredith, Course = calculus, Grade = Grade.B.ToString() },
                new Enrollment { Student = meredith, Course = trigonometry, Grade = Grade.F.ToString() },
                new Enrollment { Student = meredith, Course = composition, Grade = Grade.F.ToString() },
                new Enrollment { Student = arturo, Course = chemistry },
                new Enrollment { Student = gytis, Course = chemistry },
                new Enrollment { Student = gytis, Course = micoreconomics, Grade = Grade.F.ToString() },
                new Enrollment { Student = yan, Course = macroeconomics, Grade = Grade.C.ToString() },
                new Enrollment { Student = peggy, Course = calculus },
                new Enrollment { Student = laura, Course = trigonometry, Grade = Grade.A.ToString() }
                };

                var kim = new Instructor { FirstName = "Kim", LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") };
                var fadi = new Instructor { FirstName = "Fadi", LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") };
                var roger = new Instructor { FirstName = "Roger", LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") };
                var candace = new Instructor { FirstName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") };
                var zheng = new Instructor
                {
                    FirstName = "Roger",
                    LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12")
                };


                var english = new Department
                {
                    Name = "English",
                    Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01")
                };
                var mathematics = new Department
                {
                    Name = "Mathematics",
                    Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01")
                };
                var science = new Department
                {
                    Name = "Science",
                    Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01")
                };
                var economics = new Department
                {
                    Name = "Economics",
                    Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01")
                };

                await AddStudent(session, carson, meredith, peggy, laura, arturo, gytis, yan, nino);
                await AddCourses(session, chemistry, micoreconomics, macroeconomics, calculus, trigonometry, composition, literature);
                await AddEnrollment(session, enrollments);

                await AddInstructor(session, kim, fadi, roger, candace, zheng);

                await AddDepartment(session, english, economics, science, mathematics);

                // add course to Instructor, this relationship is many to many
                await AddCourseToInstructor(session, candace, chemistry);
                await AddCourseToInstructor(session, roger, chemistry,trigonometry);
                await AddCourseToInstructor(session, zheng, micoreconomics);
                await AddCourseToInstructor(session, zheng, macroeconomics);
                await AddCourseToInstructor(session, fadi, calculus);
                await AddCourseToInstructor(session, kim, literature,composition);

                // add instructor to Department, this relationship is one to many,so one instructor can only work at one department
                await AddInstructorToDepartment(session, english, kim);
                await AddInstructorToDepartment(session, mathematics, fadi);
                await AddInstructorToDepartment(session, science, roger,candace);
                await AddInstructorToDepartment(session, economics, zheng);

                // add instructor to Department, this relationship is one to many,so one instructor can only work at one department
                await AddCourseToDepartment(session, english, literature,composition);
                await AddCourseToDepartment(session, mathematics, calculus);
                await AddCourseToDepartment(session, science, chemistry,trigonometry);
                await AddCourseToDepartment(session, economics, micoreconomics,macroeconomics);


                transaction.Commit();
            }
        }

        public static async Task AddStudent(ISession session, params Student[] students)
        {
            var checkStudents = await session.Query<Student>().AnyAsync();
            if (!checkStudents)
            {
                foreach (var student in students)
                {
                    await session.SaveOrUpdateAsync(student);
                }
            }
        }

        public static async Task AddCourses(ISession session, params Course[] courses)
        {
            var checkCourse = await session.Query<Course>().AnyAsync();
            if (!checkCourse)
            {
                foreach (var course in courses)
                {
                    await session.SaveOrUpdateAsync(course);
                }
            }
        }

        public static async Task AddEnrollment(ISession session, params Enrollment[] enrollments)
        {
            var checkEnrollment = await session.Query<Enrollment>().AnyAsync();
            if(!checkEnrollment)
            {
                foreach (var enrollment in enrollments)
                {
                    await session.SaveOrUpdateAsync(enrollment);
                }
            }            
        }

        public static async Task AddInstructor(ISession session, params Instructor[] instructors)
        {
            var checkInstructor = await session.Query<Instructor>().AnyAsync();
            if (!checkInstructor)
            {
                foreach (var instructor in instructors)
                {
                    await session.SaveOrUpdateAsync(instructor);
                }
            }            
        }

        public static async Task AddDepartment(ISession session, params Department[] departments)
        {
            var checkDepartment = await session.Query<Department>().AnyAsync();
            if (!checkDepartment)
            {
                foreach (var department in departments)
                {
                    await session.SaveOrUpdateAsync(department);
                }
            }            
        }

        public static async Task AddCourseToInstructor(ISession session, Instructor instructor, params Course[] courses)
        {
            foreach (var course in courses)
            {
                instructor.AddCourse(course);
                await session.SaveOrUpdateAsync(instructor);
            }
        }

        public static async Task AddInstructorToDepartment(ISession session, Department department, params Instructor[] instructors)
        {
            foreach (var instructor in instructors)
            {
                department.AddInstructor(instructor);
                await session.SaveOrUpdateAsync(department);
            }
        }

        public static async Task AddCourseToDepartment(ISession session, Department department, params Course[] courses)
        {
            foreach (var course in courses)
            {
                department.AddCourse(course);
                await session.SaveOrUpdateAsync(department);
            }
        }
    }
}
