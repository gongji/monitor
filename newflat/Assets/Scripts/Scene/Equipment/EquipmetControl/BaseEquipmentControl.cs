using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEquipmentControl : MonoBehaviour {

    public EquipmentItem equipmentItem =new EquipmentItem();

    public virtual void alarm(){}

    public virtual void Locate() {


        BoxCollider collider = transform.GetComponentInChildren<BoxCollider>();

        Vector3 upPostion = transform.position + transform.up * collider.bounds.size.y * 1.5f ;

       // Vector3 targetPostion = upPostion + transform.forward * collider.bounds.size.z * 2.0f;
        Vector3 targetPostion = upPostion + transform.forward * collider.bounds.size.z * 4.0f;
        Vector3 center = transform.position + transform.up * collider.bounds.size.y * 0.5f;
        Vector3 dir = center   - targetPostion;
        Quaternion quaternion = Quaternion.LookRotation(dir, transform.up);

        CameraAnimation.CameraMove(Camera.main, targetPostion, quaternion.eulerAngles, 1.0f,null);
        //Camera.main.transform.position = targetPostion;
        //Camera.main.transform.localRotation = quaternion;
        SelectEquipment();
       // LocateBack.instance.Show();
    }


    private GameObject selectArrow = null;
    private  void SelectEquipment()
    {
        selectArrow = TransformControlUtility.CreateItem("equipment/select", null);

        selectArrow.transform.position = transform.position + Vector3.up * GetComponentInChildren<BoxCollider>().bounds.size.y + Vector3.up * 0.05f;
        selectArrow.transform.localScale = Vector3.one * 0.1f;

        GetComponent<Object3DElement>().SelectHigh(true);
        //AphlaFlashEffection afe = GetComponent<AphlaFlashEffection>();
        //if (afe == null)
        //{
        //    afe = gameObject.AddComponent<AphlaFlashEffection>();
        //}
    }

    public virtual void CancelEquipment()
    {

        if (selectArrow != null)
        {
            GameObject.Destroy(selectArrow);
        }
        AphlaFlashEffection afe = GetComponent<AphlaFlashEffection>();
        if (afe != null)
        {
            afe.StopAllTask();
        }
        //LocateBack.instance.Hide();
    }

    protected bool isShowTips = true;

    public void SetTipsShow(bool isEnable)
    {
        isShowTips = isEnable;
    }
    public void OnMouseClick()
    {
        // Main.instance.stateMachineManager.ViewEquipment(equipmentItem.id, equipmentItem.parentid);

        ShowTestPoint.Show(equipmentItem.name, equipmentItem.id);
    }


}
