namespace IlyfairyLib.Utils.Bread;

public class BreadContext
{
    public BreadStatus Status { get; set; }
    public object StartContent { get; set; }
    public object Context { get; set; }
    public object Output { get; set; }
    public BreadContext()
    {
        Status = new BreadStatus();
    }
    
    public T? GetContext<T>() where T : class
    {
        return Context as T;
    }
}