# Bread

(不知道叫什么,  看示例qwq  

## 示例

```sharp
using IlyfairyLib.Utils.Bread;
using System.Text;

var shopNode = new BreadNode()
{
    Condition = (context) =>
    {
        return (context.StartContent as string) == "shop";
    },
    Callback = (context) =>
    {
        context.Context = new StringBuilder();
        Console.WriteLine("欢迎来购物");
        Console.WriteLine("1. 买手机");
        Console.WriteLine("2. 买电脑");
        Console.WriteLine("y 结算");
    }
};

var phoneNode = new BreadNode()
{
    Condition = (context) =>
    {
        return (context.StartContent as string) == "1";
    },
    Callback = (context) =>
    {
        var s = context.Context as StringBuilder;
        s.AppendLine("买了一台手机");
        Console.WriteLine("已购买");
    }
};
var computerNode = new BreadNode()
{
    Condition = (context) =>
    {
        return (context.StartContent as string) == "2";
    },
    Callback = (context) =>
    {
        var s = context.Context as StringBuilder;
        s.AppendLine("买了一台电脑");
        Console.WriteLine("已购买");
    }
};

var completeNode = new BreadNode()
{
    Condition = (context) =>
    {
        return (context.StartContent as string) == "y";
    },
    Callback = (context) =>
    {
        var s = context.Context as StringBuilder;
        Console.WriteLine("结算");
        context.Output = s.ToString();//输出购买信息
    }
};

//购物的子节点是买某东西
shopNode.SubNodes.Add(phoneNode);
shopNode.SubNodes.Add(computerNode);

phoneNode.SubNodes.Add(phoneNode); //买了还可以继续买
phoneNode.SubNodes.Add(computerNode); //如果又想买手机和电脑
computerNode.SubNodes.Add(computerNode); //买了还可以继续买
computerNode.SubNodes.Add(phoneNode); //如果又想买手机和电脑

phoneNode.SubNodes.Add(completeNode); //按Y结算
computerNode.SubNodes.Add(completeNode); //按Y结算


BreadManager manager = new(shopNode);

string? input;
while (true)
{
    Console.Write(">>> ");
    input = Console.ReadLine();
    if (input == null) break;
    var result = manager.Run("console", input) as string;
    if(result is not null)
    {
        Console.WriteLine("输出: \n" + result);
    }
}



// output:
/*
>>> shop
欢迎来购物
1. 买手机
2. 买电脑
y 结算
>>> 1
已购买
>>> 1
已购买
>>> 2
已购买
>>> y
结算
输出:
买了一台手机
买了一台手机
买了一台电脑
*/
```
