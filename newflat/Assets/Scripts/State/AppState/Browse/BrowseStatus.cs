using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlehub.UIControls;

public class BrowseStatus : AppBaseState
{

    public BrowseStatus()
    {

    }

    public override void Init()
    {
        BrowserUI.Create();
        TreeViewControl.Instance.SetBrowserData();
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
    }

    public override void OnGUI()
    {

    }

    public override void UpdateUI()
    {
    }

    
}


