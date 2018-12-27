using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EquipmentShowMenu : MonoBehaviour {

   //设备id
    public string equipmentId;
    public string equipmentName;
    private float scaleTime = 0.3f;
    void Start () {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, scaleTime);
	}
	

    public void TestPointShow()
    {
        transform.DOScale(Vector3.zero, scaleTime).OnComplete(()=> {

            ShowTestPoint.Show(equipmentName, equipmentId);
            GameObject.Destroy(gameObject, 0.1f);
        });
       
    }

    public void TestPointControl()
    {
        transform.DOScale(Vector3.zero, scaleTime).OnComplete(() => {
           
            SetControlPoint.Show(equipmentName, equipmentId);
            GameObject.Destroy(gameObject, 0.1f);
        });
    }
}
