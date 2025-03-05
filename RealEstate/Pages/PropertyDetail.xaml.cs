using RealEstate.Models;

namespace RealEstate.Pages;

public partial class PropertyDetail : ContentPage
{
    private string phoneNumber;
    private ICollection<Bookmark> _bookmarks;
    private readonly string _userId;
    private readonly int _propertyId;
	public PropertyDetail(Property property)
	{
		InitializeComponent();
		ImgPropery.Source = property.FullImageUrl;
        LblPrice.Text = $"${property.Price}";
        LblAddress.Text = property.Address;
        LblDescription.Text = property.Detail;
        phoneNumber = property.Phone ?? "50345564647";
        _bookmarks = property.Bookmarks;
        _userId = Preferences.Get(RealEstateConstants.UserId, string.Empty);
        _propertyId = property.Id;

        ImgBookmark.Source = GetBookmarkImage(BookmarkExists());
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

    private bool BookmarkExists()
        => _bookmarks.FirstOrDefault(x => x.User_Id == int.Parse(_userId) && x.PropertyId == _propertyId) != null;

    private string GetBookmarkImage(bool exists)
    {
        var val = exists ? "fill" : "empty";
        var image = $"bookmark_{val}_icon";

        return image;
    }
}