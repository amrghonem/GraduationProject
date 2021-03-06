﻿using GraduationProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using GraduationProject.Data;
using GraduationProject.Infrastructure;
using System.Linq;
using GraduationProject.DataAccess;
using Microsoft.EntityFrameworkCore;
using GraduationProject.Data.Models;

namespace GraduationProject.Services.Implementation
{
    public class StudentProfileService : IStudentProfileService
    {
        private ApplicationDbContext _ctx;
        #region Attrs
        private IRepository<Student> _repoStud;
        private IRepository<StudentSkill> _studentSkillRepo;
        private IRepository<Question> _questionsRepo;
        private IRepository<Answer> _answerRepo;
        private IRepository<StudentCourse> _courseRepo;
        private IRepository<StudentExam> _examRepo;
        private IRepository<Friend> _frindRepo;
        private IRepository<Skill> _skillRepo;

        //ApplicationDbContext _db;
        #endregion

        #region Ctor
        public StudentProfileService(
             IRepository<Student> Stuedntrepo
            ,IRepository<StudentSkill> studentSkillRepo
            ,IRepository<Answer> answerRepo
            ,IRepository<StudentCourse> courseRepo
            ,IRepository<StudentExam> examRepo
            ,IRepository<Friend> frindRepo
            , IRepository<Skill> skillRepo
            , IRepository<Question> questionsRepo, ApplicationDbContext ctx)
        {
            _ctx = ctx;
            _repoStud = Stuedntrepo;
            _studentSkillRepo = studentSkillRepo;
            _questionsRepo = questionsRepo;
            _answerRepo = answerRepo;
            _courseRepo = courseRepo;
            _examRepo = examRepo;
            _frindRepo = frindRepo;
            _skillRepo = skillRepo;
        }
        #endregion

        #region Methods
        public Student CreateStudentProfile(Student profile)
        {
            if (profile == null)
                throw new ArgumentNullException();
            return _repoStud.Insert(profile);
        }

        public StudentProfileVM GetStudentProfile(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException();

            //Get If User Have Profile Or Not 
            //If Have No Profile
            //Create Profile And Return All Profile Data
            if (!_repoStud.GetAll().Where(s => s.ApplicationUserId == user.Id).Any())
            {
                Student newStudentProfile = new Student() { ApplicationUserId = user.Id, FirstVisit = true };
                CreateStudentProfile(newStudentProfile);
            }
            //If Have Profile Return All Profile Data
            return GetStudentFullProfile(user.Id);
        }

        public Student EditProile(Student student,ApplicationUser user)
        {
            if (student == null)
                throw new ArgumentNullException();
            //After Edting Means User Complete His Profile.
            student.FirstVisit = false;
            //Edit User Account Data ==> Gender,Birthdate,Name
            var userToEdit = _ctx.Users.AsNoTracking().SingleOrDefault(u => u.Id == user.Id);
            userToEdit = user;
            _ctx.SaveChanges();
            _ctx.Entry(userToEdit).State = EntityState.Detached;
            //Edit Studetnt 
            //var oldStudent = _repoStud.GetAll().SingleOrDefault(s => s.ApplicationUserId == student.ApplicationUserId);
            //Image Updated In Separate Method .
          //student.Image = oldStudent.Image;
            var res = _repoStud.Update(student);
            _ctx.SaveChanges();
            return  res;
        }

        public StudentSkillVM AddStudentSkill(StudentSkill newStudentSkill)
        {

            var getStudentSkill = _studentSkillRepo.GetAll().Where(ss => ss.StudentId == newStudentSkill.StudentId
            && ss.SkillId == newStudentSkill.SkillId).Any();
            if (getStudentSkill)
                return null;
            else {
                    var result = _studentSkillRepo.Insert(newStudentSkill);
                   var skill = _skillRepo.GetAll().Include(s => s.StudentSkills).SingleOrDefault(s => s.Id == result.SkillId);
                return new StudentSkillVM() { Id=result.Id , SkillName= skill.Name};
                }
        }

