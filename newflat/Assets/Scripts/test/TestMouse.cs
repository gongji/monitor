using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestMouse : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{


    public void OnPointerClick(PointerEventData eventData) {

        //if(eventData.clickCount==2)
        //{
        //    Debug.Log("shuangji");
        //}
        //else
        //{
            Debug.Log("danji");
        //}
    }

    void OnGUI()
    {
        if (Event.current.isMouse && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2)
        {
            if (isOver)
            {
                print("double click " + transform.name);
            }
        }

    }

    

    private bool isOver = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
    }

    double t1;
    double t2;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            t2 = Time.realtimeSinceStartup;
            if (t2 - t1 < 0.2f)
            {

                Debug.Log("双击");
            }
            else
            {
                Debug.Log("单击");
            }
            t1 = t2;
        }
        
       
    }
}
