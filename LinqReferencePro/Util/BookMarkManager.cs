namespace LinqReferencePro.Util;

public class BookMarkManager
{
    private string KEY = "BookMarkIds";
    private char SEPARATOR = ',';

    private static BookMarkManager instance;

    public static BookMarkManager GetInstance()
    {
        if (instance == null)
            instance = new BookMarkManager();
        return instance;
    }

    public void SetBookMark(string docId)
    {
        var bookmarks = LoadBookmarks();
        if (!bookmarks.Contains(docId))
            bookmarks.Add(docId);
        SaveBookmarks(bookmarks);
    }

    public void RemoveBookMark(string docId)
    {
        var bookmarks = LoadBookmarks();
        if (bookmarks.Contains(docId))
            bookmarks.Remove(docId);
        SaveBookmarks(bookmarks);
    }

    private List<string> LoadBookmarks()
    {
        string bookMarkSrc = Preferences.Default.Get<string>(KEY, null);
        if (bookMarkSrc != null)
        {
            var bookmarks = ToBookmarks(bookMarkSrc);
            return bookmarks;
        }
        var newBookmarks = new List<string>();
        return newBookmarks;
    }

    private void SaveBookmarks(List<string> bookMarks)
    {
        string values = string.Join(SEPARATOR, bookMarks.ToArray());
        Preferences.Default.Set(KEY, values);
    }

    private List<string> ToBookmarks(string bookMarkSrc)
    {
        var bookmarks = new List<string>();
        string[] arr = bookMarkSrc.Split(SEPARATOR);
        bookmarks.AddRange(arr);
        return bookmarks;
    }

    public bool IsBookmark(string docId)
    {
        var bookmarks = LoadBookmarks();
        return bookmarks.Contains(docId);
    }

    public List<string> GetAllBookmarks()
    {
        return LoadBookmarks();
    }


    public void Clean()
    {
        Preferences.Default.Set<string>(KEY, null);
    }
}