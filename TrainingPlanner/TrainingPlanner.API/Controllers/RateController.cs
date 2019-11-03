using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }
    }
}
