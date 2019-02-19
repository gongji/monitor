using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WqMouserOver : BaseMouseOver {
    private void Start()
    {
        DOVirtual.DelayedCall(2.0f, () => {
            gameObject.layer = LayerMask.NameToLayer("equipment");
        });
    }

    protected void OnMouseDown()
    {
        if(string.IsNullOrEmpty(sceneid))
        {
            sceneid = transform.parent.GetComponent<Object3DElement>().sceneId;
        }
        Main.instance.stateMachineManager.SwitchStatus<FullAreaState>("-1", null, 0, sceneid);
    }


}
