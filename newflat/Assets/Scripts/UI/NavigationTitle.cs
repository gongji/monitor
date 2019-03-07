using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DataModel;

public class NavigationTitle : MonoBehaviour,IEventListener {
    public static NavigationTitle instance;
    void Start() {
        instance = this;
        GetComponent<TextMeshProUGUI>().text = "";
    }

    public void ShowTitle(string id)
    {
        //Debug.Log("-1");
        string titleName = "";
        if (id.Equals("-1"))
        {
            id = string.Empty;
            titleName = GetName(id);
        }
        else
        {
            Object3dItem item = SceneData.FindObjUtilityect3dItemById(id);
            if(item != null && item.number.Equals(Constant.Main_dxName))
            {
                id = string.Empty;
                titleName = GetName(id);
            }
            else
            {
                titleName = GetName(id);
            }
            
        }
        GetComponent<TextMeshProUGUI>().text = titleName;
    }

    private string GetName(string id)
    {
        
        string result = "当前位置："+Config.parse("areaName");
        if(string.IsNullOrEmpty(id))
        {
            return result;
        }

        Object3dItem item = SceneData.FindObjUtilityect3dItemById(id);
        List<string> names = new List<string>();
        string parentid = string.Empty;
        names.Add(item.name);
        while (!string.IsNullOrEmpty(item.parentsId) && !item.parentsId.Equals("0"))
        {
            parentid = item.parentsId;
            item = SceneData.FindObjUtilityect3dItemById(parentid);
            names.Add(item.name);

        }
        names.Add(result);
        names.Reverse();
        result = names[0];
        for (int i=1;i< names.Count;i++)
        {
            result = result + "->" + names[i];
        }
        return result;
    }

    public bool HandleEvent(string eventName, IDictionary<string, object> dictionary)
    {
        // throw new System.NotImplementedException();

        return true;
    }
}
