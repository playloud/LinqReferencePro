using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqReferencePro.ViewModels;

namespace LinqReferencePro.Views;

public enum PagePurpose
{
    General, 
    Favorites, 
    Unknown
}

public partial class LinqMainPage : ContentPage
{
    private PagePurpose pp = PagePurpose.Unknown;
    private DocCollectionModel dcm = null;
    private Doc[] allDocs = null;

    public LinqMainPage(DocCollectionModel model, Doc[] allDocs, PagePurpose pp)
    {
        InitializeComponent();
        BindingContext = model;
        this.pp = pp;
        this.dcm = model;
        this.allDocs = allDocs;

        DeviceDisplay.Current.MainDisplayInfoChanged += OnDisplaychanged;
    }

    private void OnDisplaychanged(object sender, DisplayInfoChangedEventArgs e)
    {
        Console.WriteLine("display detected");
        var curWidth = e.DisplayInfo.Width;
        
    }

    public async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var model = (e.CurrentSelection.FirstOrDefault() as Doc);
        if (model != null)
        {
            await Navigation.PushAsync(new PSHHTMLViewerPage(allDocs, model));
            CollectionView cv = (CollectionView)sender;
            cv.SelectedItem = null;
        }
        // else
        // {
        //     await DisplayAlert("model is null", "", "ok");
        // }
    }

    public async void OnRefreshing(object sender, EventArgs e)
    {
        if (pp == PagePurpose.Favorites)
        {
            BindingContext = Util.BookBuilder.GetFavoritePages();
            this.allDocs = Util.BookBuilder.GetFavoritePagesAllDocs();
        }

        refreshView.IsRefreshing = false;
    }

    

    
}