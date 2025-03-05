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
        CvProperties.ItemsSource = properties;
    }

    private void CvProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var property = e.CurrentSelection.FirstOrDefault() as Property;
        if (property == null)
            return;

        Navigation.PushAsync(new PropertyDetail(property));
        ((CollectionView)sender).SelectedItem = null;
    }
}