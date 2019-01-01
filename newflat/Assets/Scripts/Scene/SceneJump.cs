using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SceneJump  {

	
    /// <summary>
    /// 调到地形的主页
    /// </summary>
    public static void JumpFirstPage()
    {
        string sceneid = SceneData.GetIdByNumber(Constant.Main_dxName);
        Main.instance.stateMachineManager.SwitchStatus<AreaState>(sceneid);
    }
}
