using RealEstate.Models;

namespace RealEstate.Services
{
    public interface IApiService
    {
        Task AddBookmarkAsync(int propertyId, int userId);
        Task<Bookmark[]> GetBookmarksAsync();
        Task<Property[]> GetPropertiesAsync(int categoryId);
        Task<Property> GetPropertyAsync(int id);
        Task<Property[]> GetTrendingPropertiesAsync();
        Task<LoginResponse> LoginAsync(Login login);
        Task RegisterUser(Register register);
        Task RemoveBookmarkAsync(int propertyId);
        Task<Property[]> SearchProperties(string query);
    }
}