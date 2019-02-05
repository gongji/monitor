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
            GameObject colliderT = FindObjUtility.GetTransformChildByName(child, Constant.ColliderName.ToLower());
            if(colliderT == null)
            {
                continue;
            }

            MeshCollider meshCollider = colliderT.GetComponent<MeshCollider>();
            if (meshCollider != null)
            {
                GameObject.DestroyImmediate(meshCollider);
            }
            BoxCollider boxCollider = colliderT.GetComponent<BoxCollider>();
            if(boxCollider==null)
            {
                boxCollider = colliderT.gameObject.AddComponent<BoxCollider>();
            }


            Vector3[] vs = Object3dUtility.GetBoxColliderVertex(boxCollider);

            Vector3 uiPostion = GetMaxXValue(vs);
            GameObject navaUI = TransformControlUtility.CreateItem("Text", canvas.transform);
            navaUI.GetComponent<RectTransform>().pivot = new Vector2(0f, 0.5f);
            navaUIList.Add(navaUI);
            navaUI.GetComponent<TMPro.TextMeshProUGUI>().alignment = TextAlignmentOptions.Justified;
            navaUI.GetComponent<TMPro.TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
            navaUI.GetComponent<TMPro.TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;

            string[] strs = child.name.Split('_');
            navaUI.name = strs[strs.Length - 1];
            navaUI.GetComponentInChildren<TextMeshProUGUI>().text = SceneData.GetNameByNumber(child.name);

            dic.Add(navaUI, boxCollider);
            navaUI.GetComponent<RectTransform>().anchoredPosition = uiPostion;
        }

        StartCoroutine(UpdateUIPostion(dic));
    }


    
}
