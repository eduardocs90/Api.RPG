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

        /// <summary>
        /// Retorna uma lista com todos os usuários cadastrados.
        /// </summary>
        /// <response code="200">Retorna uma lista com todos os usuários cadastrados.</response>
        [HttpGet("find-all")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> FindAll()
        {
            var result = await _userService.FindAll();
            return Ok(result);
        }

        /// <summary>
        /// Retorna um usuário específico com base no Id.
        /// </summary>]
        /// <param name="id">O identificador único do usuário.</param>
        /// <response code="200">Retorna o usuário.</response>
        /// <response code="404">Retorna usúario não encontrado.</response>
        [Authorize]
        [HttpGet("find-Id/{id}")]
        public async Task<ActionResult> FindByID(int id)
        {
            var result = await _userService.FindByID(id);
            if(result.Code ==404) return Problem(statusCode:404, title: result.Message);
            return Ok(result);
        }

        /// <summary>
        /// Registra um usuário no Sistema.
        /// </summary>
        /// <param name="body">Informações do usuário que serão cadastradas.</param>
        /// <response code="201">Registra um novo usuário no sistema.</response>
        /// <response code="400">Retorna que os dados de cadastro estão inconpletos.</response>
        [HttpPost("create")]
        public async Task<ActionResult> Create(UserDTO body)
        {
            var result = await _userService.Create(body);
            if (result.Code == 400) return Problem(statusCode: 400, title: result.Message);
            return Ok(result);
        }

        /// <summary>
        /// Edita as informações de um usuário.
        /// </summary>
        /// <param name="body">Informações do usuário que podem ser editadas.</param>
        /// <response code="200">Retorna o usuário editado.</response>
        /// <response code="400">Retorna que há valores inválidos na requisição.</response>
        /// <response code="404">Retorna que o usuário não foi encontrado.</response>
        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody]UserUpdateDTO body)
        {
            var result = await _userService.Update(body);
            if( result.Code ==404) return Problem(statusCode:404, title: result.Message);
            if( result.Code ==400) return Problem(statusCode:400, title: result.Message);
            return Ok(result);
        }

        /// <summary>
        /// Loga o usuário no sistema.
        /// </summary>
        /// <param name="login">Email e senha do usuário.</param>
        /// <response code="200">Retorna que o login foi realizado com sucesso.</response>
        /// <response code="400">Retorna que os campos de login devem ser preenchidos.</response>
        /// <response code="401">Retorna que as credenciais não tem acesso ao sistema.</response>
        /// <response code="404">Retorna que o usuário não existe no sistema.</response>
        /// <response code="500">Retorna erro interno ao gerar o token de acesso.</response>
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO login)
        {
            var result = await _userService.GenerateToken(login);
            if (result.Code == 200) return Ok(result.Data);
            if (result.Code == 400) return Problem(statusCode: 400, title: result.Message);
            if (result.Code == 404) return Problem(statusCode: 404, title: result.Message);
            if (result.Code == 401) return Problem(statusCode: 401, title: result.Message);
            if (result.Code == 500) return Problem(statusCode: 500, title: result.Message);
            return BadRequest(result);
        }
        
        /// <summary>
        /// Deleta um usuário com base no Id.
        /// </summary>
        /// <param name="id">O identificador único do usuário.</param>
        /// <response code="200">Retorna que o usuário foi deletado com sucesso.</response>
        /// <response code="400">Retorna que Id deve ser um numero positivo.</response>
        /// <response code="404">Retorna que o usuário não foi encontrado.</response>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            if(result.Code ==404) return Problem(statusCode: 404, title:result.Message);
            if(result.Code ==400) return Problem(statusCode: 400, title:result.Message);
            return Ok(result);
        }
    }
}
