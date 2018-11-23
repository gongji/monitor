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

        protected BaseSet baseSet = null;

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
                LoadResouce();
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

        protected void LoadResouce()
        {
            log.Debug("strat load resouce");
            curentDataList = SceneData.GetCurrentData();
            SceneContext.sceneData = curentDataList;
            var result = from o in curentDataList where o.isDownFinish == false select o;
         //  EquipmentData.GetCurrentEquipmentData(currentId, (list) => {

            if ((result!=null && result.Count() > 0))
            {
                DownLoader.Instance.StartDownLoad(result.ToList<Object3dItem>(), null, () =>
                {
                    OnLoadResouceFinish();
                });
            }
            //不加载资源
            else
            {
                OnLoadResouceFinish();
            }

          // });



        }

        protected void InitSet()
        {
            baseSet.Enter(curentDataList, () => {

                EnableEventSystem(true);
                EquipmentSet.CreateEquipment();
                if (enterCallBack != null)
                {
                    enterCallBack.Invoke();
                    
                }
            });
        }

        protected virtual void OnLoadResouceFinish()
        {

        }

        protected virtual void OnExitFront()
        {
            EquipmentSet.HideCurrentEquipmentTips();
            ViewEquipmentInfo.Instance.RemoveAllResouce();
            DisableCamera();
            if(baseEquipmentControl!=null)
            {
                baseEquipmentControl.CancelEquipment();
            }
            EnableEventSystem(false);
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
                baseEquipmentControl.Locate();
               // LocateBack.instance.SetEquipment(baseEquipmentControl);
            }
        }


    }
}

