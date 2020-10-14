using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.App.DtoModels;
using ToDo.App.Services.Exceptions;
using ToDo.App.Services.Interfaces;

namespace ToDo.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserModel> Authenticate([FromBody]LoginModel request)
        {
            try
            {
                UserModel response = _userService.Authenticate(request.Username, request.Password);
                return Ok(response);
            }
            catch (UserException ex)
            {

                return BadRequest(ex.Message);
            }

            catch (Exception ex )
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RegisterModel request)
        {
            try
            {
                _userService.Register(request);
                return Ok("Success");
            }
            catch (UserException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
