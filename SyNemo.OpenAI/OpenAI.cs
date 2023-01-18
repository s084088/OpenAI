using SyNemo.OpenAI.Comm;

namespace SyNemo.OpenAI
{
    /// <summary>
    /// OpenAI基础设置
    /// </summary>
    public static class OpenAI
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="key">您在OpenAI官方申请的Key</param>
        /// <remarks>
        ///     <see href="https://beta.openai.com/account/api-keys">申请地址</see>
        /// </remarks>
        public static void Register(string key) => HttpHelper.Init(key);
    }
}