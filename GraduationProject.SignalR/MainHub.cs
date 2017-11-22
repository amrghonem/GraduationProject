using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace GraduationProject.SignalR
{
    public class MainHub : Hub
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Question">Question Object Which Will Be Pushed To Client Browser .</param>
        /// <param name="Connections">List Of SignalR Connection Which Will Be Used To Push Question Throgh  It .</param>
        public void OnStudentAddQuestion()
        {

        }

        public override Task OnConnected()
        {
            var userId = Context.QueryString["UserId"];
            var connectionId = Context.ConnectionId;
            //Call Graduation Project Api To Save Connection Id 
            return base.OnConnected();  
        }
    }
}