namespace MyProject.Infrastructure.Interfaces;

public interface IAuthService
{
    public Task Register(string username, string password);
    public Task<string> Login(string username, string password);
}