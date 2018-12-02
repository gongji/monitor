using UnityEngine;
using System.Collections;
using DG.Tweening;
using DataModel;

namespace State
{
	public class BuilderState :BaseState,IState {

        #region enter
        public void Enter(string id, System.Action callBack)
		{
			Debug.Log ("BuilderState Enter");
			this.currentId =id;
            this.enterCallBack = callBack;
            OnEnter();
		}
       
    	
    	protected override void OnInit()
    	{
            base.OnInit();
            Debug.Log ("BuilderState OnInit");
           // Camera.main.transform.GetComponent<CameraObjectController>().enabled = false;
            Camera.main.transform.position += Camera.main.transform.position + Vector3.right * 10000;
        }

        protected override void OnLoadResouceFinish()
		{
            Debug.Log ("BuilderState OnLoadResouce finish");

            if(baseSet == null)
            {
                baseSet = new BuilderSet();
            }
            InitSet();


        }

        #endregion


        #region exit

        public void Exit(IState nextState, string nextid, System.Action callBack)
        {
            Debug.Log("BuilderState exit");

            OnExit<FloorState>(nextState, nextid, () => {

                if (callBack != null)
                {
                    callBack.Invoke();
                }
            }, false);
        }

        protected override void OnExitFront()
        {
            base.OnExitFront();
           
        }


        protected override void OnTransitionExit(IState  nextState,string nextid,System.Action callBack)
		{
            if(baseSet != null)
            {
                baseSet.Exit(nextid, callBack);
            }
           
        }

        protected override void OnNoTransitionExit(IState nextState, string nextid) {

            if (baseSet != null)
            {
                baseSet.Exit(nextid);
            }
        }


        #endregion

        public void Switch(string id)
        {
            Debug.Log("BuilderState Switch");
            if (id.Equals(currentId))
            {

                return;
            }
            this.currentId = id;
        }

        //public void LocateEquipment(string id)
        //{

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
