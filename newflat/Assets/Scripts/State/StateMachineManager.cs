
using Core.Common.Logging;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace State
{
    /**
     *  senter
            onenter
            oninit
          onfadeonut
            loadResouce
            onloadResouceFinish
              iset
                createEquipment
                setCurrentEquipment


        sexit
             OnExitFront();
            OnTransitionExit(OnNoTransitionExit)

    
    **/
    public sealed class StateMachineManager {

        private static ILog log = LogManagers.GetLogger("StateMachineManager");

        private readonly Dictionary<string, IState> switchStateDictionary = null;
        private readonly Dictionary<string, AppBaseState> appStateDictionary = null;

        public IState mCurrentState;

        public AppBaseState appCurrentState;
        public string currentSceneId = "-1";
        public int currentBuilderFloorGroup =0;

        public StateMachineManager() // 构造函数初始化
		{
			
			mCurrentState = null;
            appCurrentState = null;
            switchStateDictionary = new Dictionary<string, IState>();
            appStateDictionary = new Dictionary<string, AppBaseState>();

        }

        /// <summary>
        /// 浏览态和编辑态切换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetAppState<T>() where T : AppBaseState, new()
        {
            System.Type type = typeof(T);
            if (appStateDictionary.ContainsKey(type.Name))
            {
                appCurrentState = appStateDictionary[type.Name];
            }
            else
            {
                appCurrentState = new T();
                appStateDictionary.Add(type.Name, appCurrentState);
            }
            appCurrentState.Init();

        }


        /// <summary>
        /// 场景之间的切换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nextSceneid"></param>
        /// <param name="enterCallBack"></param>
        /// <param name="FloorGroup">切换楼层的时候，组编号</param>
        public void SwitchStatus<T>(string nextSceneid,System.Action enterCallBack =null, int builderFloorGroup = 0,string fullAreaBuiderId = "") where T : IState, new()
        {
           System.Type type = typeof(T);

            IState nextState;
            if (switchStateDictionary.ContainsKey(type.Name))
            {
                nextState = switchStateDictionary[type.Name];
            }
            else
            {
                nextState = new T();
                switchStateDictionary.Add(type.Name, nextState);
            }
            if(this.currentSceneId.Equals(nextSceneid) && this.currentBuilderFloorGroup == builderFloorGroup &&  !(nextState is RoomState))
            {
                log.Debug("switch object is same");
                return;
            }
            SceneData.SetCurrentData<T>(nextSceneid, builderFloorGroup, fullAreaBuiderId);
            //显示标题
            NavigationTitle.instance.ShowTitle(nextSceneid);
            //if (nextState == mCurrentState)
            //{
            //    log.Debug("Switch");
            //    nextState.Switch(nextSceneid);
            //    mCurrentState = nextState;
            //    this.currentSceneId = nextSceneid;
                
            //    return;
            //}
            List<Object3dItem> list = SceneData.GetCurrentData();
            if (list == null || list.Count==0)
            {
                log.Error("scene resouce is empty");
                return;
            }

            if (mCurrentState==null)
	        {
                nextState.Enter(nextSceneid, enterCallBack);
                mCurrentState  = nextState;
	        }
	        else
	        {
                
                mCurrentState.Exit(nextState, nextSceneid, ()=>{
                    mCurrentState = nextState;
                    nextState.Enter(nextSceneid, enterCallBack);
                   
	        	   });
	        }
            this.currentSceneId = nextSceneid;
            this.currentBuilderFloorGroup = builderFloorGroup;



        }
        /// <summary>
        /// 定位设备
        /// </summary>
        private string currentEquipmentid = "-1";
        public void LocateEquipment(string id,string parentid)
        {
            if(id.Equals(currentEquipmentid))
            {
                log.Debug("equipment is same" + "id="+id);
                return;
            }
            System.Type type = SceneData.GetCurrentState(parentid);
            BaseState bs = null;
            //Debug.Log(type.Name.ToString());
            if(mCurrentState!=null)
            {
                Debug.Log(mCurrentState.GetType().ToString());
            }

            if (mCurrentState != null && (mCurrentState.GetType().ToString().Contains(type.Name.ToString()))) 
            {
                bs = mCurrentState as BaseState;
                bs.LocateEquipment(id);
            }
            else
            {
                switch (type.Name.ToString())
                {
                    case "AreaState":
                        {

                            SwitchStatus<AreaState>(parentid, () => {
                                bs = mCurrentState as BaseState;
                                bs.LocateEquipment(id);
                            });
                            break;
                        }

                        
                    case "FloorState":
                        {
                            SwitchStatus<FloorState>(parentid, () => {
                                bs = mCurrentState as BaseState;
                                bs.LocateEquipment(id);
                            });
                            break;
                        }
                    case "RoomState":
                        {
                            SwitchStatus<RoomState>(parentid, () => {
                                bs = mCurrentState as BaseState;
                                bs.LocateEquipment(id);
                            });
                            break;
                        }

                    
                }
            }

            currentEquipmentid = id;


        }
		
		public void OnUpdate()
	     {   if (mCurrentState != null)
	         {
	             mCurrentState.Update();
	         }
            if(appCurrentState!=null)
            {
                appCurrentState.Update();
            }
	     }
	     public void OnFixedUpdate()
	     {
	         if (mCurrentState != null)
	         {
	             mCurrentState.FixedUpdate();
	         }
             
	     }
	     public void OnLateUpdate()
	     {
	         if (mCurrentState != null)
	         {
	             mCurrentState.LateUpdate();
	        }
	     }
	}

}
