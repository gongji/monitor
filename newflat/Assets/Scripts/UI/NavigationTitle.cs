using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DataModel;

public class NavigationTitle : MonoBehaviour {

 

    public static NavigationTitle instance;
    void Start () {
        instance = this;
        GetComponent<TextMeshProUGUI>().text = "";
    }
	
    public void ShowTitle(string id)
    {
        GetComponent<TextMeshProUGUI>().text = GetName(id);
    }

    private string GetName(string id)
    {
        string result = "当前位置：园区";
        if(string.IsNullOrEmpty(id))
        {
            return result;
        }

        Object3dItem item = SceneData.FindObjUtilityect3dItemById(id);
        List<string> names = new List<string>();
        string parentid = string.Empty;
        names.Add(item.name);
        while (!string.IsNullOrEmpty(item.parentid))
        {
            parentid = item.parentid;
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
}
