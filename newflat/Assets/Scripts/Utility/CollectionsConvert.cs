
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Utils
{
    public class CollectionsConvert
    {
		
       // private static JSONParameters jsonParamet;
        static CollectionsConvert() {
            //jsonParamet = new JSONParameters();
            //jsonParamet.UseExtensions = false;
            //jsonParamet.ShowReadOnlyProperties = true;
            //jsonParamet.UseUTCDateTime = false;
        }

        public static T ToObject<T>(IDictionary<string, object> data) {

            // return fastJSON.JSON.ToObject<T>(data, jsonParamet);
            T t = default(T);
            // return fastJSON.JSON.ToObject<T>(data, jsonParamet);
            return t;
        }

        public static T ToObject<T>(Object data)
            
        {
            T t = default(T);
            // return fastJSON.JSON.ToObject<T>(data, jsonParamet);
            return t;
        }

        public static T ToObject<T>(String data)
        {
            //return fastJSON.JSON.ToObject<T>(data, jsonParamet);
           // return UnityEngine.JsonUtility.FromJson<T>(data);
           // return LitJson.JsonMapper.ToObject<T>(data);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
        }

        public static object Parse(string json) {
            return null;
            //return fastJSON.JSON.Parse(json);
        }

        public static string ToJSON(object o)
        {
            // return fastJSON.JSON.ToJSON(json, jsonParamet);
            return Newtonsoft.Json.JsonConvert.SerializeObject(o);
            
        }
        
    }
}
