namespace LinqReferencePro.ViewModels;

public class DocGroup : List<Doc>
{

    public DocGroup(string groupName, List<Doc> dd) : base(dd)
    {
        GroupName = groupName;
    }

    public string GroupName { get; set; }

}