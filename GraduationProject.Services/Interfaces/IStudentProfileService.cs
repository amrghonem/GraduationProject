using GraduationProject.Data;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Services.Interfaces
{
    public interface IStudentProfileService
    {
        Student CreateStudentProfile(Student profile);
        StudentProfileVM GetStudentProfile(ApplicationUser user);
        Student EditProile(Student student);
        bool AddStudentSkill(StudentSkill newStudentSkill);
        int DeleteStudentSkill(int studentakillid);
        Question AddQuestion(Question newQuestion);
        int DeleteQuestion(int questionId);
        //StudentProfileVM GetStudentFullProfile(string userId);
        int UnFollowFriendint(int id);
        Friend FollowFriend(ApplicationUser user, ApplicationUser frined);
    }
}
