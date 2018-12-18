using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse2DTips : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    private Vector3 originalScale = Vector3.one;

    public string sceneid = "";
    void Star()
    {
        originalScale = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
       transform.localScale = originalScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Main.instance.stateMachineManager.SwitchStatus<RoomState>(sceneid);
    }


}
