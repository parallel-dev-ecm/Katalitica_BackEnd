namespace Identity
{
    public interface IAuthService
    {
        Task<UserInformation> Login(string username, string password);
        Task<IEnumerable<ModulesHierachy>> GetModulesHierachy(string username);
        Task<bool> Logout();
    }
}
