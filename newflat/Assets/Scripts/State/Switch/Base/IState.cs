using UnityEngine;
using System.Collections;
namespace State
{
	public interface  IState {
		void Enter(string id, System.Action callBack);
		void Exit(IState  nextState,string nextid,System.Action callBack);
		void Switch(string id);
		void Update();
		void FixedUpdate();
    	void LateUpdate();
    	void GUI();	
	}
}


