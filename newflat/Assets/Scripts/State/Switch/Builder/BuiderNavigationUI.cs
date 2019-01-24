using DataModel;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public   class BuiderNavigationUI: NavigationUIBase
{
    public  void CreateNavigateUI(List<Object3dItem> currentData)
    {
        DeleteAllUI();
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Dictionary<GameObject, BoxCollider> dic = new Dictionary<GameObject, BoxCollider>();
        foreach (Object3dItem object3dItem in currentData)
        {
            GameObject collider = SceneUtility.GetSceneCollider(object3dItem.number);
            //计算4个顶点
            Vector3[] vs = Object3dUtility.GetBoxColliderVertex(collider.GetComponent<BoxCollider>());

            Vector3 uiPostion = GetMaxXValue(vs);
            GameObject navaUI = TransformControlUtility.CreateItem("Text", canvas.transform);
            navaUIList.Add(navaUI);

           // navaUI.name = "F" + object3dItem.number.Substring(object3dItem.number.Length - 1, 1);
           navaUI.name = "F" + object3dItem.sortIndex;
            navaUI.GetComponentInChildren<TextMeshProUGUI>().text = navaUI.name;

            MouseFloorText floorText = navaUI.gameObject.AddComponent<MouseFloorText>();
            floorText.id = object3dItem.id;
            dic.Add(navaUI, collider.GetComponent<BoxCollider>());
            navaUI.GetComponent<RectTransform>().anchoredPosition = uiPostion;
        }

        StartCoroutine(UpdateUIPostion(dic));
    }


    
}
