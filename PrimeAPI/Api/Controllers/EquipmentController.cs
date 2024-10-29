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

        [HttpGet]

        public async Task<ActionResult> FindAll()
        {
            var result = await _equipmentService.FindAll();
            return Ok(result);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id)
        {
            var result = await _equipmentService.FindByID(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> Create(EquipmentDTO equipment)
        {
            var result = await _equipmentService.Create(equipment);
            return Ok(result);
        }
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
