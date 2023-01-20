﻿using SyNemo.OpenAI;

//注册
OpenAI.Register("sk" + "-" + "QtOQWB52BRNSfUj3iGFOT3BlbkFJpo48ZGuPMbzbe1uFHoGC");

//创建会话
Chat chatGPT = new();

string q1 = "我的OpenAI账户还有多少余额";
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