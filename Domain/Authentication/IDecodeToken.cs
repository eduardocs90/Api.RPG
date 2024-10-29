namespace Domain.Authentication
{
    public interface IDecodeToken
    {
        int? Decode(string token);
    }
}
