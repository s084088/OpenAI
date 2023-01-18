## 使用示例

```Install-Package SyNemo.OpenAI```

```C#
using SyNemo.OpenAI;

//注册
OpenAI.Register("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");

//创建会话
Chat chatGPT = new();

string q1 = "什么是相对论？";
string q2 = "简单介绍一些他的生平";
string q3 = "介绍一下他的其他贡献";

//对话
string a1 = await chatGPT.Ask(q1);
string a2 = await chatGPT.Ask(q2);
string a3 = await chatGPT.Ask(q3);



Console.WriteLine("我: " + q1);
Console.WriteLine("AI: " + a1);
Console.WriteLine("我: " + q2);
Console.WriteLine("AI: " + a2);
Console.WriteLine("我: " + q3);
Console.WriteLine("AI: " + a3);
```
