
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
            if(this.currentSceneId.Equals(nextSceneid) && this.currentBuilderFloorGroup == builderFloorGroup &&  
                
                !(nextState is RoomState) && !(nextState is ColorAreaState) && !(nextState is FullAreaState))
            {
                log.Debug("switch object is same");
                return;
            }
            SceneData.SetCurrentData<T>(nextSceneid, builderFloorGroup, fullAreaBuiderId);

            if (nextState is FullAreaState || nextState is ColorAreaState)
            {
                NavigationTitle.instance.ShowTitle(fullAreaBuiderId);
            }
            else

            {
                NavigationTitle.instance.ShowTitle(nextSceneid);
            }

            
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

                string _nextSceneid = nextSceneid;
                if(nextState is FullAreaState)
                {
                    _nextSceneid = fullAreaBuiderId;
                }
                mCurrentState.Exit(nextState, _nextSceneid, ()=>{
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
       
        public void StateReset()
        {
            currentEquipmentid = "-1";
        }
        public void LocateEquipment(string equipmentId,string sceneId)
        {
            
            Object3dItem currentScene = SceneContext.currentSceneData;
            Object3dItem locateScene = SceneData.FindObjUtilityect3dItemById(sceneId);
            if((mCurrentState!=null && mCurrentState is FloorState) && locateScene.parentsId.Equals(currentScene.id))
            {
                sceneId = currentScene.id;
            }
            //if (equipmentId.Equals(currentEquipmentid))
            //{
            //    log.Debug("equipment is same" + "id="+ equipmentId);
            //    return;
            //}
            DataModel.Type type = SceneData.FindObjUtilityect3dItemById(sceneId.Trim()).type;
            //if (mCurrentState!=null && mCurrentState.GetType()!=null)
            //{
            //    //Debug.Log(mCurrentState.GetType().ToString());
            //}
            BaseState bs = null;
            if (mCurrentState != null && SceneContext.currentSceneData!=null && 
                (SceneContext.currentSceneData.id.Equals(sceneId.Trim()))) 
            {
               // Debug.Log(SceneContext.currentSceneData.id);
                bs = mCurrentState as BaseState;
                bs.LocateEquipment(equipmentId);
            }
            else
            {
                switch (type)
                {
                    case DataModel.Type.Area:
                        {

                            SwitchStatus<AreaState>(sceneId, () => {
                                bs = mCurrentState as BaseState;
                                bs.LocateEquipment(equipmentId);
                            });
                            break;
                        }

                        
                    case DataModel.Type.Floor:
                        {
                            SwitchStatus<FloorState>(sceneId, () => {
                                bs = mCurrentState as BaseState;
                                bs.LocateEquipment(equipmentId);
                            });
                            break;
                        }
                    case DataModel.Type.Room:
                        {
                            SwitchStatus<RoomState>(sceneId, () => {
                                bs = mCurrentState as BaseState;
                                bs.LocateEquipment(equipmentId);
                            });
                            break;
                        }

                    
                }
            }

            currentEquipmentid = equipmentId;


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
