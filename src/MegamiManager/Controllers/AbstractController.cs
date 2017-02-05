using MegamiManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Controllers
{
    public abstract class AbstractController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public AbstractController(
            UserManager<ApplicationUser> userManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<ManageController>();
        }

        protected Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
