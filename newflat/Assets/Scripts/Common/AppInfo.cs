using Core.Common.Logging;
using State;
using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// platForm
/// </summary>
public enum BRPlatform
{
    /// <summary>
    /// edit mode
    /// </summary>
    Editor,
    /// <summary>
    /// brwser mode
    /// </summary>
    Browser,
    None
}

public enum ViewType
{
    View3D,
    View2D

}


public static class AppInfo
{
    private static ILog log = LogManagers.GetLogger("AppInfo");

    /// <summary>
    /// current platform
    /// </summary>
    public static BRPlatform Platform
    { get
        {
            int flag = Convert.ToInt16(Config.parse("runMode"));
            if (flag == 1)
            {
                return BRPlatform.Editor;
            }
            else if (flag == 0)
            {
                return BRPlatform.Browser;
            }else
            {
                return BRPlatform.None;
            }
            
        }
    }

   

    public static IState GetCurrentState
    {
        get
        {
            return Main.instance.stateMachineManager.mCurrentState;
        }
    }

    public static ViewType currentView = ViewType.View3D;

}
