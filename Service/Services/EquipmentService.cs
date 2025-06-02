using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Service.Dto.EquipmentDto;
using Service.Helper;
using Service.Helper.Utils;
using Service.Interfaces;

namespace Service.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMapper _mapper;
      
        public EquipmentService(IEquipmentRepository equipmentRepository, IMapper mapper)
        {
         
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _equipmentRepository = equipmentRepository ?? throw new ArgumentNullException(nameof(equipmentRepository));
        }
        public async Task<ExceptionManager<ICollection<EquipmentDTO>>> FindAll()
        {
            var result = await _equipmentRepository.FindAll();
            return ExceptionManager.Ok(_mapper.Map<ICollection<EquipmentDTO>>( result));
        }

        public async Task<ExceptionManager<EquipmentDTO>> FindByID(int id)
        {
            if (id <= 0) return ExceptionManager.BadRequest<EquipmentDTO>("Id inválido");
            var result = await _equipmentRepository.FindById(id);
            if (result == null) return ExceptionManager.NotFound<EquipmentDTO>("Equipmento não existe");
            return ExceptionManager.Ok(_mapper.Map<EquipmentDTO>(result));
        }

        public async Task<ExceptionManager<EquipmentDTO>> Create(EquipmentDTO body)
        {
            var fields = EquipmentUtils.ValidationEquipmentCreate(body);
            if (fields.Validation == false) return ExceptionManager.BadRequest<EquipmentDTO>(fields.Message);
            var result = await _equipmentRepository.Create(_mapper.Map<Equipment>(body));
            return ExceptionManager.Created(_mapper.Map<EquipmentDTO>(result));
        }


        public async Task<ExceptionManager<EquipmentDTO>> Update(EquipmentUpdateDTO body)
        {
            var user = await _equipmentRepository.FindById(body.Id);
            if (user == null) return ExceptionManager.NotFound<EquipmentDTO>("Não foi possível encontrar usuário");
            user = _mapper.Map<EquipmentUpdateDTO, Equipment>(body,user);
            var result = await _equipmentRepository.Update(user);
            return ExceptionManager.Ok(_mapper.Map<EquipmentDTO>(result));
        }
        public async Task<ExceptionManager<bool>> Delete(int id)
        {
            var user = await _equipmentRepository.FindById(id);
            if (user != null) return ExceptionManager.NotFound<bool>("usuário não encontrado");
            var result =  await _equipmentRepository.Delete(_mapper.Map<Equipment>( user));
            return ExceptionManager.Ok(result);

        }

    }
}
