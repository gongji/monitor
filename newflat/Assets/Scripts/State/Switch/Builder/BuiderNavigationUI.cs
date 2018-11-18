using DataModel;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed  class BuiderNavigationUI
{

    private static  List<GameObject> navaUIList = new List<GameObject>();
    public static void CreateNavigateUI(List<Object3dItem> currentData)
    {
        DeleteAllUI();
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        foreach (Object3dItem object3dItem in currentData)
        {
            GameObject collider = SceneUtility.GetSceneCollider(object3dItem.number);
            //计算4个顶点
            Vector3[] vs = Object3dUtility.GetBoxColliderVertex(collider.GetComponent<BoxCollider>());

            Vector3 uiPostion = GetMaxXValue(vs);
            GameObject navaUI = TransformControlUtility.CreateItem("Text", canvas.transform);
            navaUIList.Add(navaUI);

            navaUI.name = "L" + object3dItem.number.Substring(object3dItem.number.Length - 1, 1);
            navaUI.GetComponentInChildren<TextMeshProUGUI>().text = navaUI.name;

            MouseFloorText floorText = navaUI.gameObject.AddComponent<MouseFloorText>();
            floorText.id = object3dItem.id;
            navaUI.GetComponent<RectTransform>().anchoredPosition = uiPostion;
        }
    }

    public static void DeleteAllUI()
    {
        foreach (GameObject ui in navaUIList)
        {
            GameObject.Destroy(ui);
        }
        navaUIList.Clear();
    }
    /// <summary>
    /// 求出4个点中x的最大值
    /// 
    /// </summary>
    /// <param name="vs"></param>
    /// <returns></returns>
    private static Vector3 GetMaxXValue(Vector3[] vs)
    {
        float result = 0.0f;
        Dictionary<float, Vector3> dic = new Dictionary<float, Vector3>();
        foreach (Vector3 v in vs)
        {
            Vector2 uipostion = UIUtility.WorldToUI(v, Camera.main);
            if (uipostion.x > result)
            {
                result = uipostion.x;
                dic.Add(uipostion.x, uipostion);
            }
        }

        if (dic.ContainsKey(result))
        {
            return dic[result];
        }
        return Vector3.zero;
    }
}
