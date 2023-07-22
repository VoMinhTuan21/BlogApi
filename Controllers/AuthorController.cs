using BlogApi.DTO;
using BlogApi.ReadModel;
using BlogApi.Services.Author;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpPost]
        public IActionResult Create([FromForm] CreateAuthorDTO createAuthorDTO)
        {
            try
            {
                var result = _authorRepository.Create(createAuthorDTO);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is not HttpException)
                {
                    return BadRequest("Cannot create author");
                }

                throw;
            }
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            try
            {
                var result = _authorRepository.Get(Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is not HttpException)
                {
                    return BadRequest("Cannot get author");
                }

                throw;
            }
        }

        [HttpPut]
        public IActionResult Update([FromForm] UpdateAuthorDTO dto)
        {
            try
            {
                var result = _authorRepository.Update(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is not HttpException)
                {
                    return BadRequest("Cannot update author");
                }
                throw;
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            try
            {
                var result = _authorRepository.Delete(Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is not HttpException)
                {
                    return BadRequest("Cannot delete author");
                }
                throw;
            }
        }
    }
}
