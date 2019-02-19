using State;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloorMouseOver : MonoBehaviour
{
    public string sceneid;

    protected void OnMouseDown()
    {
        Main.instance.stateMachineManager.SwitchStatus<FloorState>(sceneid);
    }

    private GameObject floorTips;
    private void Start()
    {
        Transform t = GameObject.Find("Canvas/floortips").transform;
        floorTips = TransformControlUtility.CreateItem("Tips/EquipmentTips", t);
        floorTips.name = transform.parent.name;
        floorTips.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 30);
        sceneid = SceneData.GetIdByNumber(transform.parent.name);
        floorTips.GetComponentInChildren<TextMeshProUGUI>().text = SceneData.GetNameByNumber(transform.parent.name);

    }

    private bool isVisible = false;
    void Update()
    {
        if (floorTips != null && isVisible)
        {
            
            floorTips.GetComponent<RectTransform>().anchoredPosition = UIUtility.WorldToUI(transform.position, Camera.main);
        }

        floorTips.SetActive(isVisible);
    }
     protected void OnMouseEnter()
    {
        isVisible = true;
        EffectionUtility.PlayMulitMaterialEffect(transform.parent, Color.blue);
    }

    protected void OnMouseExit()
    {
        isVisible = false;
        EffectionUtility.StopMulitMaterialEffect(transform.parent);
    }

    protected void OnDisable()
    {
        isVisible = false;
        EffectionUtility.StopMulitMaterialEffect(transform.parent);
    }

}
