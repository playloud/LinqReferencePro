using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core.Views;
using LinqReferencePro.Util;
using LinqReferencePro.ViewModels;

namespace LinqReferencePro.Views;

public partial class PSHHTMLViewerPage : ContentPage
{
    private Doc doc = null;
    BookMarkManager bookMarkManager = null;
    private bool isBookmarked = false;
    private Doc[] allDocs = null;

    public PSHHTMLViewerPage()
    {
        InitializeComponent();
    }

    public PSHHTMLViewerPage(Doc[] allDocs, Doc doc)
    {
        InitializeComponent();
        this.doc = doc;
        this.allDocs = allDocs;
        Load(doc);
        
    }

    public void Load(Doc doc)
    {
        this.webView.Source = doc.HTMLPath;
        bookMarkManager = BookMarkManager.GetInstance();
        CheckBookmark(doc.Id);
        BindingContext = doc;
        
        if(doc != null) 
            this.Title = $"{doc.Theme} : {doc.Title}";
    }

    public void CheckBookmark(string docId)
    {
        if (bookMarkManager.IsBookmark(this.doc.Id))
        {
            imgBtn_fav.Source = "heart_1.svg";
            isBookmarked = true;
        }
        else
        {
            imgBtn_fav.Source = "heart_0.svg";
            isBookmarked = false;
        }
    }

    public void OnFavButtonClicked(object sender, EventArgs e)
    {
        if (isBookmarked)
        {
            imgBtn_fav.Source = "heart_0.svg";
            bookMarkManager.RemoveBookMark(this.doc.Id);
        }
        else
        {
            imgBtn_fav.Source = "heart_1.svg";
            bookMarkManager.SetBookMark(this.doc.Id);
        }
        isBookmarked = !isBookmarked;
    }

    public void OnBackPressedCallback(object sender, EventArgs e)
    {
        Console.WriteLine("on back button call back event");

    }


    private void But_next_OnClicked(object sender, EventArgs e)
    {
        Doc nextDoc = null;

        if (this.allDocs != null)
        {
            //int currentIndex = Array.IndexOf(allDocs, doc);
            int currentIndex = -1;
            for (int i = 0; i < allDocs.Length; i++)
            {
                if (allDocs[i].Id == doc.Id)
                {
                    currentIndex = i;
                }
            }
            if (currentIndex >= 0 && currentIndex < allDocs.Length -1)
                nextDoc = allDocs[currentIndex+1];
        }

        if (nextDoc != null)
        {
            this.doc = nextDoc;
            Load(nextDoc);
        }
        
    }

    private void But_prev_OnClicked(object sender, EventArgs e)
    {
        Doc prevDoc = null;

        if (this.allDocs != null)
        {
            //int currentIndex = Array.IndexOf(allDocs, doc);
            int currentIndex = -1;
            for (int i = 0; i < allDocs.Length; i++)
            {
                if (allDocs[i].Id == doc.Id)
                {
                    currentIndex = i;
                }
            }

            if (currentIndex > 0)
                prevDoc = allDocs[currentIndex -1];
        }

        if (prevDoc != null)
        {
            this.doc = prevDoc;
            Load(prevDoc);
        }
        
    }
}