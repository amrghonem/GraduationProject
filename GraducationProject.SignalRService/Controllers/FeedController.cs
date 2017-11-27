using GraducationProject.SignalRService.Hubs;
using GraducationProject.SignalRService.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GraducationProject.SignalRService.Controllers
{
    [EnableCors("*", "*", "*")]
    public class FeedController : ApiController
    {
        [Route("api/signalr/newquestion")]
        [HttpPost]
        public HttpResponseMessage NewQuestion([FromBody]NewQuestionVM model)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MainHub>();
            foreach (var con in model.Connection)
            {
                hubContext.Clients.Client(con).newQuestion(model.Id);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
 
}
