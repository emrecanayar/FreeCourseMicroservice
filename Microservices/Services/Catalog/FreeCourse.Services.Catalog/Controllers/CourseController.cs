using FreeCourse.Services.Catalog.Application.Course;
using FreeCourse.Services.Catalog.Dtos.Course;
using FreeCourse.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CourseController : CustomBaseController
    {
        private readonly ICourseAppService _courseAppService;

        public CourseController(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseAppService.GetAllAsync();
            return CreateActionResultInstanceDataResult(response);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseAppService.GetByIdAsync(id);
            return CreateActionResultInstanceDataResult(response);
        }

        [HttpGet]
        [Route("GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var response = await _courseAppService.GetAllByUserIdAsync(userId);
            return CreateActionResultInstanceDataResult(response);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            var response = await _courseAppService.CreateAsync(courseCreateDto);
            return CreateActionResultInstanceDataResult(response);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(CourseUpdateDto courseUpdateDto)
        {
            var response = await _courseAppService.UpdateAsync(courseUpdateDto);
            return CreateActionResultInstanceResult(response);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseAppService.DeleteAsync(id);
            return CreateActionResultInstanceResult(response);
        }
    }
}
