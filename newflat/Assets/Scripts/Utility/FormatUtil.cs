using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DataModel;

/// <summary>
/// 格式化
/// </summary>
public class FormatUtil {

    /// <summary>
    ///用charStr将字符串连接起来
    /// </summary>
    /// <param name="strs"></param>
    /// <param name="?"></param>
    /// <returns></returns>
    public static string ConnetString(List<string> strs, string singleChar)
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
    /// 按照digit保留小数
    /// </summary>
    /// <param name="data"></param>
    /// <param name="digit"></param>
    /// <returns></returns>
    public static float FloatFomart(float data, int digit)
    {
        int j = (int)(data * Mathf.Pow(10, digit));
        data = j / Mathf.Pow(10, digit);

        return data;
    }


    /// <summary>
    /// 对data保留两位有效数字
    /// </summary>
    /// <param name="data"></param>
    /// <param name="digit"></param>
    /// <returns></returns>
    public static string StringFomart(string data)
    {
       return  string.Format("{0:00}", data);
    }

    /// <summary>
    /// 截取字符串的数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string  SplitNum(string str)
    {
        MatchCollection ma = Regex.Matches(str, @"\d+");
        string disStr = "";
        foreach (Match item in ma)
        {
            disStr += item.Value;
        }
        return disStr;

    }

    public static void FormatEquipmentData(EquipmentItem item)
    {
        item.x = FormatUtil.FloatFomart(item.x, 2);
        item.y = FormatUtil.FloatFomart(item.y, 2);
        item.z = FormatUtil.FloatFomart(item.z, 2);

        item.rotationX = FormatUtil.FloatFomart(item.rotationX, 2);
        item.rotationY = FormatUtil.FloatFomart(item.rotationY, 2);
        item.rotationZ = FormatUtil.FloatFomart(item.rotationZ, 2);

        item.scaleX = FormatUtil.FloatFomart(item.scaleX, 4);
        item.scaleY = FormatUtil.FloatFomart(item.scaleY, 4);

        item.scaleZ = FormatUtil.FloatFomart(item.scaleZ, 4);
    }


}
