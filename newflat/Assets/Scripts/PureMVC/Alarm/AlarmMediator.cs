using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmMediator : Mediator
{

    public new const string NAME = "AlarmMediator";
    // Use this for initialization
    public AlarmMediator(GameObject root) : base(NAME)
    {
       

    }

    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
        
        return list;
    }

    public override void HandleNotification(INotification notification)
    {
        //判断通知的信息，并从中拿到数据，进行显示
        switch (notification.Name)
        {
            //case UserNotifications.LevelChange:
               
            //    break;
            default:
                break;

        }
    }
}
