using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using GraducationProject.SignalRService.Models;

namespace GraducationProject.SignalRService.Hubs
{
    [EnableCors("*","*","*")]
    public class MainHub : Hub
    {
       
        public override Task OnConnected()
        {
            NewConnectionVM newCon = new NewConnectionVM()
            {
                ApplicationUserId = Context.QueryString["userId"],
                ConnectionId = Context.ConnectionId
            };
            
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var result = client.UploadString("http://localhost:2449/api/signalr/addnewconnection", "POST", JsonConvert.SerializeObject(newCon));
            }
            
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            NewConnectionVM newCon = new NewConnectionVM()
            {
                ApplicationUserId = Context.QueryString["userId"],
                ConnectionId = Context.ConnectionId
            };

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var result = client.UploadString("http://localhost:2449/api/signalr/removeconnection", "POST", JsonConvert.SerializeObject(newCon));
            }
            return base.OnDisconnected(stopCalled);
        }       
    }
}