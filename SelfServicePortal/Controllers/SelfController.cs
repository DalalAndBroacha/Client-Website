using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SelfServicePortal.Controllers
{
    public class SelfController : Controller
    {
        private readonly ILogger<SelfController> _logger;

        public SelfController(ILogger<SelfController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
