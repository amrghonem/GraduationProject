using GraduationProject.Data;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Services.Interfaces
{
    public interface INewsFeedService
    {
        IEnumerable<StudentQuestionVM> FollowingQuestions(string userId);
        Answer AddAnswer(Answer answer);
    }
}
