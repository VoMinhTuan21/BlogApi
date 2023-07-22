using BlogApi.DTO;
using BlogApi.ReadModel;
using BlogApi.Services.Blog;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepo;

        public BlogController(IBlogRepository blogRepo)
        {
            _blogRepo = blogRepo;
        }
        [HttpPost]
        public IActionResult Create([FromForm] CreateBlogDTO dto)
        {
            try
            {
                var result = _blogRepo.Create(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is not HttpException)
                {
                    return BadRequest("Cannot create block");
                }
                throw;
            }
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            try
            {
                var result = _blogRepo.Get(Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is not HttpException)
                {
                    return BadRequest("Cannot get blog by Id");
                }
                throw;
            }
        }

        [HttpPut("{Id}")]
        public IActionResult Update(Guid Id, [FromForm] UpdateBlogDTO dto)
        {
            try
            {
                if (Id != dto.Id)
                {
                    throw new HttpException(HttpStatusCode.BadRequest, "Id in path doesnot match with id in body");
                };

                var result = _blogRepo.Update(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is not HttpException)
                {
                    return BadRequest("Cannot update blog");
                }
                throw;
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            try
            {
                var result = _blogRepo.Delete(Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is not HttpException)
                {
                    return BadRequest("Cannot delete blog");
                }
                throw;
            }
        }

    }
}
