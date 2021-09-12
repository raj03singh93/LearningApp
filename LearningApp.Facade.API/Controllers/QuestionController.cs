using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LearningApp.Facade.API.Controllers
{
    /// <summary>
    /// Question api
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> logger;

        public QuestionController(ILogger<QuestionController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets the list of all questions
        /// </summary>
        /// <returns>List of all question</returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Get the question by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
        ///        "id": 1
        ///     }
        /// </remarks>
        /// <param name="id"> Question Id </param>
        /// <returns>Return the question</returns>
        /// <response code="404">Question not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public string Get(int id)
        {
            return "value";
        }

        
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
