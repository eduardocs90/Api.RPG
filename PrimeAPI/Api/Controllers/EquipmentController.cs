using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Service.Dto.EquipmentDto;
using Service.Interfaces;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {

        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        /// <summary>
        /// Retorna uma lista com todos os Equipamentos cadastrados.
        /// </summary>
        /// <response code="200">Retorna uma lista com todos os equipamentos cadastrados.</response>
        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            var result = await _equipmentService.FindAll();
            return Ok(result);

        }

        /// <summary>
        /// Retorna um usuário específico com base no Id.
        /// </summary>]
        /// <param name="id">O identificador único do usuário.</param>
        /// <response code="200">Retorna o usuário.</response>
        /// <response code="404">Retorna usúario não encontrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id)
        {
            var result = await _equipmentService.FindByID(id);
            if (result.Code == 404) return Problem(statusCode: 404, title: result.Message);
            return Ok(result);
        }

        /// <summary>
        /// Registra um equipamento no Sistema.
        /// </summary>
        /// <param name="body">Informações do do equipamento que serão cadastradas.</param>
        /// <response code="201">Registra um novo equipamento no sistema.</response>
        /// <response code="400">Retorna que os dados de cadastro estão incompletos.</response>
        [HttpPost]
        public async Task<ActionResult> Create(EquipmentDTO body)
        {
            var result = await _equipmentService.Create(body);
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
        [HttpPut]
        public async Task<ActionResult> Update(EquipmentUpdateDTO body)
        {
            var result = await _equipmentService.Update(body);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _equipmentService.Delete(id);
            return Ok(result);
        }
    }
}
