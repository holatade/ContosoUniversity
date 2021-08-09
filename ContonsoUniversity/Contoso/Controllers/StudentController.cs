using AutoMapper;
using Contoso.DTOs;
using Contoso.Helpers;
using Contoso.ViewModels;
using DataAccess;
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

        [ProducesResponseType(200, Type = typeof(StudentDTO))]
        [HttpGet("[action]")]
        [Cached(20)]
        public async Task<IActionResult> StudentList()        
        {
            var students = await _repoWrapper.Student.GetAll();
            var StudentListDTO = _mapper.Map<List<Student>, List<StudentDTO>>(students);
            return Ok(StudentListDTO);
        }

        [ProducesResponseType(200, Type = typeof(StudentDTO))]
        [HttpGet("[action]")]
        [Cached(20)]
        public async Task<IActionResult> StudentDetails(Guid studentId)
        {
            var student = await _repoWrapper.Student.GetAsync(studentId);
            if (!(student is null))
            {
                _repoWrapper.BeginTransaction();
                var studentDTO = _mapper.Map<StudentDTO>(student);
                await _repoWrapper.Commit();
                return Ok(studentDTO);
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
