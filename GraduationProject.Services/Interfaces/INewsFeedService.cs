using GraduationProject.Data;
using GraduationProject.Data.Models;
using GraduationProject.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Services.Interfaces
{
    public interface INewsFeedService
    {
        IEnumerable<StudentQuestionVM> FollowingQuestions(string userId);
        AnswerVM AddAnswer(Answer answer);
        IEnumerable<StudentQuestionVM> GetQuestionsByIds(List<int> ids);
    }
}
