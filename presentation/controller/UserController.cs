
using hitsLab.domain.services;
using hitsLab.presentation.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hitsLab.presentation.controller
{
    [Route("/api/account")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet, Route("profile")]
        public ActionResult GetUserProfile() 
        {
            return Ok(_userService.GetUserProfile());
        }  

        [ValidateModel]
        [Authorize]
        [HttpPut, Route("profile")]
        public async Task<ActionResult> EditUserProfile([FromBody]UserEditDto requestDto)
        {
            await _userService.EditUserProfile(requestDto);
            return Ok();
        }
    }
}