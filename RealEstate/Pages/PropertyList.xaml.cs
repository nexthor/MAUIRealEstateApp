using RealEstate.Models;
using RealEstate.Services;

namespace RealEstate.Pages;

public partial class PropertyList : ContentPage
{
	private readonly Category _category;
	public PropertyList(Category category)
	{
		InitializeComponent();
        _category = category;
        Title = category.Name;
    }

    override protected async void OnAppearing()
    {
        base.OnAppearing();
        var apiService = ProvideService.GetService<IApiService>() ?? throw new Exception("service cannot be found");
        var properties = await apiService.GetPropertiesAsync(_category.Id);
        cvProperties.ItemsSource = properties;
    }
}