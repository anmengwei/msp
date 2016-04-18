using System;

namespace MSP.Core.Utils
{
    /// <summary>
    /// 参数断言
    /// </summary>
    public static class Ex
    {
        /// <summary>
        /// 确保参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public static void EnsureNotNull(string parameterName, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// 确保参数
        /// </summary>
        public static void Ensure(bool ok, string message = "参数错误")
        {
            if (!ok)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
