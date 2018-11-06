using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTips : MonoBehaviour {

    private GameObject selectEffection = null;

    public string id = string.Empty;

    public Vector3 scaleEffetion = Vector3.one;
    //缩放系数
    private float scaleIntensity = 1.3f;
    void OnMouseEnter()
    {

        //isShowTip = true;
        //Debug.Log (transform.name);//可以得到物体的名字
        if(selectEffection==null)
        {
            selectEffection = TransformControlUtility.CreateItem("Effection/select",transform);

            // IState currentstate = Main.instance.stateMachineManager.mCurrentState;
            selectEffection.transform.localPosition = selectEffection.transform.localPosition + (selectEffection.transform.forward * -5.0f);
            selectEffection.transform.localScale = scaleEffetion;
            transform.localScale = transform.localScale * scaleIntensity;
        }

    }
    void OnMouseExit()
    {
        if(selectEffection != null)
        {
            GameObject.Destroy(selectEffection);
            transform.localScale = transform.localScale / scaleIntensity;
        }
    }
    
    
    /// <summary>
    /// 进行定位
    /// </summary>
    public void OnMouseDown()
    {
        IState currentstate = Main.instance.stateMachineManager.mCurrentState;

        if(currentstate is AreaState && AppInfo.Platform == BRPlatform.Browser)
        {
            Main.instance.stateMachineManager.SwitchStatus<BuilderState>(id);
        }
        else if(currentstate is FloorState)
        {
            Main.instance.stateMachineManager.SwitchStatus<RoomState>(id);
        }
        

    }

    
}
