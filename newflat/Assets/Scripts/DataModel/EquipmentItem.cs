using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DataModel
{
    /// <summary>
    /// 设备对象
    /// </summary>
    ///
    [Serializable]
    public class EquipmentItem
    {
        public string id;
        public string name;

        public Vector3 postion;

        public Vector3 eulerAngles;

        public Vector3 scale = Vector3.one;

        public string parentid;

        public string modelid;

        public void UpdatePostion(Vector3 postion)
        {
            this.postion = postion;
        }

        public void UpdateScale(Vector3 scale)
        {
            this.scale = scale;
        }
        public void UpdateRotation(Vector3 eulerAngles)
        {
            this.eulerAngles = eulerAngles;
        }

        public void Update(Vector3 postion, Vector3 scale, Vector3 eulerAngles)
        {
            this.postion = postion;
            this.scale = scale;
            this.eulerAngles = eulerAngles;

        }

    }
}

