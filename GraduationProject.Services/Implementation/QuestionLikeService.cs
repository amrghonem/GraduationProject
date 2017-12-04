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

        public (QuestionLike,long,long) LikeDislikeQuestion(QuestionLike like)
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
                    return (res,question.Likes,question.Dislikes);
                }
                else
                {
                    if (like.State =="like")
                    {
                        question.Likes += 1;
                        question.Dislikes -= 1;
                        res.State = "like";
                        _queLikeRepo.Update(res);
                        _queRepo.Update(question);
                    }
                    else
                    {
                        question.Likes -= 1;
                        question.Dislikes += 1;
                        res.State = "dislike";
                        _queLikeRepo.Update(res);
                        _queRepo.Update(question);
                    }
                }
                return (_queLikeRepo.Update(res),question.Likes,question.Dislikes);
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

                return (_queLikeRepo.Insert(like), question.Likes, question.Dislikes);               
            }
        }

    }
}
