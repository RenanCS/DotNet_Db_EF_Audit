namespace DotNet_Db_EF_Audit.Domain.Interface.Service
{
    public interface IAuthorizationLocalService
    {
        Task<string> Login(string email, CancellationToken cancellationToken);
    }
}
