﻿// <auto-generated />
using System;
using BlogApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlogApi.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20230718120638_UpdateCategoryTable")]
    partial class UpdateCategoryTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlogApi.Data.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("BlogApi.Data.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Authors", (string)null);
                });

            modelBuilder.Entity("BlogApi.Data.Blog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 7, 18, 12, 6, 38, 525, DateTimeKind.Utc).AddTicks(256));

                    b.Property<int>("NumOfFavorite")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("NumOfViews")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("pending");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Blogs", (string)null);
                });

            modelBuilder.Entity("BlogApi.Data.BlogImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BlogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.ToTable("BlogImages", (string)null);
                });

            modelBuilder.Entity("BlogApi.Data.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("BlogApi.Data.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BlogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 7, 18, 12, 6, 38, 526, DateTimeKind.Utc).AddTicks(2113));

                    b.Property<Guid?>("RepliedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("RepliedBy")
                        .IsUnique()
                        .HasFilter("[RepliedBy] IS NOT NULL");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("BlogApi.Data.Author", b =>
                {
                    b.HasOne("BlogApi.Data.Account", "Account")
                        .WithOne("Author")
                        .HasForeignKey("BlogApi.Data.Author", "AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Author_Account");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BlogApi.Data.Blog", b =>
                {
                    b.HasOne("BlogApi.Data.Author", "Author")
                        .WithMany("Blogs")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Blog_Author");

                    b.HasOne("BlogApi.Data.Category", "Category")
                        .WithMany("Blogs")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Blog_Category");

                    b.Navigation("Author");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BlogApi.Data.BlogImage", b =>
                {
                    b.HasOne("BlogApi.Data.Blog", "Blog")
                        .WithMany("BlogImages")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_BlogImage_Blog");

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("BlogApi.Data.Comment", b =>
                {
                    b.HasOne("BlogApi.Data.Blog", "Blog")
                        .WithMany("Comments")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Comment_Blog");

                    b.HasOne("BlogApi.Data.Comment", "ReplyComment")
                        .WithOne()
                        .HasForeignKey("BlogApi.Data.Comment", "RepliedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Blog");

                    b.Navigation("ReplyComment");
                });

            modelBuilder.Entity("BlogApi.Data.Account", b =>
                {
                    b.Navigation("Author");
                });

            modelBuilder.Entity("BlogApi.Data.Author", b =>
                {
                    b.Navigation("Blogs");
                });

            modelBuilder.Entity("BlogApi.Data.Blog", b =>
                {
                    b.Navigation("BlogImages");

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("BlogApi.Data.Category", b =>
                {
                    b.Navigation("Blogs");
                });
#pragma warning restore 612, 618
        }
    }
}
