using AutoMapper;
using FreeCourse.Services.Catalog.Application.Category;
using FreeCourse.Services.Catalog.Dtos.Course;
using FreeCourse.Services.Catalog.Settings.Abstract;
using FreeCourse.Services.Catalog.Utilities;
using FreeCourse.Shared.Utilities.Results.Abstract;
using FreeCourse.Shared.Utilities.Results.ComplexTypes;
using FreeCourse.Shared.Utilities.Results.Concrete;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Application.Course
{
    public class CourseAppService : ICourseAppService
    {
        private readonly IMongoCollection<FreeCourse.Services.Catalog.Entities.Course> _courseCollection;
        private readonly IMongoCollection<FreeCourse.Services.Catalog.Entities.Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseAppService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<FreeCourse.Services.Catalog.Entities.Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<FreeCourse.Services.Catalog.Entities.Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;

        }

        public async Task<IDataResult<IList<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (!courses.Any())
            {
                return new DataResult<IList<CourseDto>>(ResultStatus.Error, message: Messages.Course.NotFound(true), data: null, statusCode: 404);
            }


            foreach (var course in courses)
            {
                course.Category = await _categoryCollection.Find<FreeCourse.Services.Catalog.Entities.Category>(x => x.Id == course.CategoryId).FirstAsync();
            }

            return new DataResult<IList<CourseDto>>(ResultStatus.Success, data: _mapper.Map<IList<CourseDto>>(courses), statusCode: 200);
        }

        public async Task<IDataResult<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<FreeCourse.Services.Catalog.Entities.Course>(x => x.Id == id).FirstOrDefaultAsync();
            course.Category = await _categoryCollection.Find<FreeCourse.Services.Catalog.Entities.Category>(x => x.Id == course.CategoryId).FirstAsync();
            if (course != null)
            {
                return new DataResult<CourseDto>(ResultStatus.Success, data: _mapper.Map<CourseDto>(course), statusCode: 200);
            }

            return new DataResult<CourseDto>(ResultStatus.Error, message: Messages.Course.NotFound(false), data: null, statusCode: 404);
        }

        public async Task<IDataResult<IList<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find<FreeCourse.Services.Catalog.Entities.Course>(x => x.UserId == userId).ToListAsync();

            if (!courses.Any())
            {
                return new DataResult<IList<CourseDto>>(ResultStatus.Error, message: Messages.Course.NotFound(true), data: null, statusCode: 404);
            }


            foreach (var course in courses)
            {
                course.Category = await _categoryCollection.Find<FreeCourse.Services.Catalog.Entities.Category>(x => x.Id == course.CategoryId).FirstAsync();
            }

            return new DataResult<IList<CourseDto>>(ResultStatus.Success, data: _mapper.Map<IList<CourseDto>>(courses), statusCode: 200);
        }

        public async Task<IDataResult<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<FreeCourse.Services.Catalog.Entities.Course>(courseCreateDto);
            newCourse.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);
            return new DataResult<CourseDto>(ResultStatus.Success, data: _mapper.Map<CourseDto>(newCourse), statusCode: 200);
        }
        public async Task<IResult> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<FreeCourse.Services.Catalog.Entities.Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);

            if (result != null)
            {
                return new Result(ResultStatus.Success, statusCode: 204);
            }

            return new Result(ResultStatus.Error, message: Messages.Course.NotFound(false), statusCode: 404);
        }

        public async Task<IResult> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0)
            {
                return new Result(ResultStatus.Success, statusCode: 204);
            }

            return new Result(ResultStatus.Error, message: Messages.Course.NotFound(false), statusCode: 404);
        }
    }

}
