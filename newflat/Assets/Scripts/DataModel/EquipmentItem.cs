using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace DataModel
{
    /// <summary>
    /// 设备对象
    /// </summary>
    ///
    [Serializable]
    public class EquipmentItem : ICloneable
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

       // public string parentsId = "";

        public string modelId="";

        public string sceneId = "";

        public string number = "";

        public string type = "";

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

        public void Update(Vector3 postion, Vector3 scale, Vector3 eulerAngles)
        {
            this.x = postion.x;
            this.y = postion.y;
            this.z = postion.z;
            this.scaleX = scale.x;
            this.scaleY = scale.y;
            this.scaleZ = scale.z;
            this.rotationX = eulerAngles.x;
            this.rotationY = eulerAngles.y;
            this.rotationZ = eulerAngles.z;

        }

        public object Clone()
        {

            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as EquipmentItem;
            }
        }
    }
}

