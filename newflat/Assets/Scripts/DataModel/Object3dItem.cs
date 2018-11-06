using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace DataModel
{
    public class Object3dItem
    {
        public string id;
        public string name;
        public string code;
        public Type type;
        public bool isDownFinish = false;

        public List<Object3dItem> childs;

        public string parentid;
    }

    public enum Type
    {
        None,
        Area,
        Builder,
        Floor,
        Room,
        Equipment
    }
}

