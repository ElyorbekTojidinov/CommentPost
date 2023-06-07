namespace Application.Common.Interfaces
{
    public interface IHashPassword
    {
        Task<string> GetHashPasswordAsync(string text);
    }
}
