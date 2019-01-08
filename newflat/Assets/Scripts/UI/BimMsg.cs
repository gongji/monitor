using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BimMsg : MonoBehaviour {

    private GameObject bomGameObject;
    public void OnChanged()
    {
        DoUI();
    }

    private void OnDisable()
    {
        if(bomGameObject!=null)
        {
            GameObject.DestroyImmediate(bomGameObject);
            bomGameObject = null;
        }
       
    }

    private void OnEnable()
    {
        DoUI();
    }

    private void DoUI()
    {
        if (GetComponent<Toggle>().isOn && !bomGameObject)
        {
            bomGameObject = TransformControlUtility.CreateItem("UI/Bom", UIUtility.GetRootCanvas());
            bomGameObject.transform.SetParent(transform);
            bomGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -20);
            // Vector3 v=  GetComponent<RectTransform>().anchoredPosition3D;
            // Vector2 uiPostion  = UIUtility.WorldToUI(transform.position, Camera.main);

            // bomGameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(v.x, v.y-20, v.z);

        }
        else
        {
            GameObject.DestroyImmediate(bomGameObject);
            bomGameObject = null;
        }
    }
}
