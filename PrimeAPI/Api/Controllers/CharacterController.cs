using Microsoft.AspNetCore.Mvc;
using Service.Dto.CharacterDto;
using Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
        }

        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            var result = await _characterService.FindAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindByID(int id)
        {
            var result = await _characterService.FindById(id);
            if(result.Code == 404) return Problem(statusCode:404, title: result.Message);
            if(result.Code == 400) return Problem(statusCode:400, title: result.Message);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CharacterDTO body)
        {
            var result = await _characterService.Create(body);
            if (result.Code == 400) return Problem(statusCode: 400, title: result.Message);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Update(CharacterUpdateDTO body)
        {
            var result = await _characterService.Update(body);
            if(result.Code ==400) return Problem(statusCode:400, title:result.Message);
            if(result.Code ==406) return Problem(statusCode:406, title:result.Message);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult>Delete(int id)
        {
            var result = await _characterService.Delete(id);
            if(result.Code ==400) return Problem(statusCode:400, title: result.Message);
            if(result.Code ==406) return Problem(statusCode:406, title: result.Message);
            return Ok(result);
        }


    }
}
