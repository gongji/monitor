using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class BrowserDataCommand : SimpleCommand
{
    public new const string NAME = "BrowserDataCommand";

    public override void Execute(INotification notification)
    {

        TreeProxy treeProxy = (TreeProxy)AppFacade.GetInstance().RetrieveProxy(TreeProxy.NAME);
        treeProxy.GetBrowserData();
    }
}