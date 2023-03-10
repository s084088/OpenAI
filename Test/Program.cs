using SyNemo.OpenAI;

//注册
OpenAI.Register("sk" + "-" + "GHTX95ncrJK6985HPisPT3BlbkFJkbuAC9xtMEZhrPLHqM5O");

//创建会话
Chat35 chatGPT = new();

string q1 = "什么是相对论";
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

//无上下文
string b1 = await chatGPT.AskWithOutContext(q1);
string b2 = await chatGPT.AskWithOutContext(q2);
string b3 = await chatGPT.AskWithOutContext(q3);

Console.WriteLine("我: " + q1);
Console.WriteLine("AI: " + b1);
Console.WriteLine("我: " + q2);
Console.WriteLine("AI: " + b2);
Console.WriteLine("我: " + q3);
Console.WriteLine("AI: " + b3);