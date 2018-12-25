using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentIcon : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject equipmentObject;
  
    private TMPro.TextMeshProUGUI text;
    void Start()
    {
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (equipmentObject != null)
        {
            text.text = equipmentObject.GetComponent<Object3DElement>().equipmentData.name;
        }
        text.enabled = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.enabled = false;
    }

   
    public void OnPointerClick(PointerEventData eventData)
    {
        if(equipmentObject!=null)
        {
            equipmentObject.GetComponent<BaseEquipmentControl>().OnMouseClick();
        }
       
    }

    void Update()
    {
        if(equipmentObject!=null)
        {
            transform.GetComponent<RectTransform>().anchoredPosition = UIUtility.WorldToUI(equipmentObject.transform.position, Camera.main);
        }
        
    }
   
}
