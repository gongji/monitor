using JetBrains.Annotations;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class AppFacade : Facade, IFacade
{
    private static AppFacade instance=null;

    
    public static AppFacade GetInstance()
    {
        if(instance==null)
            instance=new AppFacade();
        return instance;
    }

    protected override void InitializeController()
    {
        base.InitializeController();
        RegisterCommand(TreeNotifications.BrowserTreeData, typeof(BrowserDataCommand));
       
    }

    protected override void InitializeModel()
    {
        base.InitializeModel();
        ////Debug.Log("2222222222222");
        RegisterProxy(new TreeProxy());
    }

  
    public void StartUp(GameObject root)
    {
       // Debug.Log("333333333");
        //RegisterMediator(new StartMediator(root));
    }
}