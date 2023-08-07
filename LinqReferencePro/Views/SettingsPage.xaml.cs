using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqReferencePro.Views;
//[QueryProperty(nameof(Message), nameof(Message))]
public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {

        InitializeComponent();
    }

    public async void OnClickReset(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Reset Favorites", "Are you sure to clean up?", "Yes", "No");
        if (result)
        {
            Util.BookMarkManager.GetInstance().Clean();
        }
        
    }


    // public string Message
    // {
    //     get { return msgLabel?.Text;}
    //     set { msgLabel.Text = value; } 
    // }
}