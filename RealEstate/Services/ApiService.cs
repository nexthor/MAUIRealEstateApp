using Microsoft.Extensions.Configuration;
using RealEstate.Extensions;
using RealEstate.Models;
using System.Net.Http.Json;

namespace RealEstate.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _settings;

        public ApiService(IHttpClientFactory factory, IConfiguration configuration)
        {
            _httpClient = factory.CreateClient();
            _settings = configuration.GetSection(nameof(ApiSettings)).Get<ApiSettings>() ?? throw new Exception("No options were found!");

            if (!string.IsNullOrWhiteSpace(_settings.BaseUrl))
                _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        }

        #region User interactions
        public async Task<LoginResponse> LoginAsync(Login login)
        {
            var response = await _httpClient.PostAsJsonAsync("users/login", login);
            return await response.Content.ReadFromJsonAsync<LoginResponse>() ?? throw new Exception("No response from the server");
        }

        public async Task RegisterUser(Register register)
        {
            var response = await _httpClient.PostAsJsonAsync("users/register", register);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to register user");
        }
        #endregion

        #region Property interactions
        public async Task<ICollection<Property>> GetPropertiesAsync(int categoryId)
        {
            var response = await _httpClient.SetToken().GetFromJsonAsync<Property[]>($"Properties/PropertyList?categoryId={categoryId}");
            return response ?? throw new Exception("No response from the server");
        }

        public async Task<Property> GetPropertyAsync(int id)
        {
            _httpClient.SetToken();
            var response = await _httpClient.SetToken().GetFromJsonAsync<Property>($"Properties/PropertyDetail?id={id}");
            return response ?? throw new Exception("No response from the server");
        }

        public async Task<ICollection<Property>> GetTrendingPropertiesAsync()
        {
            var response = await _httpClient.SetToken().GetFromJsonAsync<ICollection<Property>>("Properties/TrendingProperties");
            return response ?? throw new Exception("No response from the server");
        }

        public async Task<ICollection<Property>> SearchProperties(string query)
        {
            var response = await _httpClient.SetToken().GetFromJsonAsync<ICollection<Property>>($"Properties/SearchProperties?address={query}");
            return response ?? throw new Exception("No response from the server");
        }
        #endregion

        #region Bookmark interactions
        public async Task<ICollection<Bookmark>> GetBookmarksAsync()
        {
            var response = await _httpClient.SetToken().GetFromJsonAsync<Bookmark[]>($"bookmarks");
            return response ?? throw new Exception("No response from the server");
        }

        public async Task AddBookmarkAsync(int propertyId, int userId)
        {
            var response = await _httpClient.SetToken().PostAsJsonAsync("bookmarks", new { PropertyId = propertyId, User_Id = userId });
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to add bookmark");
        }

        public async Task RemoveBookmarkAsync(int bookmarkId)
        {
            var response = await _httpClient.SetToken().DeleteAsync($"bookmarks/{bookmarkId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to remove bookmark");
        }
        #endregion

        #region Category interactions
        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            var response = await _httpClient.SetToken().GetFromJsonAsync<ICollection<Category>>("categories");
            return response ?? throw new Exception("No response from the server");
        }
        #endregion
    }
}
