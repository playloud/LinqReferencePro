using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using LinqReferencePro.ViewModels;

namespace LinqReferencePro.Util;

public class BookBuilder
{
    private const string Cate_Projecting = "Projecting";
    private const string Cate_Filtering = "Filtering";
    private const string Cate_Advanced = "Advanced";

    private static AllDocs allDocs = null;

    public async static Task<AllDocs> LoadDoc()
    {
        var result = new List<Doc>();
        using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("LinqDocs.json");
        using StreamReader reader = new StreamReader(fileStream);
        {
            string src = reader.ReadToEnd();
            AllDocs allDocs = JsonSerializer.Deserialize<AllDocs>(src);
            return allDocs;
        }
        return null;
    }

    public static DocCollectionModel GetBasicDocs()
    {
        var result = new DocCollectionModel();
        if (allDocs == null)
            allDocs = LoadDoc().Result;
        
        var queryCategory = allDocs.Docs.Where(a=>a.Theme=="Basic").Select(a => a.Category).Distinct();
        foreach (string category in queryCategory)
        {
            var queryList = allDocs.Docs.Where(a => a.Category == category);
            var dg = new DocGroup(category, queryList.ToList());
            result.Docs.Add(dg);
        }

        return result;
    }

    public static Doc[] GetAllBasicDocs()
    {
        if (allDocs == null)
            allDocs = LoadDoc().Result;
        var queryBasics = allDocs.Docs.Where(a => a.Theme == "Basic");
        return queryBasics.ToArray();
    }

    public static DocCollectionModel GetAdvanceddDocs()
    {
        var result = new DocCollectionModel();
        if (allDocs == null)
            allDocs = LoadDoc().Result;

        var queryCategory = allDocs.Docs.Where(a => a.Theme == "Advanced").Select(a => a.Category).Distinct();
        foreach (string category in queryCategory)
        {
            var queryList = allDocs.Docs.Where(a => a.Category == category);
            var dg = new DocGroup(category, queryList.ToList());
            result.Docs.Add(dg);
        }

        return result;
    }

    public static Doc[] GetAllAdvancedDocs()
    {
        if (allDocs == null)
            allDocs = LoadDoc().Result;
        var queryAdv = allDocs.Docs.Where(a => a.Theme == "Advanced");
        return queryAdv.ToArray();
    }

    public static DocCollectionModel GetFavoritePages()
    {
        var allBookmarks = Util.BookMarkManager.GetInstance().GetAllBookmarks();
        var allDocs = GetBasicDocs();
        var allAdvDocs = GetAdvanceddDocs();

        DocCollectionModel result = new DocCollectionModel();

        foreach (var docGroup in allDocs.Docs)
        {
            var tempDGroup = new DocGroup(docGroup.GroupName, new List<Doc>());
            foreach (Doc doc in docGroup)
            {
                if (allBookmarks.Contains(doc.Id))
                {
                    tempDGroup.Add(doc);
                }
            }

            if (tempDGroup.Count > 0)
            {
                result.Docs.Add(tempDGroup);
            }
        }

        foreach (var docGroup in allAdvDocs.Docs)
        {
            var tempDGroup = new DocGroup(docGroup.GroupName, new List<Doc>());
            foreach (Doc doc in docGroup)
            {
                if (allBookmarks.Contains(doc.Id))
                    tempDGroup.Add(doc);
            }

            if (tempDGroup.Count > 0)
                result.Docs.Add(tempDGroup);
        }
        return result;
    }

    public static Doc[] GetFavoritePagesAllDocs()
    {
        var allDoc = LoadDoc().Result;
        var allBookmarks = new HashSet<string>();
        BookMarkManager.GetInstance().GetAllBookmarks().ForEach(a=>allBookmarks.Add(a));
        var result = new List<Doc>();
        
        foreach (Doc d in allDoc.Docs)
        {
            if(allBookmarks.Contains(d.Id))
                result.Add(d);
        }

        return result.ToArray();

    }
}