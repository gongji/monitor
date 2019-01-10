using Endgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// grid
/// </summary>
public  class GridMsg:MonoSingleton<GridMsg> {

    private GameObject grid = null;
    private GridBase gridBase;
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url">路径</param>
    /// <param name="width">宽度</param>
    /// <param name="height">高度</param>
    /// <param name="title">标题</param>
    /// <param name="closeButtonPath">关闭按钮路径</param>
    /// <param name="columWidths">列宽</param>
    /// <param name="columTitles">列标题</param>
    /// <param name="dataSouces">数据源，二维数组</param>
    public void Show<T>(string url,int width,int height,string title, string closeButtonPath, 
        int[] columWidths, string[] columTitles, List<List<string>> dataSouces) where T : GridBase
    {
        GameObject  grid = GameObject.Instantiate(Resources.Load<GameObject>(url));
        grid.transform.SetParent(UIUtility.GetRootCanvas());
        grid.transform.localScale = Vector3.zero;
        grid.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        grid.transform.localRotation = Quaternion.identity;
        ListView listView = grid.GetComponentInChildren<ListView>();
        listView.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        T gridBase = grid.AddComponent<T>();
        this.gridBase = gridBase;
        Button coloseButton = grid.transform.Find(closeButtonPath).GetComponent<Button>();

        gridBase.Init(title, coloseButton);
        gridBase.SetColumProperty(columWidths, columTitles);
        gridBase.SetDataSource(dataSouces);
        gridBase.Show();
    }

    public void Hide()
    {

        if(gridBase!=null)
        {
            gridBase.CloseWindow();
        }
    }


}
