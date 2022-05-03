namespace IlyfairyLib.Utils.Bread;

public class BreadNode
{
    public List<BreadNode> SubNodes { get; init; } = new List<BreadNode>();//子节点
    public Func<BreadContext,bool> Condition { get; init; } //执行这个节点的条件
    public BreadNodeCallback Callback { get; init; } //执行当前节点回调
    public BreadNode(Func<BreadContext, bool> condition, BreadNodeCallback callback, params BreadNode[] subNodes)
    {
        Condition = condition;
        Callback = callback;
        SubNodes.AddRange(subNodes);
    }
    public BreadNode()
    {
    }
}
public delegate void BreadNodeCallback(BreadContext context);
