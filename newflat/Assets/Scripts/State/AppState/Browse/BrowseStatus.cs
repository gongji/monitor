using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlehub.UIControls;
using DG.Tweening;
using State;

public class BrowseStatus : AppBaseState
{

    public BrowseStatus()
    {

    }

    public override void Init()
    {
        BrowserUI.Create();
        
        
        WenshiduTimer.Instance.StartTimer();

        WebSocketService.Instance.StartSocket();

        //  SceneAlarmTimer.Instance.StartTimer();

        //TreeViewControl.Instance.SetBrowserData();
    }

    private static float maxDistance = 1.0f;
    private BaseEquipmentControl clickSelectEquipment;
    public override void Update()
    {
        base.Update();
     
        checkSelectEquipment = CheckClickEquipment(ref hitTransform);
       

        if (checkSelectEquipment && hitTransform.Equals(MouseCheck.clickHitTransform))
        {
           
            if(clickSelectEquipment!=null)
            {

                clickSelectEquipment.DestoryTestPointMenu();
            }
            clickSelectEquipment = checkSelectEquipment.transform.GetComponent<BaseEquipmentControl>();
            if(clickSelectEquipment != null)
            {

                clickSelectEquipment.OnMouseClick();
                return;
            }
           
        }

        if(MouseCheck.DOUBLE_CLICK && MouseCheck.clickHitTransform!=null)
        {
            if(Camera.main.GetComponent<CameraRotatoAround>()!=null || AppInfo.currentView == ViewType.View2D)
            {
                return;
            }

            if (clickSelectEquipment != null)
            {
                clickSelectEquipment.DestoryTestPointMenu();
            }

            //if(SubsystemMsg.isSubSystemState)
            //{
            //    SubsystemMsg.AllMaterialRestore();
            //    return;
            //}
            Vector3 hitInfoPoint = MouseCheck.clickHitPoint;
            float distance = Vector3.Distance(Camera.main.transform.position, hitInfoPoint);
            Vector3 from = Camera.main.transform.position;
            Vector3 to = hitInfoPoint - (hitInfoPoint - from).normalized * maxDistance;

            if (distance>3.0f)
            {
                CameraAnimation.RotationScreenCenter(hitInfoPoint, 1.0f, (postion, duringTime) => {
                    Main.instance.StartCoroutine(EffectionUtility.BlurEffection(duringTime, 1, 10));
                    Camera.main.transform.DOMove(to, duringTime).OnComplete(() =>
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


