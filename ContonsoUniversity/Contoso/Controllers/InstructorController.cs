using AutoMapper;
using Contoso.Helpers;
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
    public class InstructorController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;

        public InstructorController(IRepositoryWrapper repoWrapper, IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
        }

        [ProducesResponseType(200, Type = typeof(string))]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteInstructor(Guid instructorId)
        {
            _repoWrapper.BeginTransaction();
            var instructor = await _repoWrapper.Instructor.GetAsync(instructorId);
            if (!(instructor is null))
            {
                await _repoWrapper.Instructor.Delete(instructor);
                await _repoWrapper.Commit();
                return Ok(instructor.FirstName + " "+instructor.LastName + " was deleted successfully");
            }
            return NotFound("Course was not found!");
        }
    }
}
