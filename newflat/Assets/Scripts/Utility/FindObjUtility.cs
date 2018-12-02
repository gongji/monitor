using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class FindObjUtility
{

    /// <summary>
    /// 查找并获取节点下指定名称的子物体
    /// </summary>
    /// <param name="_root">指定开始节点</param>
    /// <param name="_childname">查找的物体名称</param>
    /// <returns></returns>
    public static Transform GetChild(Transform _root, string _childname)
    {
        if (_childname == _root.name)
        {
            return _root;
        }

        if (_childname == "null" || _childname == null)
        {
            return null;
        }
        if (_root.childCount == 0)
        {
            return null;
        }
        Transform tra = null;
        for (int i = 0; i < _root.childCount; i++)
        {
            Transform go = _root.GetChild(i);
            tra = GetChild(go, _childname);
            if (tra != null)
            {
                break;
            }
        }
        return tra;
    }

    /// <summary>
    /// 查找并获取节点上指定名称的父物体
    /// </summary>
    /// <param name="_root">指定节点</param>
    /// <param name="_parentname">查找的父级名称</param>
    /// <returns></returns>
    public static Transform GetParent(Transform _root,string _parentname)
    {
        if (_root.name.Contains(_parentname))
        {
            return _root;
        }
        return GetParent(_root.parent, _parentname);
    }

    public static GameObject GetTransformChildByName(Transform t, string name)
    {

        foreach (Transform child in t)
        {
            if (child.name.ToLower().Contains(name.ToLower()))
            {
                return child.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// 查找根节点是否为设备
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Object3DElement FindEquipmentParent(Transform t)
    {
        if (t == null)
        {
            return null;
        }
        Object3DElement parentObjectElement = null;
        while (t)
        {
            Object3DElement object3DElement = t.GetComponent<Object3DElement>();
            //if (object3DElement != null && object3DElement.type == type)
            //{
            //Debug.Log(object3DElement.type.ToString());
            if (object3DElement != null && object3DElement.type.ToString().StartsWith(Constant.Equipment_Prefix))
            {

                parentObjectElement = object3DElement;
                break;
            }
            t = t.parent;
        }
        return parentObjectElement;

    }

    public static List<Transform> FindRoom(Transform parentT)
    {
        List<Transform> list = new List<Transform>();
        foreach(Transform child in parentT)
        {
            string[] names = child.name.ToLower().Split('_');
            string endStr = names[names.Length - 1].ToLower().Trim();
            Regex fjRegex = new Regex("fj\\d");
            if(fjRegex.IsMatch(endStr) && !child.name.Contains(Constant.Door))
            {
                list.Add(child);
            }
        }

        return list;
    }

    /// <summary>
    /// 查找根节点
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Transform GetParentObject(Transform t)
    {

        while(t)
        {
            if(!t.parent)
            {
                break;
            }
            t = t.parent;
           
        }

        return t;
    }
}
