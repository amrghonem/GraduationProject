using GraduationProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using GraduationProject.Data.Models;
using GraduationProject.Infrastructure;
using GraduationProject.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services.Implementation
{
    public class NewsFeedService : INewsFeedService
    {
        private IRepository<Friend> _friendRepo;
        private IRepository<Question> _questionRepo;
        private IRepository<Answer> _answerRepo;
        private IRepository<Student> _repoStud;

        public NewsFeedService(IRepository<Friend> friendRepo,
            IRepository<Question> questionRepo,
            IRepository<Answer> answerRepo,
              IRepository<Student> repoStud)
        {
            _friendRepo = friendRepo;
            _questionRepo = questionRepo;
            _answerRepo = answerRepo;
            _repoStud = repoStud;
        }

        public IEnumerable<StudentQuestionVM> GetQuestionsByIds(List<int> ids)
        {
            var questions = new List<StudentQuestionVM>();
            foreach (var id in ids)
            {
                var question = _questionRepo.GetAll().SingleOrDefault(q=> q.Id==id);
                var userData = GetStudent(question.UserId);
                List<StudentQuestionVM> questionsList = new List<StudentQuestionVM>();

                    var studentData = GetStudent(question.UserId);
                    if (studentData != null)
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
                            Date = question.Date,
                            Gender = studentData.User.Gender
                        };

                        //List<QuestionAnswerVM> questionAnswersList = new List<QuestionAnswerVM>();
                        //foreach (var answer in question.Answers)
                        //{
                        //    var answerdStudentData = GetStudent(question.UserId);

                        //    QuestionAnswerVM Answer = new QuestionAnswerVM()
                        //    {
                        //        QuestionAnswer = answer.QuestionAnswer,
                        //        Id = answer.Id,
                        //        UserId = answer.UserId,
                        //        Username = answer.User.Name,
                        //        UserImage = answerdStudentData.Image,
                        //        Title = answerdStudentData.Title,
                        //        Date = answer.Date,
                        //        Gender = answerdStudentData.User.Gender
                        //    };
                        //    questionAnswersList.Add(Answer);
                        //}//End Answers ForLoop
                        //studentQuestion.Answers = questionAnswersList;

                        questionsList.Add(studentQuestion);
                    }
                 //End Student Questions
                return questionsList;
            }
            return questions;
        }

        public AnswerVM AddAnswer(Answer answer)
        {
            var result = _answerRepo.Insert(answer);
            if (result !=null)
            {
                answer.Date = DateTime.Now;
                var student = GetStudent(answer.UserId);

                return new AnswerVM()
                {
                    Username =student.User.Name,
                    UserImage = student.Image,
                    Title = student.Title,
                    UserId =  student.ApplicationUserId,
                    Id=result.Id,
                    Answer = result.QuestionAnswer,
                    Date = Convert.ToDateTime(answer.Date.ToString("yyyy-MM-dd")),
                    Gender = student.User.Gender
                };
            }
            return null;
            
        }

        public IEnumerable<StudentQuestionVM> FollowingQuestions(string userId)
        {
            //Get All Following UserId
            var Friends = _friendRepo.GetAll().Include(u=>u.FriendTwo).ToList().Select(u=>u.FriendTwo.Id).ToList();
            //Get Questions With Answers Of These Users
            var allFollowingQuestions = _questionRepo.GetAll().Include(a => a.Answers).Include(u => u.User)
                .ThenInclude(u=>u.Student).Where(q =>Friends.Contains(q.UserId));

            List<StudentQuestionVM> questionsList = new List<StudentQuestionVM>();
            foreach (var question in allFollowingQuestions)
            {
                var studentData = GetStudent(question.UserId);
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
                    Gender = studentData.User.Gender,
                    Date = Convert.ToDateTime(question.Date.ToString("yyyy-MM-dd"))
                };

                List<QuestionAnswerVM> questionAnswersList = new List<QuestionAnswerVM>();
                foreach (var answer in question.Answers)
                {
                    var answeredStudentData = GetStudent(answer.UserId);
                    QuestionAnswerVM Answer = new QuestionAnswerVM()
                    {
                        QuestionAnswer = answer.QuestionAnswer,
                        Id = answer.Id,
                        UserId = answer.UserId,
                        Username = answer.User.Name,
                        UserImage = answeredStudentData.Image,
                        Title =studentData.Title,
                        Date = Convert.ToDateTime(question.Date.ToString("yyyy-MM-dd")),
                        Gender = answer.User.Gender
                    };
                    questionAnswersList.Add(Answer);
                }//End Answers ForLoop
                studentQuestion.Answers = questionAnswersList;
                questionsList.Add(studentQuestion);
            }//End Questions ForLoop
            return questionsList;
        }

        public Student GetStudent(string id)
        {
            return _repoStud.GetAll().Include(u => u.User).SingleOrDefault(s => s.ApplicationUserId == id);
        }
    }

    public class AnswerVM
    {
        public string Answer { get; set; }
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string UserImage { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public DateTime Date { get; set; }
    }
}
