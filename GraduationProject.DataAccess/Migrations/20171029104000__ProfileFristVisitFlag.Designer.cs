using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GraduationProject.DataAccess;

namespace GraduationProject.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171029104000__ProfileFristVisitFlag")]
    partial class _ProfileFristVisitFlag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GraduationProject.Data.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("QuestionAnswer");

                    b.Property<int>("QuestionId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("QuestionsAnswers");
                });

            modelBuilder.Entity("GraduationProject.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Gender");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("UserType");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("GraduationProject.Data.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Info");

                    b.Property<string>("Name");

                    b.Property<int>("SubCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("GraduationProject.Data.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Degree");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("GraduationProject.Data.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FriendOneId");

                    b.Property<string>("FriendTwoId");

                    b.HasKey("Id");

                    b.HasIndex("FriendOneId");

                    b.HasIndex("FriendTwoId");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("GraduationProject.Data.MainCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("MainCategories");
                });

            modelBuilder.Entity("GraduationProject.Data.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Dislikes");

                    b.Property<long>("Likes");

                    b.Property<string>("QuestionHead");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("GraduationProject.Data.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("GraduationProject.Data.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<bool>("FirstVisit");

                    b.Property<string>("Image");

                    b.Property<string>("Info");

                    b.Property<string>("School");

                    b.Property<string>("Universty");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("GraduationProject.Data.StudentCourse", b =>
                {
                    b.Property<int>("CourseId");

                    b.Property<string>("StudentId");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("CourseId", "StudentId", "Id");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourse");
                });

            modelBuilder.Entity("GraduationProject.Data.StudentExam", b =>
                {
                    b.Property<int>("ExamId");

                    b.Property<string>("StudentId");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ExamId", "StudentId", "Id");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentExam");
                });

            modelBuilder.Entity("GraduationProject.Data.StudentSkill", b =>
                {
                    b.Property<int>("SkillId");

                    b.Property<string>("StudentId");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("SkillId", "StudentId", "Id");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentSkill");
                });

            modelBuilder.Entity("GraduationProject.Data.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MainCategoryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("MainCategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GraduationProject.Data.Answer", b =>
                {
                    b.HasOne("GraduationProject.Data.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GraduationProject.Data.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GraduationProject.Data.Course", b =>
                {
                    b.HasOne("GraduationProject.Data.SubCategory", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GraduationProject.Data.Friend", b =>
                {
                    b.HasOne("GraduationProject.Data.ApplicationUser", "FriendOne")
                        .WithMany()
                        .HasForeignKey("FriendOneId");

                    b.HasOne("GraduationProject.Data.ApplicationUser", "FriendTwo")
                        .WithMany()
                        .HasForeignKey("FriendTwoId");
                });

            modelBuilder.Entity("GraduationProject.Data.Question", b =>
                {
                    b.HasOne("GraduationProject.Data.ApplicationUser", "User")
                        .WithMany("Questions")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GraduationProject.Data.Student", b =>
                {
                    b.HasOne("GraduationProject.Data.ApplicationUser", "User")
                        .WithOne("Student")
                        .HasForeignKey("GraduationProject.Data.Student", "ApplicationUserId");
                });

            modelBuilder.Entity("GraduationProject.Data.StudentCourse", b =>
                {
                    b.HasOne("GraduationProject.Data.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GraduationProject.Data.ApplicationUser", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GraduationProject.Data.StudentExam", b =>
                {
                    b.HasOne("GraduationProject.Data.Exam", "Exam")
                        .WithMany("StudentExams")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GraduationProject.Data.ApplicationUser", "Student")
                        .WithMany("StudentExams")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GraduationProject.Data.StudentSkill", b =>
                {
                    b.HasOne("GraduationProject.Data.Skill", "Skill")
                        .WithMany("StudentSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GraduationProject.Data.ApplicationUser", "Student")
                        .WithMany("StudentSkills")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GraduationProject.Data.SubCategory", b =>
                {
                    b.HasOne("GraduationProject.Data.MainCategory", "MainCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("MainCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GraduationProject.Data.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GraduationProject.Data.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GraduationProject.Data.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
