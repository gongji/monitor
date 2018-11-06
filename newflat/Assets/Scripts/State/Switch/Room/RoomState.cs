using UnityEngine;
using System.Collections;
using DG.Tweening;
using Core.Common.Logging;

namespace State
{
	public class RoomState :BaseState,IState {

        private static ILog log = LogManagers.GetLogger("RoomState");
        private bool isEquipment = false;
        #region enter
        public void Enter(string id, System.Action callBack)
		{
            log.Debug ("RoomState Enter");
			this.currentId =id;
            this.enterCallBack = callBack;
            OnEnter ();
		}
        
    	
    	protected override void OnInit()
    	{
            base.OnInit();
            log.Debug("RoomState OnInit");
    	}

		protected override void OnLoadResouceFinish()
		{
            
            log.Debug("RoomState OnLoadResouce finish");
            if (baseSet == null)
            {
                baseSet = new RoomSet();
            }
            InitSet();
        }



        #endregion

        #region eixt


        public void Exit(IState nextState, string nextid, System.Action callBack)
        {
            log.Debug("RoomState Exit");
           
          //  Debug.Log("LocateBack.instance.isLocate="+ LocateBack.instance.isLocate);
            if ((nextState is FloorState && AppInfo.currentView == ViewType.View3D))
            {
                OnExitFront();
                OnTransitionExit(nextState, nextid, () =>
                {
                    OnFadeIn();
                    if (callBack != null)
                    {
                        callBack.Invoke();
                    }
                    EquipmentSet.HideCurrentEquipment();
                });

            }
            else
            {
                OnExitFront();
                EquipmentSet.HideCurrentEquipment();
                //OnHide ();
                OnNoTransitionExit(nextState, nextid);
                OnFadeIn();
                if (callBack != null)
                {
                    callBack.Invoke();
                }
            }
        }

        protected override void OnExitFront()
        {
            base.OnExitFront();
            isEquipment = false;
        }

        protected override void OnTransitionExit(IState  nextState,string nextid,System.Action callBack)
		{
            log.Debug("roomState OnTransitionExit");

            if (baseSet != null)
            {
                baseSet.Exit(nextid, callBack);
            }
		}

        protected override void OnNoTransitionExit(IState nextState, string nextid)
        {
            log.Debug("roomState OnNoTransitionExit");

            if (baseSet != null)
            {
                baseSet.Exit(nextid);
            }
        }

        #endregion
        public void LocateEquipment(string id)
        {
            log.Debug("view equipment id="+id);
            ViewEquipmentInfo.Instance.Init(id, (baseSet as RoomSet).CameraLocateEquipment);
            isEquipment = true;
        }

        public void Update()
        {

        }
        public void FixedUpdate()
        {

        }
        public void LateUpdate()
        {

        }

        public void GUI()
        {

        }


        public void Switch(string id)
        {
            log.Debug("roomState Switch");
            if(!isEquipment && id.Equals(currentId))
            {
                log.Debug("room is same");
                return;
            }
            this.enterCallBack = null;
            this.currentId = id;
            DisableCamera();
            ViewEquipmentInfo.Instance.RemoveAllResouce(true);
            OnEnter();
           
        }


    }


}
