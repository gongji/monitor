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

                SceneJump.JumpFirstPage();
                break;
            case "1":
                 Main.instance.stateMachineManager.SwitchStatus<BuilderState>(sceneid);
                break;
            case "2":
                Main.instance.stateMachineManager.SwitchStatus<FloorState>(sceneid);
                break;
            case "3":
                Main.instance.stateMachineManager.SwitchStatus<RoomState>(sceneid);
                break;
            case "4":
                Main.instance.stateMachineManager.SwitchStatus<ColorAreaState>(sceneid);
                break;
            case "5":
                Main.instance.stateMachineManager.SwitchStatus<FullAreaState>("-1", null, 0, sceneid);
                break;


        }
    }

    //save
    public  void SaveSwitchData(string type, string sceneid)
    {
        GameObject.FindObjectOfType<JSCall>()._SaveSwitchData(type,sceneid);
        
    }

}
