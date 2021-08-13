using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonolithicSampleRestApi.Controllers.V1
{
    [ApiController]
    [Route("/github/v1")]
    public class GitHubController : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<string>> GetGitHubAppPath()
        {
            return Ok("https://www.google.com.br");
        }
    }
}
