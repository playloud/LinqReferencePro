using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqReferencePro.ViewModels;

namespace LinqReferencePro.Views;

public partial class PSHCollectionPage01 : ContentPage
{

    public PSHCollectionPage01(PSHCollectionModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }

    public async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var model = (e.CurrentSelection.FirstOrDefault() as PeopleModel);
        if (model != null)
        {
            await Navigation.PushAsync(new PSHDetailImagePage(model), true);
        }
        else
        {
            await DisplayAlert("model is null", "", "ok");
        }
    }

    
}