using Endgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public  class SetControlPoint:MonoSingleton<SetControlPoint>,IEventListener
{
    private  GameObject Init()
    {
        GameObject grid = GameObject.Instantiate(Resources.Load<GameObject>("Grid/Control/ControlPoint"));
        grid.transform.SetParent(UIUtility.GetRootCanvas());
        grid.transform.localScale = Vector3.zero;
        grid.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        grid.transform.localRotation = Quaternion.identity;
        ListView listView = grid.GetComponentInChildren<ListView>();
        listView.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 290);

        return grid;
    }
    
    private  string HorizontalScrollBarName = "HorizontalScrollBar";
    private  void HideHorizontalScrollBar()
    {
        if(grid!=null)
        {
            Scrollbar[] scrollBas = grid.GetComponentsInChildren<Scrollbar>(true);
            foreach(Scrollbar sb in scrollBas)
            {
                if(sb.name.Equals(HorizontalScrollBarName))
                {
                    sb.gameObject.SetActive(false);
                    return;
                }
            }
        }
        
    }
    private  void SetStyle(ListView listView)
    {
        //listView.DefaultHeadingTextColor = Color.red;
    }
    private  GameObject grid;
    public  void Show(string equipmentName,string id)
    {
        TestPointProxy.GetControlPointList(id, (result) =>
        {
            List<EquipmentControlPointItem> ecps = Utils.CollectionsConvert.ToObject<List<EquipmentControlPointItem>>(result);
            SetDataSouce(ecps, equipmentName);
        },()=> {

            SetDataSouce(new List<EquipmentControlPointItem>(), equipmentName);
        });
    }

    private  ListView listView = null;

    private  bool isShow = false;
    public  void SetDataSouce(List<EquipmentControlPointItem> ecps,string equipmentName ="设备")
    {
        EventMgr.Instance.AddListener(this, EventName.DeleteObject);
        if(isShow)
        {
            return;
        }
        MaskManager.Instance.Show();
        //实例化
        grid = Init();
        grid.GetComponentInChildren<Button>().onClick.AddListener(CloseWindow);
        CreateTitle(equipmentName);
        listView = grid.GetComponentInChildren<ListView>();
        listView.ItemBecameVisible += OnItemBecameVisible;
        listView.ItemBecameInvisible += OnItemBecameInvisible;
        //增加列
        AddColumns(listView);
        SetColumWidth(listView);
        SetStyle(listView);
        //设置值
        //foreach (EquipmentControlPoint item in ecps)
        //{
        //    string[] subItemTexts = new string[] { "23", item.name, item.describe, item.name, "12" };
        //    ListViewItem _item = new ListViewItem(subItemTexts);
        //    listView.Items.Add(_item);
        //}
        
        foreach(EquipmentControlPointItem ecp in ecps)
        {
            string[] subItemTexts = new string[] { ecp.id, ecp.name, ecp.describe, "" };
            ListViewItem _item = new ListViewItem(subItemTexts);
            listView.Items.Add(_item);
        }
        HideHorizontalScrollBar();
        listView.GetComponent<Image>().color = listView.DefaultHeadingBackgroundColor;
        grid.transform.DOScale(Vector3.one, 0.5f);
        isShow = true;

    }

    /// <summary>
    /// 显示标题
    /// </summary>
    /// <param name="titleName"></param>
    private   void CreateTitle(string titleName)
    {
        Transform title = grid.transform.Find("Title");
        title.GetComponentInChildren<Text>().text ="  "+ titleName + "控制点设置";
    }

    private  void SetColumWidth(ListView ListView)
    {
        ListView.Columns[0].Width = 50;
        ListView.Columns[1].Width = 50;
        ListView.Columns[2].Width = 200;
        ListView.Columns[3].Width = 200;
    }

    private  void AddColumns(ListView ListView)
    {
        ListView.SuspendLayout();
        {
            AddColumnHeader(ListView, "选择");
            AddColumnHeader(ListView, "名称");
            AddColumnHeader(ListView, "描述");
            AddColumnHeader(ListView, "设置");
        }
        ListView.ResumeLayout();
    }

    private  void OnItemBecameVisible(ListViewItem item)
    {
        var confirmItem = item.SubItems[0];
        GameObject Check = GameObject.Instantiate(Resources.Load("Grid/Control/Check")) as GameObject;
        confirmItem.CustomControl = Check.transform as RectTransform;
        
        //Create locate 按钮
        var input = item.SubItems[3];
        GameObject inputButton = GameObject.Instantiate(Resources.Load("Grid/Control/Input")) as GameObject;
        input.CustomControl = inputButton.transform as RectTransform;
       
    }


    private  void OnItemBecameInvisible(ListViewItem item)
    {
        var Check = item.SubItems[0];
        GameObject CheckItemGameObject = Check.CustomControl.gameObject;


        var inputItem = item.SubItems[3];
        GameObject inputItemGameObject = inputItem.CustomControl.gameObject;

        GameObject.Destroy(CheckItemGameObject);
        GameObject.Destroy(inputItemGameObject);
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    public  void GetSelectData()
    {
        ListView.ListViewItemCollection list = listView.Items;
        Debug.Log(list.Count);
        for(int i=0;i< list.Count;i++)
        {
            ListViewItem item = list[i];
            var Check = item.SubItems[0];
            GameObject CheckItemGameObject = Check.CustomControl.gameObject;
            if (CheckItemGameObject.GetComponent<Toggle>().isOn)
            {
                var input = item.SubItems[3];
                GameObject inputItemGameObject = input.CustomControl.gameObject;
                string content = inputItemGameObject.GetComponent<TMP_InputField>().text;
                //Debug.Log(content);
            }
        }
    }


    private  void AddColumnHeader(ListView ListView, string title)
    {
        ColumnHeader columnHeader = new ColumnHeader();
        columnHeader.Text = title;
        ListView.Columns.Add(columnHeader);
    }
    private  bool clickingAColumnSorts = true;
    
    public  void DestryGrid()
    {
        if(grid!=null)
        {
            GameObject.Destroy(grid);
        }
        MaskManager.Instance.Hide();
        EventMgr.Instance.RemoveListener(this, EventName.DeleteObject);
        
    }

    public  void CloseWindow()
    {
        grid.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        grid.transform.DOScale(Vector3.zero, 0.5f).OnComplete(()=> {
            listView.ItemBecameVisible -= OnItemBecameVisible;
            listView.ItemBecameInvisible -= OnItemBecameInvisible;
          
            DestryGrid();
            isShow = false;


        });
    }

    bool IEventListener.HandleEvent(string eventName, IDictionary<string, object> dictionary)
    {
        //throw new System.NotImplementedException();
        if(eventName.Equals(EventName.DeleteObject))
        {
            DestryGrid();
        }
        return true;
    }
}
