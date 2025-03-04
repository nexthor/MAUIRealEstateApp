using RealEstate.Models;
using RealEstate.Services;

namespace RealEstate.Pages;

public partial class HomePage : ContentPage
{
	private readonly IApiService _apiService;
    public HomePage()
	{
		InitializeComponent();
		var userName = Preferences.Get(RealEstateConstants.UserName, string.Empty);
		lblUsername.Text = $"Welcome, {userName}";
        _apiService = ProvideService.GetService<IApiService>() ?? throw new Exception("service cannot be found");
    }

    override protected async void OnAppearing()
    {
        base.OnAppearing();
        var categories = await _apiService.GetCategoriesAsync();
        CvCategories.ItemsSource = categories;

        var trending = await _apiService.GetTrendingPropertiesAsync();
        CvTopics.ItemsSource = trending;
    }

    private void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var category = e.CurrentSelection.FirstOrDefault() as Category;
        if (category == null)
            return;

        Navigation.PushAsync(new PropertyList(category));
        ((CollectionView)sender).SelectedItem = null;
    }
}