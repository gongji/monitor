using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeProxy :  Proxy
{
    public new const string NAME = "TreeProxy";

	
    public void GetBrowserData()
    {
        EquipmentData.GetEquipmentListByParentId("", (list) =>
        {
            //SetEquipmentData(datas);
            //TreeView.Items = datas;

           
            SendNotification(TreeNotifications.UpdateBrowserTreeData);
        
        });
    }
}
