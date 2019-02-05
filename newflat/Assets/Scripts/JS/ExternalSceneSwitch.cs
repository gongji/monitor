using State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalSceneSwitch:MonoSingleton<ExternalSceneSwitch>  {

    //swtich
    public   void SwitchScene(string type,string sceneid)
    {

        Debug.Log(String.Format("type {0}, sceneid {1}", type, sceneid));
        switch (type)
        {
            case "0":

                Debug.Log("open area");
                SceneJump.JumpFirstPage();
                break;
            case "1":
                Debug.Log("open BuilderState");
                Main.instance.stateMachineManager.SwitchStatus<BuilderState>(sceneid);
                break;
            case "2":
                Debug.Log("open FloorState");
                Main.instance.stateMachineManager.SwitchStatus<FloorState>(sceneid);
                break;
            case "3":
                Debug.Log("open RoomState");
                Main.instance.stateMachineManager.SwitchStatus<RoomState>(sceneid);
                break;
            case "4":
                Debug.Log("open ColorAreaState");
                Main.instance.stateMachineManager.SwitchStatus<ColorAreaState>(sceneid);
                break;
            case "5":
                Debug.Log("open FullAreaState");
                Main.instance.stateMachineManager.SwitchStatus<FullAreaState>("-1", null, 0, sceneid);
                break;
        }
    }

    //save
    public  void SaveSwitchData(string type, string sceneid)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
       GameObject.FindObjectOfType<JSCall>()._SaveSwitchData(type,sceneid);
#endif

    }

}
