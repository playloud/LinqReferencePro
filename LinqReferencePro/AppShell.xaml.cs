using System.Windows.Input;
using LinqReferencePro.ViewModels;
using LinqReferencePro.Views;

namespace LinqReferencePro;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("Settings", typeof(Views.SettingsPage));
        NavigateToSettingsCommand = new Command(async () =>
        {
            //await DisplayAlert("menu item", "settings selected", "ok");

            //await GoToAsync("///Settings");  // when Routing is not here
            FlyoutIsPresented = this.FlyoutBehavior != FlyoutBehavior.Flyout;
            await GoToAsync("Settings", new Dictionary<string, object>()
            {
                {"Message", "the power of navigation"},
                {"Key","my value for settings"}
            }); // when routing registered
        });

        BindingContext = this;

        var viewModel = Util.BookBuilder.GetBasicDocs();
        var page1 = new LinqMainPage(Util.BookBuilder.GetBasicDocs(), Util.BookBuilder.GetAllBasicDocs(), PagePurpose.General);
        var page2 = new LinqMainPage(Util.BookBuilder.GetAdvanceddDocs(), Util.BookBuilder.GetAllAdvancedDocs(), PagePurpose.General);
        linqBasicPage.Content = page1;
        linqAdvancedPage.Content = page2;

        // favorite pages
        DocCollectionModel favs = Util.BookBuilder.GetFavoritePages();
        Doc[] allFavDocArray = Util.BookBuilder.GetFavoritePagesAllDocs();
        var favPageSrc = new LinqMainPage(favs, allFavDocArray, PagePurpose.Favorites);
        linqFavPage.Content = favPageSrc;
    }

    public ICommand NavigateToSettingsCommand { get; set; }

    public async void Test()
    {
        await GoToAsync("Settings", new Dictionary<string, object>()
        {
            {"Message", "the power of navigation"},
            {"Key","my value for settings"}
        }); // when routing registered
    }
}
