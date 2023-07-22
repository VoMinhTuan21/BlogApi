using BlogApi.DTO;
using BlogApi.ReadModel;
using BlogApi.Services.Category;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _categoryRepository.GetAll();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCategoryDTO categoryDTO)
        {
            var result = _categoryRepository.Create(categoryDTO);

            return Ok(result);
        }

        [HttpPut("{Id}")]
        public IActionResult update(Guid Id, [FromBody] UpdateCategoryDTO categoryDTO)
        {
            try
            {
                var result = _categoryRepository.Update(Id, categoryDTO);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is not HttpException)
                {
                    throw new HttpException(HttpStatusCode.BadRequest, "Update category fail");
                }

                throw;
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            try
            {
                var result = _categoryRepository.Delete(Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is not HttpException)
                {
                    throw new HttpException(HttpStatusCode.BadRequest, "Delete category fail");
                }

                throw;
            }
        }
    }
}
