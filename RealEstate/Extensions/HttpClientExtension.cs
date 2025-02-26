using System.Net.Http.Headers;

namespace RealEstate.Extensions
{
    public static class HttpClientExtension
    {
        public static HttpClient SetToken(this HttpClient httpClient)
        {
            var token = Preferences.Get(RealEstateConstants.AccessToken, string.Empty);
            if (!string.IsNullOrWhiteSpace(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return httpClient;
        }
    }
}
