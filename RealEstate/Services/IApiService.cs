using RealEstate.Models;

namespace RealEstate.Services
{
    public interface IApiService
    {
        Task AddBookmarkAsync(int propertyId, int userId);
        Task<ICollection<Bookmark>> GetBookmarksAsync();
        Task<ICollection<Property>> GetPropertiesAsync(int categoryId);
        Task<Property> GetPropertyAsync(int id);
        Task<ICollection<Property>> GetTrendingPropertiesAsync();
        Task<LoginResponse> LoginAsync(Login login);
        Task RegisterUser(Register register);
        Task RemoveBookmarkAsync(int bookmarkId);
        Task<ICollection<Property>> SearchPropertiesAsync(string query);
        Task<ICollection<Category>> GetCategoriesAsync();
    }
}