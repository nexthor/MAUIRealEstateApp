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
            var response = await _httpClient.PostAsJsonAsync("api/users/login", login);
            return await response.Content.ReadFromJsonAsync<LoginResponse>() ?? throw new Exception("No response from the server");
        }

        public async Task RegisterUser(Register register)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users/register", register);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to register user");
        }
        #endregion

        #region Property interactions
        public async Task<ICollection<Property>> GetPropertiesAsync(int categoryId)
        {
            var response = await _httpClient.SetToken().GetFromJsonAsync<Property[]>($"api/Properties/PropertyList?categoryId={categoryId}");
            return response ?? throw new Exception("No response from the server");
        }

        public async Task<Property> GetPropertyAsync(int id)
        {
            _httpClient.SetToken();
            var response = await _httpClient.SetToken().GetFromJsonAsync<Property>($"api/Properties/PropertyDetail?id={id}");
            return response ?? throw new Exception("No response from the server");
        }

        public async Task<ICollection<Property>> GetTrendingPropertiesAsync()
        {
            var response = await _httpClient.SetToken().GetFromJsonAsync<ICollection<Property>>("api/Properties/TrendingProperties");
            var properties = response ?? throw new Exception("No response from the server");
            return properties.Select(x => new Property
            {
                Id = x.Id,
                Name = x.Name,
                Detail = x.Detail,
                Address = x.Address,
                Price = x.Price,
                IsTrending = x.IsTrending,
                UserId = x.UserId,
                ImageUrl = x.ImageUrl,
                FullImageUrl = $"{_settings.BaseUrl}{x.ImageUrl}",
                CategoryId = x.CategoryId,
                Bookmarks = x.Bookmarks
            }).ToList(); ;
        }

        public async Task<ICollection<Property>> SearchProperties(string query)
        {
            var response = await _httpClient.SetToken().GetFromJsonAsync<ICollection<Property>>($"api/Properties/SearchProperties?address={query}");
            return response ?? throw new Exception("No response from the server");
        }
        #endregion

        #region Bookmark interactions
        public async Task<ICollection<Bookmark>> GetBookmarksAsync()
        {
            var response = await _httpClient.SetToken().GetFromJsonAsync<Bookmark[]>("api/bookmarks");
            return response ?? throw new Exception("No response from the server");
        }

        public async Task AddBookmarkAsync(int propertyId, int userId)
        {
            var response = await _httpClient.SetToken().PostAsJsonAsync("api/bookmarks", new { PropertyId = propertyId, User_Id = userId });
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to add bookmark");
        }

        public async Task RemoveBookmarkAsync(int bookmarkId)
        {
            var response = await _httpClient.SetToken().DeleteAsync($"api/bookmarks/{bookmarkId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to remove bookmark");
        }
        #endregion

        #region Category interactions
        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            var response = await _httpClient.SetToken().GetFromJsonAsync<ICollection<Category>>("api/categories");
            var categories = response ?? throw new Exception("No response from the server");
            return categories.Select(x => new Category
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    FullImageUrl = $"{_settings.BaseUrl}{x.ImageUrl}",
                    Properties = x.Properties
                }).ToList();
        }
        #endregion
    }
}
