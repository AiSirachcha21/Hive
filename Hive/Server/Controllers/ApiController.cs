using Hive.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Hive.Server.Controllers
{
    public class ApiController : ControllerBase
    {
        private IMediator _mediator;
        private UserManager<ApplicationUser> _userManager;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected UserManager<ApplicationUser> ApplicationUser => _userManager ??= HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();
        protected string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
