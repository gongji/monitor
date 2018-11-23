using UnityEngine;
using System.Collections;
using DG.Tweening;
using DataModel;
using System.Collections.Generic;
using Core.Common.Logging;

namespace State
{
	public class FloorState :BaseState,IState {

        private static ILog log = LogManagers.GetLogger("FloorState");
        #region enter
        public void Enter(string id,System.Action callBack)
		{
            log.Debug("FloorState Enter");
			this.currentId =id;
            this.enterCallBack = callBack;
            OnEnter();
		}
       
    	
    	protected override void OnInit()
    	{
            base.OnInit();
            log.Debug("FloorState OnInit");
    	}
		protected override void OnLoadResouceFinish()
		{
           
            log.Debug("FloorState OnLoadResouce finish");

            if (baseSet == null)
            {
                baseSet = new FloorSet();
            }
            InitSet();
        }


        #endregion


        #region exit
        public void Exit(IState nextState, string nextid, System.Action callBack)
        {
            log.Debug("FloorState exit");

            if ((nextState is RoomState))
            {
                OnExit<RoomState>(nextState, nextid, () => {

                    OnFadeIn();
                    if (callBack != null)
                    {
                        callBack.Invoke();
                    }
                    EquipmentSet.HideCurrentEquipment();
                }, true);
            }
            else 
            {
                if (nextState is BuilderState)
                {
                    OnFadeIn();
                }
                OnExitFront();
            
                EquipmentSet.HideCurrentEquipment();
                OnNoTransitionExit(nextState, nextid);
                if (callBack != null)
                {
                    callBack.Invoke();
                }
            }
            
        }
        protected override void OnExitFront()
        {
            base.OnExitFront();
           // EquipmentSet.HideCurrentEquipment();
        }

        protected override void OnTransitionExit(IState  nextState,string nextid, System.Action callBack)
		{
            if(baseSet != null)
            {
                baseSet.Exit(nextid, callBack);
            }
		}

        protected override void OnNoTransitionExit(IState nextState, string nextid)
        {
            if (baseSet != null)
            {
                baseSet.Exit(nextid);
            }
        }

        #endregion


        public void Switch(string id)
        {
            log.Debug("FloorState Switch");
            if (id.Equals(currentId))
            {
                return;
            }
            ///DisableCamera();
            this.currentId = id;
            OnEnter();

        }


        //public   void LocateEquipment(string id)
        //{
        //   // EquipmentInfoView(id);
        //}



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

    }
}
