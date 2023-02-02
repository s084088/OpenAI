using System;

namespace SyNemo.OpenAI.Comm
{
    /// <summary>
    /// SyNemo.OpenAI的包异常
    /// </summary>
    public class OpenAIException : Exception
    {
        /// <summary>
        /// 异常代码
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorCode">异常代码</param>
        /// <param name="message">异常信息</param>
        /// <param name="inner"></param>
        internal OpenAIException(ErrorCode errorCode, string message = null, Exception inner = null) : base(message, inner) => ErrorCode = errorCode;
    }

    /// <summary>
    /// 异常枚举
    /// </summary>
    public enum ErrorCode
    {

        /// <summary>
        /// 网络异常
        /// </summary>
        NetFail,

        /// <summary>
        /// 无效Token
        /// </summary>
        TokenInvalid,

        /// <summary>
        /// 账户欠费
        /// </summary>
        Arrears,
    }
}
