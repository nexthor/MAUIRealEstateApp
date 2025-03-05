using RealEstate.Models;

namespace RealEstate.Pages;

public partial class PropertyDetail : ContentPage
{
    private string phoneNumber;
	public PropertyDetail(Property property)
	{
		InitializeComponent();
		ImgPropery.Source = property.FullImageUrl;
        LblPrice.Text = $"${property.Price}";
        LblAddress.Text = property.Address;
        LblDescription.Text = property.Detail;
        phoneNumber = property.Phone ?? "50345564647";
    }

    private void ImgBack_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void ImgBookmark_Clicked(object sender, EventArgs e)
    {

    }

    private void TapMessage_Tapped(object sender, TappedEventArgs e)
    {
        Sms.ComposeAsync(new SmsMessage("Hi! I'm interested in your property", phoneNumber));
    }

    private void TapCall_Tapped(object sender, TappedEventArgs e)
    {
        PhoneDialer.Open(phoneNumber);
    }
}