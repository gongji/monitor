using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Utils
{
    public static class StrUtil
    {
        /// <summary>
        ///用charStr将字符串连接起来
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public static string ConnetString(IList<string> strs, string singleChar)
        {
            if (strs.Count == 0)
            {
                return string.Empty;
            }
            string result = "";
            foreach (string temp in strs)
            {
                result += temp + singleChar;
            }
            result = result.Substring(0, result.Length - 1);
            return result;
        }


        /// <summary>
        /// 获取一个新的guid
        /// </summary>
        /// <returns></returns>
        public static string GetNewGuid()
        {
            return System.Guid.NewGuid().ToString("N");
        }
    }

  
}


