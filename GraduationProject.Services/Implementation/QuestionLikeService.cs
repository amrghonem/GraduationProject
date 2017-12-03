using GraduationProject.Data;
using GraduationProject.Infrastructure;
using GraduationProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace GraduationProject.Services.Implementation
{
    public class QuestionLikeService : IQuestionLikeService
    {
        private IRepository<QuestionLike> _queLikeRepo;
        private IRepository<Question> _queRepo;

        public QuestionLikeService(IRepository<QuestionLike> queLikesRepo,
                                    IRepository<Question> queRepo)
        {
            _queLikeRepo = queLikesRepo;
            _queRepo = queRepo;
        }

        public QuestionLike LikeDislikeQuestion(QuestionLike like)
        {
            //Check if liked or disliked before 
            var res = _queLikeRepo.GetAll().SingleOrDefault(q => q.QuestionId == like.QuestionId && q.UserId == like.UserId);
            //Like/Dislike Counter 
            var question = _queRepo.Get(like.QuestionId);
            if (res !=null)
            {
                //Check If Like/Dislike To Toggle
                if (res.State == like.State )//Means Need To Remove Like/Dis Action
                {
                    if (like.State == "like")
                    {
                        question.Likes -= 1;
                        _queRepo.Update(question);
                    }
                    else
                    {
                        question.Dislikes -= 1;
                        _queRepo.Update(question);
                    }
                    _queLikeRepo.Delete(res);
                    return res;
                }

                res.State = like.State;
                if (like.State == "like")
                {
                    question.Likes += 1;
                    _queRepo.Update(question);
                }
                else
                {
                    question.Dislikes += 1;
                    _queRepo.Update(question);
                }
                return _queLikeRepo.Update(res);
            }
            else //Means New Action
            {
                if (like.State == "like")
                {
                    question.Likes += 1;
                    _queRepo.Update(question);
                }
                else
                {
                    question.Dislikes += 1;
                    _queRepo.Update(question);
                }
                try
                {

                    return _queLikeRepo.Insert(like);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

    }
}
