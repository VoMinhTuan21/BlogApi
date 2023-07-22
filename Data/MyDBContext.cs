using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options) { }

        #region DBset
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(acc =>
            {
                acc.ToTable("Accounts");
                acc.HasKey(a => a.Id);
                acc.HasIndex(a => a.Username).IsUnique();
                acc.Property(a => a.Username).IsRequired().HasMaxLength(20);
                acc.Property(a => a.PasswordSalt).IsRequired();
                acc.Property(a => a.PasswordHash).IsRequired();
                acc.Property(a => a.Email).IsRequired();
            });

            modelBuilder.Entity<Author>(ath =>
            {
                ath.ToTable("Authors");
                ath.HasKey(a => a.Id);
                ath.Property(a => a.Name).IsRequired().HasMaxLength(100);
                ath.Property(a => a.Avatar).IsRequired();
                ath.Property(a => a.AccountId).IsRequired();
                ath.Property(a => a.Bio).IsRequired();

                ath.HasOne(a => a.Account)
                    .WithOne(a => a.Author)
                    .HasForeignKey<Author>(a => a.AccountId)
                    .HasConstraintName("FK_Author_Account").OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Category>(c =>
            {
                c.ToTable("Categories");
                c.HasKey(a => a.Id);
                c.Property(a => a.Name).IsRequired().HasMaxLength(100);

            });

            modelBuilder.Entity<Blog>(b =>
            {
                b.ToTable("Blogs");
                b.HasKey(blg => blg.Id);
                b.Property(blg => blg.Title).IsRequired();
                b.Property(blg => blg.CoverImage).IsRequired();
                b.Property(blg => blg.Content).IsRequired();
                b.Property(blg => blg.AuthorId).IsRequired();
                b.Property(blg => blg.CategoryId).IsRequired();
                b.Property(blg => blg.CreatedAt).HasDefaultValue<DateTime>(DateTime.UtcNow);
                b.Property(blg => blg.NumOfViews).HasDefaultValue<int>(0);
                b.Property(blg => blg.NumOfFavorite).HasDefaultValue<int>(0);
                b.Property(blg => blg.Status).HasDefaultValue<string>("pending");

                b.HasOne(blg => blg.Author)
                .WithMany(a => a.Blogs)
                .HasForeignKey(blg => blg.AuthorId)
                .HasConstraintName("FK_Blog_Author").OnDelete(DeleteBehavior.Restrict);

                b.HasOne(blg => blg.Category)
                .WithMany(c => c.Blogs)
                .HasForeignKey(blg => blg.CategoryId)
                .HasConstraintName("FK_Blog_Category").OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BlogImage>(bm =>
            {
                bm.ToTable("BlogImages");
                bm.HasKey(blgImg => blgImg.Id);

                bm.HasOne(blgImg => blgImg.Blog)
                .WithMany(b => b.BlogImages)
                .HasForeignKey(blgImg => blgImg.BlogId)
                .HasConstraintName("FK_BlogImage_Blog").OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Comment>(c =>
            {
                c.ToTable("Comments");
                c.HasKey(cmt => cmt.Id);
                c.Property(cmt => cmt.Content).IsRequired();
                c.Property(cmt => cmt.UserName).IsRequired();
                c.Property(cmt => cmt.UserEmail).IsRequired();
                c.Property(cmt => cmt.CreatedAt).HasDefaultValue<DateTime>(DateTime.UtcNow);

                c.HasOne(cmt => cmt.Blog)
                .WithMany(blg => blg.Comments)
                .HasForeignKey(cmt => cmt.BlogId)
                .HasConstraintName("FK_Comment_Blog")
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OTP>(o =>
            {
                o.ToTable("OTPs");
                o.HasKey(otp => otp.Id);
                o.Property(otp => otp.Email).IsRequired();
                o.Property(otp => otp.OTPCode).IsRequired();
                o.Property(otp => otp.ExpiredAt).HasDefaultValue<DateTime>(DateTime.UtcNow.AddMinutes(5));
                o.HasIndex(otp => otp.OTPCode).IsUnique();
            });
        }
    }
}
