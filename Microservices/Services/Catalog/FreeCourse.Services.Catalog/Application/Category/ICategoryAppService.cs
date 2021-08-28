using FreeCourse.Services.Catalog.Dtos.Category;
using FreeCourse.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Application.Category
{
    public interface ICategoryAppService
    {
        Task<IDataResult<IList<CategoryDto>>> GetAllAsync();
        Task<IDataResult<CategoryDto>> CreateAsync(CategoryCreateDto categoryDto);
        Task<IDataResult<CategoryDto>> GetByIdAsync(string id);
        Task<IResult> DeleteAsync(string id);

    }
}
