using AutoMapper;
using Contoso.DTOs;
using Contoso.Helpers;
using Contoso.ViewModels;
using DataAccess;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;

        public CourseController(IRepositoryWrapper repoWrapper, IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
        }

        [ProducesResponseType(200, Type = typeof(CourseDTO))]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _repoWrapper.Course.GetAll();
            return Ok(courses);
        }

        [HttpGet("[action]")]
        public IActionResult CourseAverageCredit()
        {
            var averageCredit = _repoWrapper.Course.CourseAverageCredit();
            return Ok(averageCredit);
        }

        [ProducesResponseType(200, Type = typeof(string))]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteCourse(Guid courseId)
        {
            _repoWrapper.BeginTransaction();
            var course = await _repoWrapper.Course.GetAsync(courseId);
            if(!(course is null))
            {
                await _repoWrapper.Course.Delete(course);
                await _repoWrapper.Commit();
                return Ok(course.Title + " was deleted successfylly");
            }
            return NotFound("Course was not found!");
        }

        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateCourse(CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                _repoWrapper.BeginTransaction();
                var course = _mapper.Map<Course>(courseViewModel);
                await _repoWrapper.Course.Save(course);
                await _repoWrapper.Commit();
                return Ok("Course was saved succesfully");
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
