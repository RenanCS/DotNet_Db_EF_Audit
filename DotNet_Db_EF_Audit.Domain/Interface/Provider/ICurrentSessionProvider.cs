namespace DotNet_Db_EF_Audit.Domain.Interface.Provider
{
    public interface ICurrentSessionProvider
    {
        Guid? GetUserId();
    }
}
