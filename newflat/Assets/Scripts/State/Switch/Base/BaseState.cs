using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataModel;
using System.Linq;
using Core.Common.Logging;
using UnityEngine.EventSystems;

namespace State
{
	public  abstract class BaseState   {

        private static ILog log = LogManagers.GetLogger("BaseState");
		protected string currentId = string.Empty;
		protected string nextId =string.Empty;
        protected System.Action enterCallBack;

        public BaseSet baseSet = null;

        protected List<Object3dItem> curentDataList = null;


        protected  virtual void OnFadeIn()
		{
			FadeManager.Instance.FadeIn();
		}
		
		protected  virtual void OnFadeOut(System.Action callBack)
		{
            log.Debug("OnFadeOut");
			FadeManager.Instance.FadeOut(callBack);
		}

        
		protected void OnEnter()
		{
			OnInit ();
			OnFadeOut (()=> {
                LoadSceneResouce();
            });
            
		}
        protected virtual void OnInit()
        {
            EnableEventSystem(false);
        }

        private void EnableEventSystem(bool isEnable)
        {
           
            EventSystem eventSystem = GameObject.FindObjectOfType<EventSystem>();
            eventSystem.enabled = isEnable;
        }

        protected void LoadSceneResouce()
        {
            log.Debug("strat load resouce");
            curentDataList = SceneData.GetCurrentData();
            SceneContext.sceneDataList = curentDataList;
            var result = from o in curentDataList where o.isDownFinish == false select o;
         //  EquipmentData.GetCurrentEquipmentData(currentId, (list) => {

            if ((result!=null && result.Count() > 0))
            {
                DownLoader.Instance.StartSceneDownLoad(result.ToList<Object3dItem>(), () =>
                {
                    OnLoadSceneResouceFinish();
                });
            }
            //不加载资源
            else
            {
                OnLoadSceneResouceFinish();
            }

          // });



        }

        protected void InitSet()
        {
            baseSet.Enter(curentDataList, () => {
                InitCreateEquipment.CreateEquipment(()=> {
                    EnableEventSystem(true);
                    if(AppInfo.Platform == BRPlatform.Browser)
                    {
                        SceneAlarmTimer.Instance.StartTimer();
                    }
                   
                    if (enterCallBack != null)
                    {
                        enterCallBack.Invoke();

                    }

                });
                
            });
        }

        protected virtual void OnLoadSceneResouceFinish()
        {

        }

        protected virtual void OnExitFront()
        {
            InitCreateEquipment.HideCurrentEquipmentTips();
            ViewEquipmentInfo.Instance.RemoveAllResouce();
            DisableCamera();
            if(baseEquipmentControl!=null)
            {
                baseEquipmentControl.CancelEquipment();
            }
            EnableEventSystem(false);
            EquipmentData.SetAllEquipmentParentEmpty();
            if(AppInfo.Platform == BRPlatform.Browser)
            {
                SceneAlarmTimer.Instance.StopTimer();
            }
          
        }



        protected void DisableCamera()
        {
            //if (Camera.main.GetComponent<CameraRotatoAround>() != null)
            //{
            //    Camera.main.GetComponent<CameraRotatoAround>().SetEnable(false);
            //}
        }


        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="nextState">Next state.</param>
        /// <param name="nextid">下一个对象的标识id</param>
        /// <param name="callBack">Call back.</param>
        /// <param name="isTransitionFade">退出时候是否使用屏幕切换</c> is transition fade.</param>
        /// <typeparam name="T">下一个退出的状态</typeparam>
        protected void OnExit<T>(IState  nextState,string nextid,
			System.Action callBack,bool isTransitionFade = true)
		{
            OnExitFront();
			if (nextState is T && AppInfo.currentView == ViewType.View3D) {
                OnTransitionExit(nextState, nextid, () => {
					//OnHide ();
					if(isTransitionFade)
					{
						OnFadeIn();
					}

					if(callBack!=null)
					{
						callBack.Invoke();
					}
				});
			} else {
				//OnHide ();
				//OnFadeIn();
                OnNoTransitionExit(nextState, nextid);

                if (callBack!=null)
				{
					callBack.Invoke();
				}
			}
		}


        protected virtual void OnTransitionExit(IState nextState, string nextid, System.Action callBack) { }
        protected virtual void OnNoTransitionExit(IState nextState, string nextid) { }


        public BaseEquipmentControl baseEquipmentControl = null;
        public virtual  void LocateEquipment(string id)
        {
            log.Debug("定位设备"+id);

            if(AppInfo.currentView == ViewType.View2D)
            {

                return;
            }


            if(baseEquipmentControl!=null)
            {
                baseEquipmentControl.CancelEquipment();
            }
           // Camera.main.GetComponent<CameraRotatoAround>().SetEnable(false);
            GameObject equipmentObject = EquipmentData.FindGameObjectById(id);
            baseEquipmentControl = equipmentObject.GetComponent<BaseEquipmentControl>();
            if (equipmentObject!=null && baseEquipmentControl != null)
            {
                baseEquipmentControl.SelectEquipment();
               // LocateBack.instance.SetEquipment(baseEquipmentControl);
            }
        }


    }
}

