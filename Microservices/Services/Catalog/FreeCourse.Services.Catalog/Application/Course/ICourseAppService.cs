using FreeCourse.Services.Catalog.Dtos.Course;
using FreeCourse.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Application.Course
{
    public interface ICourseAppService
    {
        Task<IDataResult<IList<CourseDto>>> GetAllAsync();
        Task<IDataResult<CourseDto>> GetByIdAsync(string id);
        Task<IDataResult<IList<CourseDto>>> GetAllByUserIdAsync(string userId);
        Task<IDataResult<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<IResult> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<IResult> DeleteAsync(string id);
    }
}
