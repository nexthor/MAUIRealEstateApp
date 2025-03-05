using RealEstate.Models;
using RealEstate.Services;

namespace RealEstate.Pages;

public partial class SearchPage : ContentPage
{
    private IApiService _apiService;
	public SearchPage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
	}

    private void ImgBack_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }

    private async void SbProperty_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue == null)
            return;

        var search = await _apiService.SearchPropertiesAsync(e.NewTextValue);
        if (search != null)
            CvSearch.ItemsSource = search;
    }

    private void CvSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var property = e.CurrentSelection.FirstOrDefault() as Property;
        if (property == null) return;

        Navigation.PushAsync(new PropertyDetail(property));
    }
}