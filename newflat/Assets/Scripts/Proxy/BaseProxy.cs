using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BaseProxy {
    /// <summary>
    /// 通过数据id得到发送的数据
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public static Dictionary<string, string> GetPostDataByArray(List<string> ids)
    {
        string resultPostData = FormatUtil.ConnetString(ids, ",");
        return GetPostDataByString(resultPostData);
    }

    public static Dictionary<string, string> GetPostDataByString(string resultPostData)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>();
        postData.Add("result", resultPostData);

        return postData;
    }

}
