using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
public class AppBaseState
{
    public virtual void Init()
    {

    }
    protected Transform hitTransform = null;
    public virtual void Update()
    {
         EquipmentEffect();
       // CheckClickEquipment(null);
    }

    public virtual void OnGUI()
    {

    }

    public virtual void UpdateUI()
    {

    }

    private Object3DElement baseObject3DElement = null;

    private Object3DElement preObject3DElement = null;

    protected Object3DElement checkSelectEquipment;
    /// <summary>
    /// 检测是否单击到了设备
    /// </summary>
    /// <returns></returns>
    protected Object3DElement CheckClickEquipment(ref Transform hitTransform)
    {
       
        if (MouseCheck.CLICK)
        {
           
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            ////单击检测
            if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << Constant.EquipmentLayer))

            {
                Debug.Log("单击的设备对象：" + hit.transform.name);
                hitTransform = hit.transform;
                return FindObjUtility.FindEquipmentParent(hit.transform);
            }

        }

        return null;
    }


    protected bool  CheckClickScene(ref Transform hitTransform,ref Vector3 hitPostion)
    {
        if (MouseCheck.CLICK)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            ////单击检测
            if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << Constant.SceneLayer))

            {
                hitTransform = hit.transform;
                //Debug.Log("pengzhuang:"+ hitTransform.name);
                hitPostion = hit.point;
                return true;

            }

        }

        return false;
    }


    protected void EquipmentEffect()
    {
       
        if ((EventSystem.current!=null && EventSystem.current.IsPointerOverGameObject())|| 
            OperateControlManager.Instance.CurrentState != OperateControlManager.EquipmentEditState.None)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        //悬停检测
        if (Physics.Raycast(ray, out hit, float.MaxValue, (1 << Constant.EquipmentLayer)))
        {
            // Debug.Log(hit.transform);
            baseObject3DElement = FindObjUtility.FindEquipmentParent(hit.transform);
           
            if (baseObject3DElement != null)
            {
                if (preObject3DElement != null)
                {
                    preObject3DElement.SelectHigh(false);
                }
                baseObject3DElement.SelectHigh(true);

                preObject3DElement = baseObject3DElement;
            }
           
        }
        else if (preObject3DElement != null)
        {
            preObject3DElement.SelectHigh(false); 
        }
    }


}

