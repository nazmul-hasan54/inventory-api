namespace InventoryApi.Interface
{
    public interface IAuthService
    {
        string GenerateJwt(string username, string role);
    }
}
