using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class UIUtility
{
    /// <summary>
    /// 形成精灵
    /// </summary>
    /// <param name="data"></param>
    /// <param name="image"></param>
    /// <param name="m_Pivot"></param>
    public static void CreateSprite(Texture2D picv, Image image, Vector2 m_Pivot)
    {
        try
        {
           
            Vector4 tempborder = Vector4.zero;
            Sprite spr = Sprite.Create(picv, new Rect(0, 0, picv.width, picv.height), new Vector2(m_Pivot.x, m_Pivot.y), 100.0f, 0, SpriteMeshType.Tight, tempborder);//后面Vector2就是你Anchors的Pivot的x/y属性值
            image.sprite = spr;
        }
        catch (Exception e)
        {

        }

    }

    /// <summary>
    /// 形成精灵
    /// </summary>
    /// <param name="url"></param>
    /// <param name="image"></param>

    public static void GetSpring(string url, Image image)//Application.dataPath + "/Resources/UITableComponent/wgsj_up__0_0_0_0.png"
    {
        // if (!url.Contains(Application.dataPath)) return;
        // Debug<Seaweed>.Log(url);
        Texture2D picTemp = Resources.Load(url) as Texture2D;
        Texture2D pic = picTemp;

        string numbs = System.Text.RegularExpressions.Regex.Match(url, @"(?<=__)[\d_]+$").Value;

        String[] num = numbs.Split('_');

        Vector4 tempborder = new Vector4((float)int.Parse(num[2]), (float)int.Parse(num[1]), (float)int.Parse(num[3]), (float)int.Parse(num[0]));

        Sprite spr = Sprite.Create(pic, new Rect(0, 0, pic.width, pic.height), new Vector2(0.5f, 0.5f), 100.0f, 0, SpriteMeshType.Tight, tempborder);//后面Vector2就是你Anchors的Pivot的x/y属性值

        image.sprite = spr;

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);

        image.type = Image.Type.Sliced;
    }

    public static Sprite GetSpriteByTexture(Texture2D texture)
    {
        Vector4 tempborder = new Vector4(0, 0, 0, 0);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f, 0, SpriteMeshType.Tight, tempborder);
        return sprite;
    }


    /// <summary>
    /// 设置瞄点位置
    /// </summary>
    /// <param name="pivotType"></param>
    /// <param name="gameObject"></param>
    //public static void SetPivot(PivotType pivotType, GameObject gameObject)
    //{
    //    if (pivotType == PivotType.Center)
    //    {
    //        gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
    //    }
    //    else if (pivotType == PivotType.CenterButtom)
    //    {
    //        gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
    //    }
    //    else if (pivotType == PivotType.CenterUp)
    //    {
    //        gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1.0f);
    //    }
    //    else if (pivotType == PivotType.LeftUp)
    //    {
    //        gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
    //    }
    //    else if (pivotType == PivotType.LeftButtom)
    //    {
    //        gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 0.0f);
    //    }
    //    else if (pivotType == PivotType.RightUp)
    //    {
    //        gameObject.GetComponent<RectTransform>().pivot = new Vector2(1.0f, 1.0f);
    //    }
    //    else if (pivotType == PivotType.LeftCenter)
    //    {
    //        gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 0.5f);
    //    }

    //}

    /// <summary>
    /// 世界坐标转UGUI Cavans局部坐标
    /// </summary>
    /// <param name="worldPoint">世界坐标点</param>
    /// <param name="canvas">对应的Canvas</param>
    /// <returns></returns>
    public static Vector2 WorldToUI(Vector3 worldPoint, Camera objCamera)
    {
       // Debug.Log(worldPoint);
        Vector2 ScreenPostion = GetWordToScreen(worldPoint, objCamera);
       // Debug.Log(ScreenPostion);
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Vector2 uiPostion = GetScreenToUIPos(new Vector3(ScreenPostion.x, ScreenPostion.y, 0), canvas);
        return uiPostion;
    }

    private static Vector2 GetWordToScreen(Vector3 worldPoint, Camera objCamera)
    {
       
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(objCamera, worldPoint);
        //Vector2 screenPoint = objCamera.WorldToScreenPoint(worldPoint);
        return screenPoint;
    }
    /// <summary>
    /// 世屏幕坐标 Cavans局部坐标
    /// </summary>
    /// <param name="worldPoint">世界坐标点</param>
    /// <param name="canvas">对应的Canvas</param>
    /// <returns></returns>
    private static Vector2 GetScreenToUIPos(Vector3 screenPoint, Canvas canvas)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(),
            screenPoint, canvas.worldCamera, out localPoint);
        return localPoint;
    }

    public static Transform GetRootCanvas()
    {
        GameObject result = GameObject.Find("Canvas");

        if(result!=null)
        {
            return result.transform;
        }

        return null;
    }

    public static void HideShowAllUI(Boolean isShow)
    {
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if(canvas!=null)
        {
            canvas.enabled = isShow;

        }

    }

    /// <summary>
    /// 世界坐标转UI屏幕坐标
    /// </summary>
    /// <param name="wordPosition"></param>
    /// <returns></returns>
    public static Vector2 WordToScenePoint(Vector3 wordPosition)
    {
        CanvasScaler canvasScaler = GameObject.Find("UIFramework").gameObject.GetComponent<CanvasScaler>();

        float resolutionX = canvasScaler.referenceResolution.x;

        float resolutionY = canvasScaler.referenceResolution.y;

        float offect = (Screen.width / canvasScaler.referenceResolution.x) * (1 - canvasScaler.matchWidthOrHeight) + (Screen.height / canvasScaler.referenceResolution.y) * canvasScaler.matchWidthOrHeight;

        Vector2 a = RectTransformUtility.WorldToScreenPoint(GameObject.Find("SystemUICamera").GetComponent<Camera>(), wordPosition);

        return new Vector2(a.x / offect, a.y / offect);
        ;
    }

    public static void ShowTips(string tipText)
    {
       GameObject item =  GameObject.Instantiate(Resources.Load<GameObject>("UI/MessageTips"));
       item.transform.SetParent(GetRootCanvas());
        item.GetComponentInChildren<Text>().text = tipText;
        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        item.GetComponent<CanvasGroup>().alpha = 0;
        item.GetComponent<CanvasGroup>().DOFade(1, 1.5f);
        DOVirtual.DelayedCall(5.0f, () =>
        {
            item.GetComponent<CanvasGroup>().DOFade(0, 1.5f);

            DOVirtual.DelayedCall(1.5f, () =>
            {
                UnityEngine.GameObject.DestroyImmediate(item);
            });
                
        });

    }


}
