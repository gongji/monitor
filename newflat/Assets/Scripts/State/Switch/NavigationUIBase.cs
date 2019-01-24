using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationUIBase : MonoBehaviour {

    protected List<GameObject> navaUIList = new List<GameObject>();

    protected IEnumerator UpdateUIPostion(Dictionary<GameObject, BoxCollider> dic)
    {
        while (true)
        {
            foreach (GameObject ui in dic.Keys)
            {
                Vector3[] vs = Object3dUtility.GetBoxColliderVertex(dic[ui]);

                Vector3 uiPostion = GetMaxXValue(vs);
                if(ui==null)
                {
                   yield break;
                }
                ui.GetComponent<RectTransform>().anchoredPosition = uiPostion;
            }
            yield return 0;
        }
    }
    public void DeleteAllUI()
    {
        StopAllCoroutines();
        foreach (GameObject ui in navaUIList)
        {
            GameObject.DestroyImmediate(ui);
        }
        navaUIList.Clear();
    }
    /// <summary>
    /// 求出4个点中x的最大值
    /// 
    /// </summary>
    /// <param name="vs"></param>
    /// <returns></returns>
    protected Vector3 GetMaxXValue(Vector3[] vs)
    {
        float result = 0.0f;
        Dictionary<float, Vector3> dic = new Dictionary<float, Vector3>();
        foreach (Vector3 v in vs)
        {
            Vector2 uipostion = UIUtility.WorldToUI(v, Camera.main);
            if (uipostion.x > result)
            {
                result = uipostion.x;
                dic.Add(uipostion.x, uipostion);
            }
        }

        if (dic.ContainsKey(result))
        {
            return dic[result];
        }
        return Vector3.zero;
    }
}
