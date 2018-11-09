using Battlehub.UIControls;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNavigationMeditor : Mediator
{

    public TreeNavigationMeditor(GameObject root) : base(NAME)
    {

        root.GetComponent<TreeViewControl>().callBack= GetBrowserData;

    }

    private void GetBrowserData()
    {
        SendNotification(TreeNotifications.BrowserTreeData, null);
    }

    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
        list.Add(TreeNotifications.UpdateBrowserTreeData);

        return list;
    }

    public override void HandleNotification(INotification notification)
    {
        //判断通知的信息，并从中拿到数据，进行显示
        switch (notification.Name)
        {


            case TreeNotifications.UpdateBrowserTreeData:
                //..
               // root.GetComponent<TreeViewControl>()
             break;

        //    break;
        default:
                break;

        }
    }
}
