using System;

namespace Br.Utils
{
    /// <summary>
    /// 1、 java 的 System.currentTimeMillis() 计算的长整型，是从1970年1月1日开始，截止当前的毫秒数。
    /// 2、C#中计算毫秒数的方法 TimeSpan.TotalMilliseconds
    /// </summary>
    public static class UnixTimeUtility
    {
        public static DateTime m_javaLongTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 取致1970年1月1日起的时间毫秒
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns>时间毫秒数</returns>
        public static long GetTimeLong(DateTime datetime)
        {
            var interval = datetime.ToUniversalTime() - m_javaLongTime;
            return (long)interval.TotalMilliseconds;
        }

        /// <summary>
        /// 取致1970年1月1日起的时间毫秒
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns>时间毫秒数</returns>
        public static long GetTimeLong()
        {
            return GetTimeLong(DateTime.Now);
        }

        /// <summary>
        /// 返回日期时间对象
        /// </summary>
        /// <param name="milliseconds">时间毫秒</param>
        /// <returns>日期时间对象</returns>
        public static DateTime getDateTime(string milliseconds)
        {
            return getDateTime(long.Parse(milliseconds));
        }

        /// <summary>
        /// 返回日期时间对象
        /// </summary>
        /// <param name="milliseconds">时间毫秒</param>
        /// <returns>日期时间对象</returns>
        public static DateTime getDateTime(long milliseconds)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds(milliseconds).ToLocalTime();
        }
    }
}
