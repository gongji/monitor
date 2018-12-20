using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace DataModel
{
    public class Object3dItem
    {
        public string id;
        public string name;
        public string number;
        public Type type;
        public bool isDownFinish = false;

        public List<Object3dItem> childs;

        public string parentsId;

        public string path;
    }

    public enum Type
    {
        None,
        Area,
        Builder,
        Floor,
        Room,
        RoomDoor,
        De_Normal,
        De_Camera,
        De_LouShui,
        De_WenShidu,
        De_Door
    }
}

