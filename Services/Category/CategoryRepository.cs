using BlogApi.Data;
using BlogApi.DTO;
using BlogApi.ReadModel;
using System.Net;

namespace BlogApi.Services.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyDBContext _context;

        public CategoryRepository(MyDBContext context)
        {
            _context = context;
        }

        public ApiResponse<CategoryRM> Create(CreateCategoryDTO dto)
        {
            try
            {
                var existedCtg = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == dto.Name.ToLower());

                if (existedCtg != null)
                {
                    throw new HttpException(HttpStatusCode.Conflict, "This name has already been used.");
                }

                var category = new Data.Category
                {
                    Name = dto.Name,
                };

                _context.Add(category);
                _context.SaveChanges();

                var ctgRM = new CategoryRM
                {
                    Id = category.Id,
                    Name = category.Name
                };

                return new ApiResponse<CategoryRM>
                {
                    Message = "Create category success",
                    Data = ctgRM
                };

            }
            catch (Exception ex)
            {
                if (ex is not HttpException)
                {
                    throw new HttpException(HttpStatusCode.BadRequest, "Create category fail");
                }

                throw;
            }
        }

        public ApiResponse<string> Delete(Guid Id)
        {
            var category = _context.Categories.SingleOrDefault(e => e.Id == Id)
                ?? throw new HttpException(HttpStatusCode.NotFound, "Category not found");

            _context.Remove(category);
            _context.SaveChanges();

            return new ApiResponse<string>
            {
                Message = "Delete cagetory success",
                Data = Id.ToString(),
            };
        }

        public CategoryRM Get(Guid Id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == Id)
                ?? throw new HttpException(HttpStatusCode.NotFound, "Category not found");
            return new CategoryRM
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public ApiResponse<List<CategoryRM>> GetAll()
        {
            try
            {
                var categories = _context.Categories.Select(c => new CategoryRM
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

                return new ApiResponse<List<CategoryRM>>
                {
                    Message = "Get categories success.",
                    Data = categories
                };
            }
            catch (Exception ex)
            {
                throw new HttpException(HttpStatusCode.BadRequest, "Get categories fail");
            }
        }

        public ApiResponse<CategoryRM> Update(Guid Id, UpdateCategoryDTO dto)
        {
            if (Id != dto.Id)
            {
                throw new HttpException(HttpStatusCode.NotAcceptable, "Id in the path not match with Id in body");
            };

            var existedName = _context.Categories.SingleOrDefault(e => e.Name.ToLower() == dto.Name.ToLower());

            if (existedName != null && existedName.Id != Id)
            {
                throw new HttpException(HttpStatusCode.Conflict, "Please use another name");
            }

            var category = _context.Categories.SingleOrDefault(e => e.Id == Id)
                ?? throw new HttpException(HttpStatusCode.NotFound, "Category not found");

            category.Name = dto.Name;

            _context.SaveChanges();

            return new ApiResponse<CategoryRM>
            {
                Message = "Update category success.",
                Data = new CategoryRM
                {
                    Id = category.Id,
                    Name = category.Name
                }
            };
        }
    }
}
