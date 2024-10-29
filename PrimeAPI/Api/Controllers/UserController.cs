using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dto.UserDto;
using Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            var result = await _userService.FindAll();
            return Ok(result);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> FindByID(int id)
        {
            var result = await _userService.FindByID(id);
            if(result.Code == 406) return Problem(statusCode: 406, title: result.Message);
            if(result.Code ==404) return Problem(statusCode:404, title: result.Message);
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult> Create(UserDTO body)
        {
            var result = await _userService.Create(body);
            if (result.Code == 400) return Problem(statusCode: 400, title: result.Message);
            if (result.Code ==406) return Problem(statusCode:406, title: result.Message);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]UserUpdateDTO body)
        {
            var result = await _userService.Update(body);
            if( result.Code ==404) return Problem(statusCode:404, title: result.Message);
            if( result.Code ==406) return Problem(statusCode:406, title: result.Message);
            if( result.Code ==400) return Problem(statusCode:400, title: result.Message);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO login)
        {
            var result = await _userService.GenerateToken(login);
            if (result.Code == 200) return Ok(result.Data);
            if (result.Code == 400) return Problem(statusCode: 400, title: result.Message);
            if (result.Code == 404) return Problem(statusCode: 404, title: result.Message);
            return BadRequest(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            if(result.Code ==404) return Problem(statusCode: 404, title:result.Message);
            if(result.Code ==406) return Problem(statusCode: 406, title:result.Message);
            return Ok(result);
        }
    }
}
