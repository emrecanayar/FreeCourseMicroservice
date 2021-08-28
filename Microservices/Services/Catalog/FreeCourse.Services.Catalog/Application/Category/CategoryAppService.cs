using AutoMapper;
using FreeCourse.Services.Catalog.Application.Category;
using FreeCourse.Services.Catalog.Dtos.Category;
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

namespace FreeCourse.Services.Catalog.Application
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly IMongoCollection<FreeCourse.Services.Catalog.Entities.Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryAppService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<FreeCourse.Services.Catalog.Entities.Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<IDataResult<IList<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            if (categories != null)
            {
                return new DataResult<IList<CategoryDto>>(resultStatus: ResultStatus.Success, data: _mapper.Map<List<CategoryDto>>(categories), statusCode: 200);
            }
            return new DataResult<IList<CategoryDto>>(resultStatus: ResultStatus.Error, data: null, message: Messages.Category.NotFound(true), statusCode: 404);
        }

        public async Task<IDataResult<CategoryDto>> CreateAsync(CategoryCreateDto categoryDto)
        {
            var category = _mapper.Map<FreeCourse.Services.Catalog.Entities.Category>(categoryDto);
            if (category != null)
            {
                await _categoryCollection.InsertOneAsync(category);
                return new DataResult<CategoryDto>(resultStatus: ResultStatus.Success, data: _mapper.Map<CategoryDto>(category), statusCode: 200);
            }
            return new DataResult<CategoryDto>(resultStatus: ResultStatus.Error, data: null, statusCode: 404);

        }

        public async Task<IDataResult<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<FreeCourse.Services.Catalog.Entities.Category>(x => x.Id == id).FirstOrDefaultAsync();
            if (category != null)
            {
                return new DataResult<CategoryDto>(resultStatus: ResultStatus.Success, data: _mapper.Map<CategoryDto>(category), statusCode: 200);
            }
            return new DataResult<CategoryDto>(resultStatus: ResultStatus.Error, data: null, message: Messages.Category.NotFound(false), statusCode: 404);
        }

        public async Task<IResult> DeleteAsync(string id)
        {
            var result = await _categoryCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0)
            {
                return new Result(ResultStatus.Success, statusCode: 204);
            }

            return new Result(ResultStatus.Error, message: Messages.Category.NotFound(false), statusCode: 404);
        }
    }
}
