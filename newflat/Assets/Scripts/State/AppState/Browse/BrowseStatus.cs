using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlehub.UIControls;
using DG.Tweening;

public class BrowseStatus : AppBaseState
{

    public BrowseStatus()
    {

    }

    public override void Init()
    {
        BrowserUI.Create();
        //TreeViewControl.Instance.SetBrowserData();
    }

    public override void Update()
    {
        base.Update();
     
        checkSelectEquipment = CheckClickEquipment(ref hitTransform);
        if(checkSelectEquipment && hitTransform.Equals(MouseCheck.clickHitTransform))
        {
            BaseEquipmentControl bec = checkSelectEquipment.transform.GetComponent<BaseEquipmentControl>();
            if(bec!=null)
            {
                bec.OnMouseClick();
            }
           
        }

        if(MouseCheck.DOUBLE_CLICK)
        {
            Vector3 centerPostion = MouseCheck.clickHitPoint;
            float distance = Vector3.Distance(Camera.main.transform.position, centerPostion);
            if(distance>3.0f)
            {
                CameraAnimation.RotationScreenCenter(centerPostion, 1.0f, (postion, duringTime) => {
                    Main.instance.StartCoroutine(EffectionUtility.BlurEffection(duringTime, 0, 10));
                    Camera.main.transform.DOMove(postion, duringTime).OnComplete(() =>
                    {


                    });

                });

               
            }
            
        }
    }

    public override void OnGUI()
    {

    }

    public override void UpdateUI()
    {
    }

    
}


