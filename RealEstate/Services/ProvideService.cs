namespace RealEstate.Services
{
    public static class ProvideService
    {
        public static IServiceProvider? Instance { get; set; }

        public static T? GetService<T>()
        {
            if (Instance == null)
                throw new Exception("Service provider is not set");
            return Instance.GetService<T>() ?? throw new Exception($"Service of type {typeof(T).Name} not found");
        }
    }
}
