using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Service.Dto.CharacterDto;
using Service.Helper;
using Service.Helper.Utils;
using Service.Interfaces;

namespace Service.Services
{
    public class CharacterService : ICharacterService
    {

        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public CharacterService(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        public async Task<ExceptionManager<ICollection<CharacterDTO>>> FindAll()
        {
            var result = await _characterRepository.FindAll();
            return ExceptionManager.Ok(_mapper.Map<ICollection<CharacterDTO>>(result));
        }

        public async Task<ExceptionManager<CharacterDTO>> FindById(int id)
        {
           
            if (CharacterUtils.VerifyIdCharacter(id)) return ExceptionManager.BadRequest<CharacterDTO>("id inválido!");
                var result = await _characterRepository.FindById(id);
            if (result == null) return ExceptionManager.NotFound<CharacterDTO>("personagem não encontrado");
            return ExceptionManager.Ok(_mapper.Map<CharacterDTO>(result));
        }

        public async Task<ExceptionManager<CharacterDTO>> Update(CharacterUpdateDTO body)
        {
            if (!CharacterUtils.IsValidCharacterUpdateDTO(body)) return ExceptionManager.BadRequest<CharacterDTO>("preenchas os campos obrigatórios");
            if (CharacterUtils.VerifyIdCharacter(body.Id)) return ExceptionManager.NotAcceptable<CharacterDTO>("id inválido");
            if (!CharacterUtils.VerifyLuckCharacter((int)body.Luck)) return ExceptionManager.NotAcceptable<CharacterDTO>("Sorte é um numero de 1 a 6");
            if (!CharacterUtils.VerifyStrengthCharacter((int)body.Strength)) return ExceptionManager.NotAcceptable<CharacterDTO>("Força é um numero de 1 a 30");
            if (!CharacterUtils.VerifyDexterityCharacter((int)body.Dexterity)) return ExceptionManager.NotAcceptable<CharacterDTO>("Destreza é um numero de 1 a 20");
            if (!CharacterUtils.VerifyAgilityCharacter((int)body.Agility)) return ExceptionManager.NotAcceptable<CharacterDTO>("Agilidade é um numero de 1 a 20");
            var character = await _characterRepository.FindByIdForUpdate(body.Id);
            if (character == null) return ExceptionManager.NotFound<CharacterDTO>("personagem não encontardo");
            character = _mapper.Map<CharacterUpdateDTO, Character>(body, character);
            var result = await _characterRepository.Update(character);
            return ExceptionManager.Ok(_mapper.Map<CharacterDTO>(result));
        }

        public async Task<ExceptionManager<CharacterDTO>> Create(CharacterDTO body)
        {
            if (!CharacterUtils.VerifyLuckCharacter((int)body.Luck)) return ExceptionManager.NotAcceptable<CharacterDTO>("Sorte é um numero de 1 a 6");
            if (!CharacterUtils.VerifyStrengthCharacter((int)body.Strength)) return ExceptionManager.NotAcceptable<CharacterDTO>("Força é um numero de 1 a 30");
            if (!CharacterUtils.VerifyDexterityCharacter((int)body.Dexterity)) return ExceptionManager.NotAcceptable<CharacterDTO>("Destreza é um numero de 1 a 20");
            if (!CharacterUtils.VerifyAgilityCharacter((int)body.Agility)) return ExceptionManager.NotAcceptable<CharacterDTO>("Agilidade é um numero de 1 a 20");
            var result = await _characterRepository.Create(_mapper.Map<Character>(body));
            return ExceptionManager.Created(_mapper.Map<CharacterDTO>(result));
        }

        public async Task<ExceptionManager<bool>> Delete(int id)
        {
            if (CharacterUtils.VerifyIdCharacter(id)) return ExceptionManager.NotAcceptable<bool>("id inválido");
            var user = await _characterRepository.FindById(id);
            if (user == null) return ExceptionManager.NotFound<bool>("personagem não encontrado");
            var result = await _characterRepository.Delete(_mapper.Map<Character>(user));
            return ExceptionManager.Ok(result);
        }

      
    }
}
