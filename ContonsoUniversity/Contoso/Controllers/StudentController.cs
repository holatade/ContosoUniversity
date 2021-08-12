using AutoMapper;
using Contoso.Helpers;
using Contoso.ViewModels;
using DataAccess;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;

        public StudentController(IRepositoryWrapper repoWrapper, IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
        }

        [ProducesResponseType(200, Type = typeof(Student))]
        [HttpGet("[action]")]
        public async Task<IActionResult> StudentList()
        {
            var students = await _repoWrapper.Student.GetAll();
            return Ok(students);
        }

        /// <summary>
        /// Get the list of student offering a particular course using course Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudentsByCourseId(Guid courseId)
        {
            var students = await _repoWrapper.Student.GetStudentsByCourseId(courseId);
            return Ok(students);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> PaginatedStudentList([FromQuery] PaginationQuery paginationQuery)
        {
            var paginatedResponse = await _repoWrapper.Student.GetPaginatedStudentData(paginationQuery);
            return Ok(paginatedResponse);
        }

        [ProducesResponseType(200, Type = typeof(Student))]
        [HttpGet("[action]")]
        public async Task<IActionResult> StudentDetails(Guid studentId)
        {
            var student = await _repoWrapper.Student.GetAsync(studentId);
            if (!(student is null))
            {
                return Ok(student);
            }
            return NotFound("Student profile does not exist");
        }

        [ProducesResponseType(200, Type = typeof(string))]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                _repoWrapper.BeginTransaction();
                var student = _mapper.Map<Student>(studentViewModel);
                await _repoWrapper.Student.Save(student);
                await _repoWrapper.Commit();
                return Ok("Student was saved succesfully");
            }
            //return validation errors
            var errors = new List<string>();
            var errorList = ModelState.Values.SelectMany(m => m.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var error in errorList)
            {
                errors.Add(error);
            }
            return BadRequest(errors);
        }

        [ProducesResponseType(200, Type = typeof(string))]
        [HttpPut("[action]")]
        public async Task<IActionResult> Edit(Guid studentId, StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var student = await _repoWrapper.Student.GetAsync(studentId);
                if(!(student is null))
                {
                    _repoWrapper.BeginTransaction();
                    student = _mapper.Map(studentViewModel, student);
                    await _repoWrapper.Student.Save(student);
                    await _repoWrapper.Commit();
                    return Ok("Student profile was updated successfully");
                }
                return NotFound("Student profile does not exist");
            }
            //return validation errors
            var errors = new List<string>();
            var errorList = ModelState.Values.SelectMany(m => m.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var error in errorList)
            {
                errors.Add(error);
            }
            return BadRequest(errors);
        }

        [ProducesResponseType(200, Type = typeof(string))]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(Guid studentId)
        {
            if (ModelState.IsValid)
            {
                var student = await _repoWrapper.Student.GetAsync(studentId);
                if (!(student is null))
                {
                    _repoWrapper.BeginTransaction();
                    await _repoWrapper.Student.Delete(student);
                    await _repoWrapper.Commit();
                    return Ok("Student profile was deleted successfully");
                }
                return NotFound("Student profile does not exist");
            }
            //return validation errors
            var errors = new List<string>();
            var errorList = ModelState.Values.SelectMany(m => m.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var error in errorList)
            {
                errors.Add(error);
            }
            return BadRequest(errors);
        }
    }
}
