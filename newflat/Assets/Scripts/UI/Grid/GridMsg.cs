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
    /// <param name="url">path=</param>
    /// <param name="width">width</param>
    /// <param name="height">height</param>
    /// <param name="title">title</param>
    /// <param name="closeButtonPath">close button path</param>
    /// <param name="columWidths">column width</param>
    /// <param name="columTitles">column title</param>
    /// <param name="dataSouces">datasource</param>
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
