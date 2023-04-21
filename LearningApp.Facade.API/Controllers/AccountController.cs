using Common.Library.Library.Exceptions;
using Common.Library.Model;
using Common.Library.Utils;
using LearningApp.Common.Model;
using LearningApp.Facade.API.Controllers.Base;
using LearningApp.Facade.API.Model;
using LearningApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LearningApp.Facade.API.Controllers
{
    /// <summary>
    /// Account Controller.
    /// </summary>
    [Route("[controller]")]
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> logger;
        private readonly AppSetting appSetting;

        public AccountController(ILogger<AccountController> logger, AppSetting appSetting)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.appSetting = appSetting ?? throw new ArgumentNullException(nameof(appSetting));
        }

        [HttpPost]
        public IActionResult Token(LoginModel user)
        {
            string token = string.Empty;
            try
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, "Raj"));

                var jwtSetting = new JwtTokenGeneratorModel()
                {
                    Issuer = appSetting.jwtSetting.Issuer,
                    Key = appSetting.jwtSetting.Key,
                    ExpireAfter = 5,
                    Claims = claims
                };
                token = JwtTokenGenerator.GenerateJwtToken(jwtSetting);
                //if (user == null || string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
                //    return new JsonResult { data }
            }
            catch (ApiException ex)
            {
                logger.LogError(ex, "Exception Occured.");
                return UnprocessableEntity(ex.Error);
            }

            return Ok(new { Token = token} );
        }
    }
}
