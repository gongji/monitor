using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 切换到层
/// </summary>
public class MouseFloorText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    private GameObject selectEffection = null;

    ///[HideInInspector]
    public string id = string.Empty;

    private float scale = 1.2f;

    void OnMouseEnter()
    {

     
    }
    void OnMouseExit()
    {
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectEffection == null)
        {
            selectEffection = TransformControlUtility.CreateItem("Effection/select", transform);
            transform.localScale = transform.localScale * scale;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selectEffection != null)
        {
            GameObject.Destroy(selectEffection);
            transform.localScale = transform.localScale / scale;
        }
    }

    /// <summary>
    /// 切换到层
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
         Main.instance.stateMachineManager.SwitchStatus<FloorState>(id);
        
    }

    
}
