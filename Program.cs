using BlogApi.Data;
using BlogApi.Email;
using BlogApi.Extensions;
using BlogApi.Services.Author;
using BlogApi.Services.Blog;
using BlogApi.Services.BlogImage;
using BlogApi.Services.Category;
using BlogApi.Services.Cloudinary;
using BlogApi.Services.Comment;
using BlogApi.Services.OTP;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionStrings:MyDB"];

builder.Services.AddDbContext<MyDBContext>(options => options.UseSqlServer(connectionString));

//builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection(nameof(EmailConfiguration)));

var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

var account = builder.Configuration
        .GetSection("CloudinaryConfiguration")
        .Get<CloudinaryDotNet.Account>();

Cloudinary cloudinary = new Cloudinary(account);
cloudinary.Api.Secure = true;
builder.Services.AddSingleton(cloudinary);

builder.Services.AddScoped<ICloudinaryRepository, CloudinaryRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IOtpRepository, OtpRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBlogImageRepository, BlogImageRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseNativeGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
