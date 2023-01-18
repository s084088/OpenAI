namespace Nemo.OpenAI
{
    /// <summary>
    /// OpenAI基础设置
    /// </summary>
    public static class OpenAI
    {
        internal static string Key { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="key">您在OpenAI官方申请的Key</param>
        /// <remarks>
        ///     <see href="https://beta.openai.com/account/api-keys">申请地址</see>
        /// </remarks>
        public static void Register(string key) => Key = key;
    }
}