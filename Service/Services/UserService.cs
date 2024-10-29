using AutoMapper;
using Domain.Authentication;
using Domain.Entities;
using Domain.Repositories;
using Service.Dto.TokenDto;
using Service.Dto.UserDto;
using Service.Helper;
using Service.Helper.Utils;
using Service.Interfaces;

namespace Service.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IDecodeToken _decodeToken;

        public UserService(IUserRepository userRepository, IMapper mapper, ITokenGenerator tokenGenerator, IDecodeToken decodeToken)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
            _decodeToken = decodeToken ?? throw new ArgumentNullException(nameof(decodeToken));
        }

        public async Task<ExceptionManager<ICollection<ResponseUserDTO>>> FindAll()
        {
            var result = await _userRepository.FindAll();
            return ExceptionManager.Ok(_mapper.Map<ICollection<ResponseUserDTO>>(result));

        }


        public async Task<ExceptionManager<ResponseUserDTO>> FindByID(int id)
        {
            if (UserUtils.VerifyIdUser(id)) return ExceptionManager.NotAcceptable<ResponseUserDTO>("Id deve ser maior que 0");

            var user = await _userRepository.FindById(id);

            if (user == null) return ExceptionManager.NotFound<ResponseUserDTO>("usuário não encontrado");

            return ExceptionManager.Ok(_mapper.Map<ResponseUserDTO>(user));
        }

        public async Task<ExceptionManager<ResponseUserDTO>> Create(UserDTO body)
        {
            if (!UserUtils.IsValidUserDTO(body)) return ExceptionManager.NotAcceptable<ResponseUserDTO>("campos obrigatórios não preenchidos");
            if (!UserUtils.VerifyDocument(body.Document)) return ExceptionManager.BadRequest<ResponseUserDTO>("cpf inválido");
            if (!UserUtils.VerifyEmail(body.Email) || !UserUtils.VerifyPassword(body.Password))
            {
                return
                ExceptionManager.BadRequest<ResponseUserDTO>("email e senha denvem ter formato válido");
            }
            var result = await _userRepository.Create(_mapper.Map<User>(body));
            return ExceptionManager.Created(_mapper.Map<ResponseUserDTO>(result));
        }

        public async Task<ExceptionManager<ResponseUserDTO>> Update(UserUpdateDTO body)
        {
            if (!UserUtils.IsValidUserUpdateDTO(body)) return ExceptionManager.BadRequest<ResponseUserDTO>("há campos obrigatórios não preenchidos");
            if (!UserUtils.VerifyIdUser(body.Id)) return ExceptionManager.NotAcceptable<ResponseUserDTO>("id inválido!");
            if (!UserUtils.VerifyDocument(body.Document)) return ExceptionManager.NotAcceptable<ResponseUserDTO>("CPF inválido!");
            var user = await _userRepository.FindByIdForUpdate(body.Id);
            if (user == null) return ExceptionManager.NotFound<ResponseUserDTO>("usuário não encontrado!");
            user = _mapper.Map<UserUpdateDTO, User>(body, user);
            var result = await _userRepository.Update(user);
            return ExceptionManager.Ok(_mapper.Map<ResponseUserDTO>(result));
        }

        public async Task<ExceptionManager<bool>> Delete(int id)
        {
            if (UserUtils.VerifyIdUser(id)) return ExceptionManager.BadRequest<bool>("Id inválido!");
            var user = await _userRepository.FindById(id);
            if (user == null) return ExceptionManager.NotFound<bool>("não foi possivél encontrar o usuário");
            var result = await _userRepository.Delete(_mapper.Map<User>(user));
            return ExceptionManager.Ok(result);
        }

        public async Task<ExceptionManager<ResponseLoginDTO>> GenerateToken(LoginDTO loginDTO)
        {
            if (loginDTO == null) return ExceptionManager.BadRequest<ResponseLoginDTO>("Os campos devem ser preenchidos");
            if (!UserUtils.VerifyEmail(loginDTO.Email) || !UserUtils.VerifyPassword(loginDTO.Password))
                return ExceptionManager.BadRequest<ResponseLoginDTO>("Email ou Senha incorretos");
            var user = await _userRepository.Login(loginDTO.Email, loginDTO.Password);
            if (user == null) return ExceptionManager.NotFound<ResponseLoginDTO>("Não exite usuário com este email e/ou senha");
            var responseUserDTO = _mapper.Map<ResponseUserDTO>(user);

            var tokenData = _tokenGenerator.Generator(user);
            if (tokenData == null)
                return ExceptionManager.NotAcceptable<ResponseLoginDTO>("Erro ao gerar token");

            var accessToken = (string)tokenData.GetType().GetProperty("acess_token").GetValue(tokenData, null);

            var response = new ResponseLoginDTO
            {
                Token = new TokenDTO { AccessToken = accessToken },
                User = responseUserDTO
            };
            return ExceptionManager.Ok(response);
        }
    }
}
