using Microsoft.AspNetCore.Mvc;
using MonolithicSampleRestApi.FilterAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonolithicSampleRestApi.Controllers
{
    [ApiController]
    [ApiAuthorization]
    public class ApiBaseController : ControllerBase
    {
    }
}
