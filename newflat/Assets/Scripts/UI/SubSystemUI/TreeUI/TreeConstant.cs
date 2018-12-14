/***********************************
    **  日期:
    **  姓名:jss
    **  审阅:jss
    **  功能:
    **  备注:
************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Tree
{
    public class Constant
    {
        public struct BranchStruct
        {
            public string name;
            public List<BranchStruct> childs;

            public BranchStruct(string name, List<BranchStruct> childs)
            {
                this.name = name;
                this.childs = childs;
            }
        }

        public static GameObject CreateBranchButton()
        {
            GameObject go = GameObject.Instantiate(Resources.Load("UI/UITree/BranchButton")) as GameObject;
            return go;
        }

        public static GameObject CreateBranchGrid()
        {
            GameObject go = GameObject.Instantiate(Resources.Load("UI/UITree/tree")) as GameObject;
            return go;
        }

        public static GameObject CreateBranchArrowUp()
        {
            GameObject go = GameObject.Instantiate(Resources.Load("UI/UITree/ArrowUp")) as GameObject;
            return go;
        }

        public static GameObject CreateBranchArrowDown()
        {
            GameObject go = GameObject.Instantiate(Resources.Load("UI/UITree/ArrowDown")) as GameObject;
            return go;
        }

        public static T TraverseParentFind<T>(Transform tran)
        {
            T val;
            val = tran.GetComponent<T>();
            if (val == null)
            {
                if (tran.parent != null)
                    return TraverseParentFind<T>(tran.parent);
            }
            return val;
        }

        public static void TraverseChild(Transform target, Action<Transform> callback)
        {
            if (target.childCount != 0)
            {
                for (int i = 0; i < target.childCount; i++)
                {
                    Transform val = target.GetChild(i);
                    callback(val);
                    TraverseChild(val, callback);
                }
            }
        }
    }

    public class TreeJsonStruct
    {
        public string name;
        public List<object> childs;
    }
}
