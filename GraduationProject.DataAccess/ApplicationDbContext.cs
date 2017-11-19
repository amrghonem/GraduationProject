using GraduationProject.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> QuestionsAnswers { get; set; }
        public DbSet<Friend> Friends { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Student Skills
            builder.Entity<StudentSkill>()
                .Property(i => i.Id).ValueGeneratedOnAdd();

            builder.Entity<StudentSkill>()
                .HasKey(s => new { s.SkillId, s.StudentId,s.Id });

            builder.Entity<StudentSkill>()
                .HasOne(s => s.Student)
                .WithMany(ss => ss.StudentSkills)
                .HasForeignKey(s => s.StudentId);

            builder.Entity<StudentSkill>()
                .HasOne(s => s.Skill)
                .WithMany(ss => ss.StudentSkills)
                .HasForeignKey(s => s.SkillId);
            //End Student Skills
            //Student Exams
            builder.Entity<StudentExam>()
                .Property(i => i.Id).ValueGeneratedOnAdd();

            builder.Entity<StudentExam>()
                .HasKey(s => new { s.ExamId, s.StudentId, s.Id });

            builder.Entity<StudentExam>()
                .HasOne(s => s.Student)
                .WithMany(ss => ss.StudentExams)
                .HasForeignKey(s => s.StudentId);

            builder.Entity<StudentExam>()
                .HasOne(s => s.Exam)
                .WithMany(ss => ss.StudentExams)
                .HasForeignKey(s => s.ExamId);
            //End Student Exams
            //Student Courses
            builder.Entity<StudentCourse>()
                .Property(i => i.Id).ValueGeneratedOnAdd();

            builder.Entity<StudentCourse>()
                .HasKey(s => new { s.CourseId, s.StudentId, s.Id });

            builder.Entity<StudentCourse>()
                .HasOne(s => s.Student)
                .WithMany(ss => ss.StudentCourses)
                .HasForeignKey(s => s.StudentId);

            builder.Entity<StudentCourse>()
                .HasOne(s => s.Course)
                .WithMany(ss => ss.StudentCourses)
                .HasForeignKey(s => s.CourseId);
            //End Student Courses

            //Friends Mapping
            builder.Entity<Friend>().HasKey(f => new {f.Id,f.FriendOneId, f.FriendTwoId });

            builder.Entity<Friend>().Property(f => f.Id).UseSqlServerIdentityColumn();

            builder.Entity<Friend>()
                .HasOne(e => e.FriendOne)
                .WithMany(e => e.Friends)
                .HasForeignKey(e => e.FriendOneId);


            builder.Entity<Friend>()
                .HasOne(e => e.FriendTwo)
                .WithMany(e => e.Friends1)
                .HasForeignKey(e => e.FriendTwoId);
            //End Friends Mapping

        }
    }
}
