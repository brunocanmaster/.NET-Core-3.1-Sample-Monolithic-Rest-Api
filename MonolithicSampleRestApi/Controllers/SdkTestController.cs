using Microsoft.AspNetCore.Mvc;
using MonolithicSampleRestApi.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonolithicSampleRestApi.Controllers
{
    [Route("/sdk/test")]
    public class SdkTestController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                MonolithicSampleRestApiSdk sdk = new MonolithicSampleRestApiSdk(Settings.MonolithicSampleRestApiSdkSettings.ApiHost, Settings.MonolithicSampleRestApiSdkSettings.BasicToken);

                var interestRate = await sdk.GetInterestRate();

                var interestCalc = await sdk.PostCalcInterest(new Domain.Models.ApiModels.RequestCalcInterest(100, 5));

                var githubVirtualPath = await sdk.GetGitHubAppPath();

                return Ok(new { 
                
                    interestRate,
                    interestCalc,
                    githubVirtualPath

                });
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
