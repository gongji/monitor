using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentIcon : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public EquipmentItem equipmentItem;
  
    private TMPro.TextMeshProUGUI text;
    void Start()
    {
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (equipmentItem != null)
        {
            text.text = equipmentItem.name;
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
        if(!string.IsNullOrEmpty(equipmentItem.id )&&  EquipmentData.GetAllEquipmentData.ContainsKey(equipmentItem.id))
        {
            GameObject item = EquipmentData.GetAllEquipmentData[equipmentItem.id];
            if(item.GetComponent<BaseEquipmentControl>()!=null)
            {
                item.GetComponent<BaseEquipmentControl>().OnMouseClick();
            }
        }
       
    }
}
