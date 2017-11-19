using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Services.Interfaces
{
    public interface IEmailSender
    {
        void AccountConfirmationEmail(string email,string token);
    }
}
