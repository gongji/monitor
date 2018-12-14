using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEquipmentControl : MonoBehaviour {

    public EquipmentItem equipmentItem =new EquipmentItem();

    private bool isAlarm = false;
    public virtual void Alarm(){
        if(isAlarm)
        {
            return;
        }
        isAlarm = true;
        UpdateShow();
    }

    public virtual void CancleAlarm() {
        if(!isAlarm)
        {
            return;
        }
        isAlarm = false;
        UpdateShow();
    }

    private bool isSelect = false;
    public virtual void SelectEquipment() {

        if(isSelect)
        {
            return;
        }
        isSelect = true;
        UpdateShow();
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
       // SelectEffection();
       // LocateBack.instance.Show();
    }


    private GameObject selectArrow = null;
    private  void CreateSelectEffection()
    {
       
        selectArrow = TransformControlUtility.CreateItem("equipment/select", null);

        selectArrow.transform.position = transform.position + 
            Vector3.up * GetComponentInChildren<BoxCollider>().bounds.size.y + Vector3.up * 0.05f;
        
        selectArrow.transform.SetParent(transform.parent);
        EffectionUtility.PlayDotweenAphlaFlash(gameObject,Color.blue, "_Color");
       
    }


    private GameObject alarmArrow;
    private void CreateAlarmEffection()
    {
        alarmArrow = TransformControlUtility.CreateItem("equipment/alarm", null);
        alarmArrow.transform.position = transform.position +
            Vector3.up * GetComponentInChildren<BoxCollider>().bounds.size.y + Vector3.up * 0.05f;
        alarmArrow.transform.SetParent(transform.parent);

        EffectionUtility.PlayDotweenAphlaFlash(gameObject, Color.red, "_Color");
    }

    public virtual void CancelEquipment()
    {
        if (!isSelect)
        {
            return;
        }

        isSelect = false;
        UpdateShow();
       
        //LocateBack.instance.Hide();
    }

    private void CancelEffection(GameObject effectionObject)
    {
        if (effectionObject != null)
        {
            GameObject.Destroy(effectionObject);
        }
        DoTweenAphlaFlashEffection afe = GetComponent<DoTweenAphlaFlashEffection>();
        if (afe != null)
        {
            afe.StopAllTask();
        }
    }

 
    public virtual void OnMouseClick() { }

   

    private void UpdateShow()
    {
        CancelEffection(selectArrow);
        CancelEffection(alarmArrow);

        if (isSelect)
        {
            
            CreateSelectEffection();
        }
        else
        {
            if(isAlarm)
            {
                CreateAlarmEffection();
            }
        }
    }
   
}
