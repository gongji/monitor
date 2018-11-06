using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EditEquipmentSet  {

    /// <summary>
    /// 通过设备的碰撞tranform获取父对象
    /// </summary>
    /// <param name="hitTransform">碰撞点的设备</param>
    public static  Transform  GetEquipmentParent(Transform hitTransform,Vector3 hitPostion)
    {
        Transform parent = FindObjUtility.GetParentObject(hitTransform);
        Object3DElement element = parent.GetComponent<Object3DElement>();
        //园区
        if(element ==null || (element!=null && element.type == DataModel.Type.Builder))
        {
            return null;
        }
        //房间
        else if(element != null && element.type == DataModel.Type.Room)
        {

            return element.transform;
        }
        //层
        else if(element != null && element.type == DataModel.Type.Floor)
        {

            Transform result = FindFloor(hitPostion, element.transform);
            
            //楼层下
            if(result==null)
            {

                return element.transform;
            }
            //房间
            return result;
        }

        return null;
    }


    /// <summary>
    /// 碰撞点是否位于房间下
    /// </summary>
    /// <param name="hitTransform"></param>
    /// <param name="hitPostion"></param>
    /// <param name="floorObject"></param>
    /// <returns></returns>
    private static Transform FindFloor(Vector3 hitPostion,Transform floorTransform)
    {
        Object3DElement roomelElment = null;
        foreach (Transform room in floorTransform)
        {
            roomelElment = room.GetComponent<Object3DElement>();
            if(roomelElment!=null && roomelElment.type == DataModel.Type.Room)
            {
                GameObject  box  = FindObjUtility.GetTransformChildByName(room, Constant.ColliderName);
                if(box!=null && box.GetComponent<BoxCollider>()!=null)
                {
                    box.GetComponent<BoxCollider>().enabled = true;
                    Vector3 size = box.GetComponent<BoxCollider>().size;

                    box.GetComponent<BoxCollider>().size = new Vector3(size.x,100, size.z);
                    if (box.GetComponent<BoxCollider>().bounds.Contains(hitPostion))
                    {
                        box.GetComponent<BoxCollider>().size = size;
                        box.GetComponent<BoxCollider>().enabled = false;
                        return room;
                    }
                    box.GetComponent<BoxCollider>().size = size;
                    box.GetComponent<BoxCollider>().enabled = false;


                }
            }
        }

        return null;
    }

    public static void AddEquipmentScripts(GameObject equipment)
    {
        Object3DElement object3DElement = equipment.GetComponent<Object3DElement>();
        if(object3DElement==null)
        {
            object3DElement = equipment.gameObject.AddComponent<Object3DElement>();
        }
    }

 
}
