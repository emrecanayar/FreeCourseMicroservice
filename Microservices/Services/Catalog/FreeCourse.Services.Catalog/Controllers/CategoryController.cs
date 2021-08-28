using FreeCourse.Services.Catalog.Application.Category;
using FreeCourse.Services.Catalog.Dtos.Category;
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
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryAppService.GetAllAsync();
            return CreateActionResultInstanceDataResult(response);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categoryAppService.GetByIdAsync(id);
            return CreateActionResultInstanceDataResult(response);
        }


        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CategoryCreateDto categoryCreateDto)
        {
            var response = await _categoryAppService.CreateAsync(categoryCreateDto);
            return CreateActionResultInstanceDataResult(response);
        }


        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _categoryAppService.DeleteAsync(id);
            return CreateActionResultInstanceResult(response);
        }
    }
}
