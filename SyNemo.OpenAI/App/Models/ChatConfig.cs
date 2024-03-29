﻿using System.Collections.Generic;

namespace SyNemo.OpenAI.Models;

/// <summary>
/// 聊天配置
/// </summary>
public class ChatConfig
{
    /// <summary>
    /// 回答多样性，取值范围为 0.0 ~ 1.0，默认值为0.5。
    /// </summary>
    public float Temperature { get; set; } = 0.5f;

    /// <summary>
    /// 质量好的前%多，取值范围为 0.0 ~ 1.0，默认值为 1。
    /// </summary>
    public float Top_p { get; set; } = 1f;

    /// <summary>
    /// 历史对话
    /// </summary>
    public IEnumerable<IChatMessage> Messages { get; set; }
}

/// <summary>
/// 聊天配置
/// </summary>
public class Chat35Config
{
    /// <summary>
    /// 历史对话
    /// </summary>
    public IEnumerable<IChatMessage> Messages { get; set; }

    /// <summary>
    /// 系统指令
    /// </summary>
    public string SystemCommand { get; set; }

    /// <summary>
    /// 使用的模型
    /// </summary>
    public string Model { get; set; }
}