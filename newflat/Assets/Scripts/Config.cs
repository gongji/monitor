using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System;
using UnityEngine;

static public class Config
{
    static private Dictionary<string, string> dictionary = null;

    static public Action configFileNotFound;
    static public Action<object> print;
    public static MonoBehaviour mono;

    static public void Startload( MonoBehaviour _mono,System.Action callBack)
    {
        mono = _mono;
        if(dictionary==null)
        {
            dictionary = new Dictionary<string, string>();
        }
        mono.StartCoroutine(LoadConfig(callBack));

    }

    static private IEnumerator LoadConfig(Action calllBack)
    {
        string url = string.Empty;
#if UNITY_EDITOR
        url = "file://" + Application.dataPath + "/StreamingAssets/config.ini";
        

#else
        url = Application.dataPath + "/StreamingAssets/config.ini";
#endif
       //Debug.LogError(url);
        WWW www = new WWW(url);

        yield return www;
        if (www.error == null)
        {
            string content = www.text;

           // Debug.LogError("content="+ content);
            string[] lines = content.Split('\n');
            GetTextInfo(dictionary, lines);
          
            Debug.Log("config server success");
            //WebPlayerMgr.Instance.SetIpAndPort();
            calllBack();
        }
        else
        {
            Debug.LogError(www.error);
        }
    }
    /// <summary>
    /// 形成字典
    /// </summary>
    /// <param name="map"></param>
    /// <param name="text"></param>
    static private  void GetTextInfo(Dictionary<string, string> map, string[] text)
    {
        if (map == null || text == null)
        {
            throw new ArgumentNullException();
        }

        if (text.Length == 0)
            return;
        string[] kv;
        char equal = '=';
        string sharp = "#";
        foreach (string line in text)
        {
            if (string.IsNullOrEmpty(line))
                continue;
            if (line.Trim().StartsWith(sharp))
                continue;
            kv = line.Split(equal);
            if (kv.Length < 2)
                continue;
            kv[0] = kv[0].Trim();
            if (string.IsNullOrEmpty(kv[0]))
                continue;
            // 当参数中含有'='号时，拼接参数
            if (kv.Length > 2)
            {
                string v = kv[1];
                for (int i = 2; i < kv.Length; ++i)
                {
                    v += equal + kv[i];
                }
                kv[1] = v;
            }
            kv[1] = kv[1].Trim();
            map[kv[0]] = kv[1];
        }

    }

    static public void parse(string key, Action<string> callback)
    {
        if (dictionary == null)
        {
            //load(Application.streamingAssetsPath + "/config.ini");

        }
        if (dictionary.ContainsKey(key))
        {
            callback(dictionary[key]);
        }
    }

    static public string parse(string key)
    {
        if (dictionary!=null && dictionary.ContainsKey(key))
        {
            return dictionary[key];

        }
       return "";
    }

    //public void parse(string key, Action<string> callback)
    //{
    //    if (dictionary.ContainsKey(key))
    //    {
    //        callback(dictionary[key]);
    //    }
    //}
}
