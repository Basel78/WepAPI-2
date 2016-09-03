using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BackendService.Model;
using MobileWebAPI.Models;
using MobileWebAPI.Common;
using System.Security.Claims;
using MobileWebAPI.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Infrastructure;

namespace MobileWebAPI.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly ILoginProvider _loginProvider;


        public AccountController(ILoginProvider Repository)
        { 
            _loginProvider = Repository;
        }

        [HttpPost, Route("Token")]
        public IHttpActionResult Token(LoginViewModel login)
        {

            if (!ModelState.IsValid)
            {
                return this.BadRequestError(ModelState);
            }
            
            ClaimsIdentity identity;

            if (!_loginProvider.ValidateCredentials(login.UserName, login.Password, out identity))
            {
                //Log.Debug("Leaving Token(): Incorrect user or password");
                return BadRequest("Incorrect user or password");
            }

            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));

            return Ok(new LoginAccessViewModel
            {
                UserName = login.UserName,
                AccessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket)
            });
        }

        [Authorize]
        [HttpGet, Route("Profile")]
        public IHttpActionResult Profile()
        {
            //Log.DebugFormat("Entering Profile(): User={0}", User.Identity.Name);
            return Ok(new AccountProfileViewModel
            {
                UserName = User.Identity.Name
            });
        }

        /// <summary>
        /// Use this action to test authentication
        /// </summary>
        /// <returns>status code 200 if the user is authenticated, otherwise status code 401</returns>
        [Authorize]
        [HttpGet, Route("Ping")]
        public IHttpActionResult Ping()
        {
            //Log.DebugFormat("Entering Ping(): User={0}", User.Identity.Name);
            return Ok();
        }

    }
}
