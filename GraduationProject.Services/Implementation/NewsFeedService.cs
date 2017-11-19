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

        public NewsFeedService(IRepository<Friend> friendRepo,IRepository<Question> questionRepo,IRepository<Answer> answerRepo)
        {
            _friendRepo = friendRepo;
            _questionRepo = questionRepo;
            _answerRepo = answerRepo;
        }

        public Answer AddAnswer(Answer answer)
        {
            return _answerRepo.Insert(answer);
        }

        public IEnumerable<StudentQuestionVM> FollowingQuestions(string userId)
        {
            //Get All Following UserId
            var Friends = _friendRepo.GetAll().Include(u=>u.FriendTwo).ToList().Select(u=>u.FriendTwo.Id).ToList();
            //Get Questions With Answers Of These Users
            var allFollowingQuestions = _questionRepo.GetAll().Include(a => a.Answers).Include(u => u.User).ThenInclude(u=>u.Student).Where(q =>Friends.Contains(q.UserId));
            List<StudentQuestionVM> questionsList = new List<StudentQuestionVM>();
            foreach (var question in allFollowingQuestions)
            {
                StudentQuestionVM studentQuestion = new StudentQuestionVM()
                {
                    Id = question.Id,
                    Dislikes = question.Dislikes,
                    Likes = question.Likes,
                    QuestionHead = question.QuestionHead,
                    Username = question.User.Name,
                    //Image = question.User.Student.Image,
                    UserId = question.UserId
                };

                List<QuestionAnswerVM> questionAnswersList = new List<QuestionAnswerVM>();
                foreach (var answer in question.Answers)
                {
                    QuestionAnswerVM Answer = new QuestionAnswerVM()
                    {
                        Answer = answer.QuestionAnswer,
                        Id = answer.Id,
                        UserId = answer.UserId,
                        Username = answer.User.Name,
                        //UserImage = answer.User.Student.Image
                    };
                    questionAnswersList.Add(Answer);
                }//End Answers ForLoop
                studentQuestion.Answers = questionAnswersList;
                questionsList.Add(studentQuestion);
            }//End Questions ForLoop
            return questionsList;
        }
    }
}
