using Core.Common.Logging;
using State;
using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 平台
/// </summary>
public enum BRPlatform
{
    /// <summary>
    /// 编辑器模式
    /// </summary>
    Editor,
    /// <summary>
    /// 浏览模式
    /// </summary>
    Browser,
    None
}

public enum ViewType
{
    View3D,
    View2D

}
/// <summary>
/// 管理应用全局信息
/// 1.部分信息由本地csv,xml控制
/// 2.部分信息由类封装
/// </summary>
public static class AppInfo
{
    private static ILog log = LogManagers.GetLogger("AppInfo");

    /// <summary>
    /// 当前平台
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

   
    /// <summary>
    /// 获取当前的状态
    /// </summary>
    public static IState GetCurrentState
    {
        get
        {
            return Main.instance.stateMachineManager.mCurrentState;
        }
    }

    public static ViewType currentView = ViewType.View3D;

}
