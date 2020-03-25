using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwsTest;

namespace TwsTestWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MissionController : ControllerBase
    {
        [HttpPost]
        [Route("Run")]
        public ReturnDe Run(MissionDe req)
        {
            var mission = new Mission(req.Parameters);
            var res = mission.RunMission();
            if (res)
            {
                return ReturnDe.Success(mission.Result);
            }
            else
            {
                return ReturnDe.Fail(mission.Message);
            }
        }

        public class ReturnDe
        {
            public bool OK { get; set; } = true;
            public int Status { get; set; } = 0;
            public string Message { get; set; } = "";
            public string[] Mapped { get; set; } = null;

            public static ReturnDe Fail(string message, int status = -1) { return new ReturnDe() { OK = false, Message = message, Status = status }; }
            public static ReturnDe Success(string message, int status = 0) { return new ReturnDe() { Message = message, Status = status }; }
        }

        public class MissionDe
        {
            public string Parameters { get; set; } = "";
        }

    }
}