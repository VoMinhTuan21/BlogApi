using BlogApi.Data;
using BlogApi.DTO;
using BlogApi.Helpers;
using BlogApi.ReadModel;
using BlogApi.Services.Cloudinary;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BlogApi.Services.Author
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly MyDBContext _context;
        private readonly ICloudinaryRepository _cloudinaryRepository;

        public AuthorRepository(MyDBContext context, ICloudinaryRepository cloudinaryRepository)
        {
            _context = context;
            _cloudinaryRepository = cloudinaryRepository;
        }
        public ApiResponse<AuthorRM> Create(CreateAuthorDTO createAuthorDTO)
        {
            var author = _context.Authors.Include(a => a.Account).SingleOrDefault(a => a.Account.Email == createAuthorDTO.Email
                                                                || a.Account.Username == createAuthorDTO.Username);

            if (author != null)
            {
                if (author.Account.Username == createAuthorDTO.Username)
                {
                    throw new HttpException(HttpStatusCode.Conflict, "Username has been used");
                }
                if (author.Account.Email == createAuthorDTO.Email)
                {
                    throw new HttpException(HttpStatusCode.Conflict, "Email has been used");
                }
            }

            var uploadImageResult = _cloudinaryRepository.UploadImage(createAuthorDTO.Avatar, "authors");


            var hash = PasswordHelper.HashPassword(createAuthorDTO.Password, out var salt);

            var newAccount = new Data.Account
            {
                Email = createAuthorDTO.Email,
                PasswordHash = hash,
                PasswordSalt = Convert.ToHexString(salt),
                Role = "author",
                Username = createAuthorDTO.Username
            };

            var newAuthor = new Data.Author
            {
                Name = createAuthorDTO.Name,
                Avatar = uploadImageResult.PublicId,
                Bio = createAuthorDTO.Bio,
                AccountId = newAccount.Id
            };

            _context.Add(newAccount);
            _context.Add(newAuthor);
            _context.SaveChanges();

            return new ApiResponse<AuthorRM>
            {
                Message = "Create author success",
                Data = new AuthorRM
                {
                    Id = newAuthor.Id,
                    Avatar = uploadImageResult.SecureUrl.ToString(),
                    Bio = newAuthor.Bio,
                    Name = newAuthor.Name
                }
            };
        }

        public ApiResponse<string> Delete(Guid Id)
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == Id);

            if (author == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Author not found");
            }

            _cloudinaryRepository.Delete(author.Avatar);

            // check if author has any blogs and delete these blogs

            _context.Remove(author);
            _context.SaveChanges();

            return new ApiResponse<string>
            {
                Message = "Delete author success",
                Data = Id.ToString()
            };
        }

        public ApiResponse<AuthorRM> Get(Guid Id)
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == Id);

            if (author == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "There is no author with this Id");
            }

            return new ApiResponse<AuthorRM>
            {
                Message = "Get author success",
                Data = new AuthorRM
                {
                    Id = author.Id,
                    Avatar = _cloudinaryRepository.GetURL(author.Avatar),
                    Name = author.Name,
                    Bio = author.Bio
                }
            };
        }

        public ApiResponse<AuthorRM> Update(UpdateAuthorDTO updateAuthorDTO)
        {
            var author = _context.Authors.Include(a => a.Account).SingleOrDefault(a => a.Id == updateAuthorDTO.Id)
                ?? throw new HttpException(HttpStatusCode.NotFound, "Author not found");

            if (updateAuthorDTO.Name != null)
            {
                author.Name = updateAuthorDTO.Name;
            }
            if (updateAuthorDTO.Email != null)
            {
                author.Account.Email = updateAuthorDTO.Email;
            }
            if (updateAuthorDTO.Bio != null)
            {
                author.Bio = updateAuthorDTO.Bio;
            }
            if (updateAuthorDTO.Avatar != null)
            {
                var uploadImageResult = _cloudinaryRepository.UploadImage(updateAuthorDTO.Avatar, "authors");

                var deleteResult = _cloudinaryRepository.Delete(author.Avatar);
                author.Avatar = uploadImageResult.PublicId;

            }

            _context.SaveChanges();

            return new ApiResponse<AuthorRM>
            {
                Message = "Update author success",
                Data = new AuthorRM
                {
                    Id = author.Id,
                    Name = author.Name,
                    Bio = author.Bio,
                    Avatar = _cloudinaryRepository.GetURL(author.Avatar)
                }
            };
        }
    }
}
