namespace IlyfairyLib.Utils.Bread;

public class BreadManager
{
    public BreadSession Origin { get; set; }
    public Dictionary<string, BreadSession> Sessions = new();
    public BreadManager(params BreadNode[] nodes)
    {
        BreadNode parent = new((context) => true, (context) => { }, nodes);
        Origin = new BreadSession(parent);
    }
    public object? Run(string id, object startContext)
    {
        BreadSession session;
        lock (Sessions)
        {
            if (!Sessions.TryGetValue(id, out session!))
            {
                session = Origin.Clone(id);
                Sessions.Add(id, session);
            }
        }

        if (session.Current.SubNodes.Count == 0)
        {
            session.Reset();
        }

        foreach (var sub in session.Current.SubNodes)
        {
            session.Context.StartContent = startContext;
            if (Next(session, sub, out object? output))
            {
                //如果子节点执行了
                return output;
            }
        }
        session.Reset();
        return null;
    }

    public bool Next(BreadSession session, BreadNode next, out object? output)
    {
        if (next.Condition(session.Context))
        {
            session.Context.Status = new();
            session.Context.Output = null;
            next.Callback(session.Context);
            output = session.Context.Output;

            if (session.Context.Status.IsReset)
            {
                //重置
                session.Reset();
            }
            else if (session.Context.Status.IsGotoParent)
            {
                //重回上一个节点
                session.GotoParent();
            }
            else if (session.Context.Status.IsPause)
            {
                //暂停
            }
            else
            {
                //到达下一个节点
                session.GotoNext(next);
            }
            return true;
        }

        output = null;
        return false;
    }
}
