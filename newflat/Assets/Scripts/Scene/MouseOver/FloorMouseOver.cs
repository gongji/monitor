using State;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloorMouseOver : MonoBehaviour
{
    public string sceneid;

    protected void OnMouseDown()
    {
        if ((EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()))
        {
            return;
        }

        Main.instance.stateMachineManager.SwitchStatus<FloorState>(sceneid);
    }

    private GameObject floorTips;
    private void Start()
    {

        CreateTips();
    }

    private void CreateTips()
    {
        if(floorTips == null) 
        {
            Transform t = GameObject.Find("Canvas/floortips").transform;
            floorTips = TransformControlUtility.CreateItem("Tips/EquipmentTips", t);
            floorTips.name = transform.parent.name;
            floorTips.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 30);
            sceneid = SceneData.GetIdByNumber(transform.parent.name);
            floorTips.GetComponentInChildren<TextMeshProUGUI>().text = SceneData.GetNameByNumber(transform.parent.name);
        }
       
    }

    private bool isVisible = false;
    void Update()
    {
        if (floorTips != null && isVisible)
        {
            
            floorTips.GetComponent<RectTransform>().anchoredPosition = UIUtility.WorldToUI(transform.position, Camera.main);
        }

        if(floorTips)
        {
            floorTips.SetActive(isVisible);
        }
        
    }
     protected void OnMouseEnter()
    {
        

        isVisible = true;
        EffectionUtility.PlaySinlgeMaterialEffect(transform.parent, Color.yellow);
    }

    protected void OnMouseExit()
    {
       

        isVisible = false;
        EffectionUtility.StopSinlgeMaterialEffect(transform.parent);
    }

    protected void OnDisable()
    {
        isVisible = false;
        EffectionUtility.StopMulitMaterialEffect(transform.parent);
        if(floorTips!=null)
        {
            GameObject.Destroy(floorTips);
        }
    }

    protected void OnEnable()
    {
        
         CreateTips();
        
    }

}
