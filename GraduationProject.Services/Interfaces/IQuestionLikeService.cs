using GraduationProject.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Services.Interfaces
{
    public interface IQuestionLikeService
    {
        (QuestionLike, long, long) LikeDislikeQuestion(QuestionLike like);
    }
}
