using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransformControlUtility
{
   
    public static GameObject CreateItem(GameObject _item, Transform _itemParent)
    {
        GameObject item = GameObject.Instantiate(_item, _itemParent);
        item.SetActive(true);
        item.transform.localScale = Vector3.one;
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.Euler(Vector3.zero);
        return item;
    }

  
    public static GameObject CreateItem(string url, Transform _itemParent)
    {
        GameObject item = GameObject.Instantiate(Resources.Load<GameObject>(url), _itemParent);
        item.SetActive(true);
        item.transform.localScale = Vector3.one;
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.Euler(Vector3.zero);
        return item;
    }



    /// <summary>
    /// 移除对象上的按钮事件
    /// </summary>
    /// <param name="_btn"></param>
    public static void RemoveEvent(GameObject _btn)
    {
        if (_btn && _btn.GetComponent<EventTrigger>() != null)
        {
            _btn.GetComponent<EventTrigger>().triggers.Clear();
        }
    }

    /// <summary>
    /// 添加按钮事件
    /// </summary>
    /// <param name="_btn">需要添加事件的按钮</param>
    /// <param name="_eventid">触发方式</param>
    /// <param name="_callback">事件</param>
    public static void AddEventToBtn(GameObject _btn, EventTriggerType _eventid, UnityEngine.Events.UnityAction<BaseEventData> _callback)
    {
        if (_btn.GetComponent<EventTrigger>() == null)
        {
            _btn.AddComponent<EventTrigger>();
        }
        EventTrigger trigger = _btn.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = _eventid;
        entry.callback.AddListener(_callback);
        trigger.triggers.Add(entry);
     
    }


    /// <summary>
    /// get total size
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Vector3 Get3DTransfromTotalSize(GameObject obj)
    {
        Vector3 size = Vector3.zero;

        Vector3 center = Vector3.zero;
        Renderer[] renders = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renders)
        {
            center += child.bounds.center;
        }
        center /= obj.GetComponentsInChildren<Transform>().Length;
        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach (Renderer child in renders)
        {
            bounds.Encapsulate(child.bounds);
        }
        size = bounds.size;
        return size;
    }

 
    public static Vector2 Get2DTransfromSize(Transform rectTransform)
    {

        float[] minXs = new float[rectTransform.childCount + 1];
        float[] maxXs = new float[rectTransform.childCount + 1];
        float[] minYs = new float[rectTransform.childCount + 1];
        float[] maxYs = new float[rectTransform.childCount + 1];


        minXs[0] = -rectTransform.GetComponent<RectTransform>().rect.width / 2;
        maxXs[0] = rectTransform.GetComponent<RectTransform>().rect.width / 2;
        minYs[0] = -rectTransform.GetComponent<RectTransform>().rect.height / 2;
        maxYs[0] = rectTransform.GetComponent<RectTransform>().rect.height / 2;

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            RectTransform item = rectTransform.GetChild(i).GetComponent<RectTransform>();
            if (item != null)
            {
                Vector3 scale =
                    new Vector3(item.lossyScale.x / rectTransform.lossyScale.x,
                                item.lossyScale.y / rectTransform.lossyScale.y,
                                item.lossyScale.z / rectTransform.lossyScale.z);
                minXs[i + 1] = item.localPosition.x - item.rect.width / 2 * scale.x;
                maxXs[i + 1] = item.localPosition.x + item.rect.width / 2 * scale.x;
                minYs[i + 1] = item.localPosition.y - item.rect.height / 2 * scale.y;
                maxYs[i + 1] = item.localPosition.y + item.rect.height / 2 * scale.y;
            }
        }

        float width = Mathf.Max(maxXs) - Mathf.Min(minXs);
        float height = Mathf.Max(maxYs) - Mathf.Min(minYs);
        Vector2 size = new Vector2(width, height);
        return size;
    }
}

