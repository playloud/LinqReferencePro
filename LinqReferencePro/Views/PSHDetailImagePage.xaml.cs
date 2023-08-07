using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqReferencePro.ViewModels;

namespace LinqReferencePro.Views;

public partial class PSHDetailImagePage : ContentPage
{
    public PSHDetailImagePage(PeopleModel peopleModel)
    {
        InitializeComponent();
        BindingContext = peopleModel;
        //DetailImage.Source = peopleModel.ImageUrl;
    }
}