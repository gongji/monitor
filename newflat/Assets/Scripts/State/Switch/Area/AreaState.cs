using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using SystemCore.Task;
using DataModel;
using Core.Common.Logging;

namespace State
{
	public class AreaState :BaseState,IState {

        private ILog log = LogManagers.GetLogger("AreaState");

        #region enter
        public void Enter(string id, System.Action callBack)
		{
            log.Debug ("area Enter");
			this.currentId =id;
            this.enterCallBack = callBack;
            OnEnter ();

		}

     
    	protected override void OnInit()
    	{
            base.OnInit();
            log.Debug ("area OnInit");
            //CameraViewProxy.GetCameraView(Constant.AreaViewName,(postion,angel,isExsits) =>
            //{
            //    if(isExsits)
            //    {
            //        Camera.main.transform.position = postion;
            //        Camera.main.transform.eulerAngles = angel;
            //    }


            //},()=> {

            //    log.Debug("GetCameraView  isError");
            //});
        }
        protected override void OnLoadSceneResouceFinish()
		{
            
            Debug.Log ("area OnLoadResouce finish");

            if (baseSet == null)
            {
                baseSet = new AreaSet();
            }
            InitSet();
        }

        #endregion


        #region eixt
        public void Exit(IState nextState, string nextid, System.Action callBack)
        {
            log.Debug("area exit");
            OnExit<FullAreaState>(nextState, nextid, () => {
                callBack.Invoke();
            });

        }

        protected override void OnExitFront()
        {
            base.OnExitFront();
           
        }

        protected override void OnTransitionExit(IState  nextState,string nextid,System.Action callBack)
		{
            Debug.Log(" area OnTransitionAnimation");
            
           if(baseSet != null)
            {
                baseSet.Exit(nextid,callBack);
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

        //public void LocateEquipment(string id)
        //{
        //    EquipmentInfoView(id);
        //}

        public virtual void Switch(string id)
        {
           
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

    }


}
