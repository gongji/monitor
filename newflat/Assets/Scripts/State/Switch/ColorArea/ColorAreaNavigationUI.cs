using DataModel;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorAreaNavigationUI : NavigationUIBase
{
    public  void CreateNavigateUI(List<Transform> currentData)
    {
        DeleteAllUI();
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Dictionary<GameObject, BoxCollider> dic = new Dictionary<GameObject, BoxCollider>();
        foreach (Transform child in currentData)
        {
            if(child.name.Contains(Constant.ColliderName))
            {
                continue;
            }
            Transform collider = FindObjUtility.GetChild(child, Constant.ColliderName.ToLower());
            if(collider==null)
            {
                continue;
            }
            //计算4个顶点
            Vector3[] vs = Object3dUtility.GetBoxColliderVertex(collider.GetComponent<BoxCollider>());

            Vector3 uiPostion = GetMaxXValue(vs);
            GameObject navaUI = TransformControlUtility.CreateItem("Text", canvas.transform);
            navaUI.GetComponent<RectTransform>().pivot = new Vector2(0f, 0.5f);
            navaUIList.Add(navaUI);
            navaUI.GetComponent<TMPro.TextMeshProUGUI>().alignment = TextAlignmentOptions.Justified;
            navaUI.GetComponent<TMPro.TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;

            string[] strs = child.name.Split('_');
            navaUI.name = strs[strs.Length - 1];
            navaUI.GetComponentInChildren<TextMeshProUGUI>().text = navaUI.name;

            dic.Add(navaUI, collider.GetComponent<BoxCollider>());
            navaUI.GetComponent<RectTransform>().anchoredPosition = uiPostion;
        }

        StartCoroutine(UpdateUIPostion(dic));
    }


    
}
