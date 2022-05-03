namespace IlyfairyLib.Utils.Bread;

public class BreadSession
{
    public string Id { get; private set; }
    public BreadNode Origin { get; init; }
    public BreadNode Current { get; private set; }
    public BreadNode Parent { get; private set; }
    public BreadContext Context { get; private set; }
    private BreadSession(string id, BreadNode node)
    {
        Id = id;
        Origin = node;
        Current = node;
        Parent = node;
    }

    public BreadSession(BreadNode node)
    {
        Id = null!;
        Origin = node;
        Current = node;
        Parent = node;
    }

    public void Reset()
    {
        Current = Origin;
        Parent = Origin;
        Context = new();
    }

    public void GotoParent()
    {
        Current = Parent;
    }

    public void GotoNext(BreadNode nextNode)
    {
        Parent = Current;
        Current = nextNode;
    }


    public BreadSession Clone(string id)
    {
        BreadSession session = new(id, Origin);
        session.Context = new();
        return session;
    }
}
