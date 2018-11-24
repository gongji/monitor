using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 管网的处理
/// </summary>
public static class GuanWangMsg
{
    private static List<Object3dItem> curentStata;
    public static void ShowGuanWangShow()
    {
       // Debug.Log("管网显示");
        List<Object3dItem> guanwangs = SceneData.GetCurrentGuangWang();
        curentStata = guanwangs;
        var result = from o in guanwangs where o.isDownFinish == false select o;

        //需要下载
        if(result.Count()>0)
        {
            DownLoader.Instance.StartSceneDownLoad(result.ToList<Object3dItem>(), () => {

                SetRootShowHide(guanwangs, true);

            }, true);
        }
        else
        {
            SetRootShowHide(guanwangs, true);
        }
    }

    private static void SetRootShowHide(List<Object3dItem> guanwangs,bool isShow)
    {
        foreach (Object3dItem item in guanwangs)
        {
            GameObject root = SceneUtility.GetGameByRootName(item.number, item.number);
            if (root)
            {
                root.SetActive(isShow);
                //设置偏移位置和楼层匹配
                if(isShow && root.GetComponent<TransformObject>()!=null)
                {
                    //Debug.Log("管网显示");
                    root.transform.position = root.GetComponent<TransformObject>().defaultPostion + Vector3.up * (SceneContext.offestIndex-1) * 100;
                }
            }

        }
    }



    /// <summary>
    /// 隐藏当前所有的管网
    /// </summary>
    public static void AllGuanWangHide()
    {
        Debug.Log("隐藏管网");
        //List<Object3dItem> guanwangs = SceneData.GetCurrentGuangWang();
        if(curentStata!=null)
        {
            SetRootShowHide(curentStata, false);
        }
       
        //curentStata = null;


    }
}
