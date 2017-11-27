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
        Student EditProile(Student student,ApplicationUser user);
        StudentSkillVM AddStudentSkill(StudentSkill newStudentSkill);
        int DeleteStudentSkill(int studentakillid);
        Question AddQuestion(Question newQuestion);
        int StudentFollowersCount(string id);
        int StudentFollowingCount(string id);
        int DeleteQuestion(int questionId);
        //StudentProfileVM GetStudentFullProfile(string userId);
        int UnFollowFriendint(int id);
        Friend FollowFriend(ApplicationUser user, ApplicationUser frined);
        Student GetStudent(string id);
        string UpdateStudentImage(string userid, string imagepath);
        IEnumerable<Friend> GetStudentFriends(string userId);
    }
}
