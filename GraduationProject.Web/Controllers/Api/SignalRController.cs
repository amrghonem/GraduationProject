using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduationProject.Services.Interfaces;
using GraduationProject.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraduationProject.Web.Controllers.Api
{
    public class SignalRController : Controller
    {
        private ISignalrService _signalrSev;

        public SignalRController(ISignalrService signalrSrv)
        {
            _signalrSev = signalrSrv;
        }
        [Route("api/signalr/addnewconnection")]
        [HttpPost]
        public IActionResult AddNewConnection([FromBody]SignalRConnection con)
        {
            var result = _signalrSev.AddNewConnections(con);
            if (!string.IsNullOrEmpty(result))
                return Ok(new { Status = "Success" });
            return Ok(new { Status = "Failed" });
        }

        [Route("api/signalr/removeconnection")]
        [HttpPost]
        public IActionResult RemoveConnection([FromBody]SignalRConnection con)
        {
            var result = _signalrSev.RemoveConnections(con);
            if (result > 0)
                return Ok(new { Status = "Success" });
            return Ok(new { Status = "Failed" });
        }
    }
}