        public int DeleteStudentSkill(int studentakillid)
        {
            return _studentSkillRepo.Delete(_studentSkillRepo.Get(studentakillid));
        }

        public Question AddQuestion(Question newQuestion)
        {
            newQuestion.Date = DateTime.Now;
            var q = _questionsRepo.Insert(newQuestion);
            return q;
        }

        public int DeleteQuestion(int questionId)
        {
            return _questionsRepo.Delete(_questionsRepo.Get(questionId));
        }

        public Friend FollowFriend(ApplicationUser user,ApplicationUser frined)
        {
            try
            {
                var f = new Friend()
                {
                    FriendOneId = user.Id,
                    FriendTwoId = frined.Id
                };
                return _frindRepo.Insert(f);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int UnFollowFriendint (int id)
        {
            return _frindRepo.Delete(_frindRepo.Get(id));
        }

        public string UpdateStudentImage(string userid , string imagepath)
        {
            var Student = _repoStud.GetAll().SingleOrDefault(s => s.ApplicationUserId == userid);
            Student.Image = imagepath;
            _repoStud.Update(Student);
            return Student.Image;
        }


        StudentProfileVM GetStudentFullProfile(string userId)
        {
            //Student Info
            var studentInfo = _repoStud.GetAll().Include(u => u.User).SingleOrDefault(u => u.ApplicationUserId == userId);
            StudentProfileVM studentProfile = new StudentProfileVM() {
                ProfileId= studentInfo.Id,
                BirthDate = studentInfo.User.BirthDate.ToString("MM/dd/yyyy"),
                Email = studentInfo.User.Email,
                FirstVisit = studentInfo.FirstVisit,
                Info = studentInfo.Info,
                Username = studentInfo.User.Name,
                Gender =studentInfo.User.Gender,
                Image = studentInfo.Image,
                University = studentInfo.Universty,
                UniverstyYearFrom=studentInfo.UniverstyYearFrom,
                UniverstyYearTo = studentInfo.UniverstyYearTo,
                SchholYearFrom=studentInfo.SchholYearFrom,
                SchholYearTo =studentInfo.SchholYearTo,
                UserId=studentInfo.ApplicationUserId,
                School=studentInfo.School,
                Title=studentInfo.Title
            };
            //Student Skills
            var allSkills = GetStudentSkills(userId);
            List<StudentSkillVM> skillsList = new List<StudentSkillVM>();
            foreach (var skill in allSkills)
            {
                StudentSkillVM studentSkill = new StudentSkillVM() {
                    Id = skill.Id,
                    SkillName=skill.Skill.Name
                };
                skillsList.Add(studentSkill);
            }
            studentProfile.Skills = skillsList;
            //End Student Skills
            //Student Exams
            var allExams = GetStudentExams(userId);
            List<StudentExamVM> examsList = new List<StudentExamVM>();
            foreach (var exam in allExams)
            {
                StudentExamVM studentExam = new StudentExamVM()
                {
                    ExamName = exam.Exam.Title,
                    StudentDegree = exam.Degree
                };
                examsList.Add(studentExam);
            }
            studentProfile.Exams = examsList;
            //End Student Exams
            //Student Courses
            var allCourses = GetStudentCourses(userId);
            List<StudentCourseVM> coursesList = new List<StudentCourseVM>();
            foreach (var course in allCourses)
            {
                StudentCourseVM studentCourse = new StudentCourseVM()
                {
                    CourseName = course.Course.Name,
                    Info=course.Course.Info
                };
                coursesList.Add(studentCourse);
            }
            studentProfile.Courses = coursesList;
            //End Student Courses
            //Student Firneds
            var allFriends = GetStudentFriends(userId);
            List<StudentFollowingVM> finedsList = new List<StudentFollowingVM>();
            foreach (var frined in allFriends)
            {
                var friendData = GetStudent(frined.FriendTwoId);
                if (friendData != null)
                {
                    StudentFollowingVM studentFriend = new StudentFollowingVM()
                    {
                        Id = frined.Id,
                        Name = frined.FriendTwo.Name,
                        FriendId = frined.FriendTwo.Id,
                        Title = friendData.Title,
                        FriendImage = friendData.Image,
                        Gender = friendData.User.Gender
                    };
                    finedsList.Add(studentFriend);
                }
            }
            studentProfile.Friends = finedsList;
            //End Student Firneds
            //Student Questions
            var allQuestions = GetStudentQuestions(userId);
            List<StudentQuestionVM> questionsList = new List<StudentQuestionVM>();
            foreach (var question in allQuestions)
            {
                var studentData = GetStudent(question.UserId);
                if (studentData !=null)
                {
                    StudentQuestionVM studentQuestion = new StudentQuestionVM()
                    {
                        Id = question.Id,
                        Dislikes = question.Dislikes,
                        Likes = question.Likes,
                        QuestionHead = question.QuestionHead,
                        Username = question.User.Name,
                        Image = studentData.Image,
                        UserId = question.UserId,
                        Title = studentData.Title,
                        Date=question.Date,
                        Gender = studentData.User.Gender
                    };

                    List<QuestionAnswerVM> questionAnswersList = new List<QuestionAnswerVM>();
                    foreach (var answer in question.Answers)
                    {
                        var answerdStudentData = GetStudent(question.UserId);

                        QuestionAnswerVM Answer = new QuestionAnswerVM()
                        {
                            QuestionAnswer = answer.QuestionAnswer,
                            Id = answer.Id,
                            UserId = answer.UserId,
                            Username = answer.User.Name,
                            UserImage = answerdStudentData.Image,
                            Title = answerdStudentData.Title,
                            Date=answer.Date,
                            Gender = answerdStudentData.User.Gender
                        };
                        questionAnswersList.Add(Answer);
                    }//End Answers ForLoop
                    studentQuestion.Answers = questionAnswersList;

                    questionsList.Add(studentQuestion);
                }

            }//End Questions ForLoop
            studentProfile.Questions = questionsList;
            //End Student Questions
            return studentProfile;
        }

        public int StudentFollowersCount(string id)
        {
            return _frindRepo.GetAll().Where(u => u.FriendTwoId == id).ToList().Count; ;
        }
        public int StudentFollowingCount(string id)
        {
            return _frindRepo.GetAll().Where(u => u.FriendOneId == id).ToList().Count; ;
        }

        //End Get Profile
        //Get Student Skills
        IEnumerable<StudentSkill> GetStudentSkills(string userId)
        {
           return  _studentSkillRepo.GetAll().Include(s=> s.Skill).Where(u => u.StudentId == userId);
        }
        //Get Student Exams
         IEnumerable<StudentExam> GetStudentExams(string userId)
        {
            return _examRepo.GetAll().Include(e=> e.Exam).Where(u => u.StudentId == userId);
        }
        //Get Student Courses
         IEnumerable<StudentCourse> GetStudentCourses(string userId)
        {
            return _courseRepo.GetAll().Include(c=> c.Course).Where(u => u.StudentId == userId);
        }
        //Get Student Questions With Answers
         IEnumerable<Question> GetStudentQuestions(string userId)
        {
            return _questionsRepo.GetAll().Include(a=>a.Answers).Include(u=>u.User.Student).Where(u => u.UserId == userId).OrderByDescending(q=>q.Date);
        }
        //Get Student Friends
        public IEnumerable<Friend> GetStudentFriends(string userId)
        {
            return _frindRepo.GetAll().Where(u => u.FriendOne.Id == userId).Include(u=>u.FriendOne).Include(u=> u.FriendTwo);
        }
        public Student GetStudent(string id)
        {
            return _repoStud.GetAll().Include(u=> u.User).SingleOrDefault(s=>s.ApplicationUserId==id);
        }
#endregion
    }
}
