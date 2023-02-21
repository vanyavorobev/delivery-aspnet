using hitsLab.domain.services;
using hitsLab.presentation.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hitsLab.presentation.controller
{
    [Route("/api/account")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [ValidateModel]
        [HttpPost, Route("register")]
        public async Task<ActionResult> RegisterUser([FromBody]UserRegisterDto requestDto)
        {
            return Ok(await _authService.RegisterUser(requestDto));
        }

        [ValidateModel]
        [HttpPost, Route("login")] 
        public async Task<ActionResult> LoginUser([FromBody]LoginCredentials requestDto) 
        {
            return Ok(await _authService.LoginUser(requestDto));
        }
        
        [Authorize]
        [HttpPost, Route("logout")] 
        public async Task<ActionResult> LogoutUser() 
        {
            await _authService.LogoutUser(HttpContext.Request.Headers.Authorization.ToString());
            return Ok();
        }
    }    
}
