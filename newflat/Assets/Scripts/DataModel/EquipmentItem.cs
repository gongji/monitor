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
        public string id = "";
        public string name = "";

        public float x =  0.0f;
        public float y = 0.0f;
        public float z = 0.0f;

        public float rotationX = 0.0f;

        public float rotationY = 0.0f;

        public float rotationZ = 0.0f;

        public float scaleX = 0.0f;

        public float scaleY = 0.0f;

        public float scaleZ = 0.0f;

        public string parentsId = "";

        public string modelId="";

        public void UpdatePostion(float x,float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void UpdateScale(float scaleX, float scaleY, float scaleZ)
        {
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            this.scaleZ = scaleZ;

        }
        public void UpdateRotation(float rotationX,float rotationY, float  rotationZ)
        {
            this.rotationX = rotationX;
            this.rotationY = rotationY;
            this.rotationZ = rotationZ;
        }

        //public void Update(Vector3 postion, Vector3 scale, Vector3 eulerAngles)
        //{
        //    this.postion = postion;
        //    this.scale = scale;
        //    this.eulerAngles = eulerAngles;

        //}

    }
}

