using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BimMsg : MonoBehaviour {

    public static BimMsg instacne;

    public bool isSelected = false;
    private void Awake()
    {
        instacne = this;
    }
    private GameObject bomGameObject;
    public void OnChanged()
    {
        isSelected = GetComponent<Toggle>().isOn;
        DoUI();
    }

    private void OnDisable()
    {
        if(bomGameObject!=null)
        {
            Remove();
        }
       
    }

    private void SplitBim(float sliderChange)
    {
      //  Debug.Log("123");
        BimMouse[] bimMouse = GameObject.FindObjectsOfType<BimMouse>();

        BomSeparate.UpdatePostion(sliderChange, bimMouse, SceneContext.sceneBox.GetComponent<BoxCollider>().bounds.center);
          
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
            bomGameObject.GetComponentInChildren<Slider>().onValueChanged.AddListener(SplitBim);
            // Vector3 v=  GetComponent<RectTransform>().anchoredPosition3D;
            // Vector2 uiPostion  = UIUtility.WorldToUI(transform.position, Camera.main);
            // bomGameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(v.x, v.y-20, v.z);
        }
        else
        {
            Remove();
        }
    }

    private void Remove()
    {
        if(bomGameObject!=null)
        {
            bomGameObject.GetComponentInChildren<Slider>().onValueChanged.RemoveAllListeners();
            GameObject.DestroyImmediate(bomGameObject);
            bomGameObject = null;
            BimResetPostion();
        }
    }

    private void BimResetPostion()
    {
        BimMouse[] bimMouses = GameObject.FindObjectsOfType<BimMouse>();
        foreach(BimMouse bimMouse  in bimMouses)
        {
            bimMouse.Reset();
        }
    }
}
