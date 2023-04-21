using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningApp.Facade.API.Controllers.Base
{
    [Produces("application/json")]
    [Route("api/v1")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
