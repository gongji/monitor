using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定时请求报警数据
/// </summary>
public class SceneAlarmTimer :MonoSingleton<SceneAlarmTimer>,ITimer {

    private int time = 5;
    public int Time
    {
        get
        {
            return time;
        }

        set
        {
            time = value;
        }

       
    }
    public void StartTimer()
    {
        StartCoroutine(Start());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            DoTimer();
            yield return new WaitForSeconds(time);
        }
    }

    public void DoTimer()
    {
        List<SceneAlarmBase> list = GetCurrrentAlarmScene();
        if(list != null && list.Count>0)
        {
            List<string> sceneids = new List<string>();
            foreach(SceneAlarmBase item in list)
            {
                if(!string.IsNullOrEmpty(item.sceneId))
                {
                    sceneids.Add(item.sceneId);
                }
                
            }
            if(sceneids.Count==0)
            {

                return;
            }
            SceneAlamProxy.GetSceneAlarmStateList((success) => {

                List<SceneAlarmItem> sceneAlarmItems = Utils.CollectionsConvert.ToObject<List<SceneAlarmItem>>(success);

                foreach(SceneAlarmItem item in sceneAlarmItems)
                {
                    if(SceneData.sceneAlarmDic.ContainsKey(item.id))
                    {
                        SceneAlarmBase sab = SceneData.sceneAlarmDic[item.id];
                        if(item.state == 1)
                        {
                            sab.Alarm();
                        }
                        else
                        {
                            sab.Restore();
                        }
                    }
                }
            }, sceneids);


        }
    }

    /// <summary>
    /// 获取当前场景的报警场景列表
    /// </summary>
    /// <returns></returns>
    public List<SceneAlarmBase> GetCurrrentAlarmScene()
    {
        IState istate = Main.instance.stateMachineManager.mCurrentState;
        List<SceneAlarmBase> sceneAlarmBases = new List<SceneAlarmBase>(GameObject.FindObjectsOfType<SceneAlarmBase>()); ;

        //全景
        if(istate is FullAreaState)
        {
            return null;
        }
        //园区状态或者房间状态
        else if(istate is AreaState || istate is RoomState)
        {
            return sceneAlarmBases;
        }
        //楼层
        else if(istate is FloorState)
        {
            for(int i= sceneAlarmBases.Count-1; i>=0;i--)
            {
                if(sceneAlarmBases[i].GetComponent<Object3DElement>().type == DataModel.Type.Floor)
                {
                    sceneAlarmBases.RemoveAt(i);
                }
            }
        }
        //大楼
        else if(istate is BuilderState)
        {
            for (int i = sceneAlarmBases.Count - 1; i >= 0; i--)
            {
                if (sceneAlarmBases[i].GetComponent<Object3DElement>().type == DataModel.Type.Room)
                {
                    sceneAlarmBases.RemoveAt(i);
                }
            }
        }

        return null;
    }
}
