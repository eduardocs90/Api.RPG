using Service.Dto.UserDto;

namespace Service.Dto.TokenDto
{
    public class ResponseLoginDTO
    {

        public TokenDTO Token { get; set; } 
        public ResponseUserDTO User { get; set; }

    }
}
