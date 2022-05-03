namespace IlyfairyLib.Utils.Bread;

public class BreadStatus
{
    public bool IsReset { get; private set; }
    public bool IsGotoParent { get; private set; }
    public bool IsPause { get; private set; }
    public void Reset()
    {
        IsReset = true;
    }
    public void GotoParent()
    {
        IsGotoParent = true;
    }
    public void Pause()
    {
        IsPause = true;
    }
}