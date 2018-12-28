using Endgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class ShowAlarmEvent :MonoBehaviour
{

    class ItemData
    {
        public string locateId;
        public string confirmId;
    }


    public Transform horScrollBar;
    //private void Start()
    //{
    //    Show();
    //}

    private  void SetStyle(ListView listView)
    {
        //listView.DefaultHeadingTextColor = Color.red;
    }

    private  bool isInit = false;
    private   void Init()
    {
        CreateTitle(name);
        ListView listView = transform.GetComponentInChildren<ListView>();
        listView.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(605, 290);
        listView.ColumnClick += OnColumnClick;
        listView.ItemBecameVisible += this.OnItemBecameVisible;
        listView.ItemBecameInvisible += this.OnItemBecameInvisible;
        //增加列
        AddColumns(listView);
        SetColumWidth(listView);
        SetStyle(listView);
        horScrollBar.gameObject.SetActive(false);
        listView.GetComponent<Image>().color = listView.DefaultHeadingBackgroundColor;
    }
    
    public   void Show()
    {

        if(!isInit)
        {
            Init();
        }
        
        //listView.SuspendLayout();
        //{
        //    listView.Items.Clear();
        //}
        //listView.ResumeLayout();
        //for (int   i = 0;i<10;i++)
        //{
        //    string locateid = i + "locateid";
        //    string confirmId = i + "confirmId";
        //    string[] subItemTexts = new string[] { "ups"+i, "通讯中断无法连接", "2018-09-09 11:12:11" , "配电房1f101" ,"",""};
        //    ListViewItem _item = new ListViewItem(subItemTexts);
        //    ItemData item = new ItemData();
        //    item.locateId = locateid;
        //    item.confirmId = confirmId;
        //    _item.Tag = item;
        //    listView.Items.Add(_item);
        //}
       
    }

    /// <summary>
    /// 插入事件到表格最前列
    /// </summary>
    /// <param name="aei"></param>
    private void AddEvent(AlarmEventItem aei)
    {
        transform.GetComponentInChildren<ListView>().Items.Insert(0, CreateItem(aei));
    }

    /// <summary>
    /// 创建行数据
    /// </summary>
    /// <param name="aei"></param>
    /// <returns></returns>
    private ListViewItem CreateItem(AlarmEventItem aei)
    {
        string[] subItemTexts = new string[] { aei.name, aei.content, aei.dateTime, aei.station};
        ListViewItem _item = new ListViewItem(subItemTexts);
        ItemData item = new ItemData();
        item.locateId = aei.id;
        item.confirmId = aei.id;
        _item.Tag = item;

        return _item;
    }


    /// <summary>
    /// 创建确认和定位的按钮
    /// </summary>
    /// <param name="item"></param>
    private void OnItemBecameVisible(ListViewItem item)
    {
        ItemData itemData = item.Tag as ItemData;
        var confirmItem = item.SubItems[4];
        GameObject confirmButton = GameObject.Instantiate(Resources.Load("UI/Alarm/confirm")) as GameObject;
        confirmItem.CustomControl = confirmButton.transform as RectTransform;
        confirmButton.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            ConfirmEquipment(itemData.confirmId, item);
        });

        // Create locate 按钮
        var locateItem = item.SubItems[5];
        GameObject locateButton = GameObject.Instantiate(Resources.Load("UI/Alarm/locate")) as GameObject;
        locateItem.CustomControl = locateButton.transform as RectTransform;
        locateButton.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            LocateEquipment(itemData.locateId);
        });
    }

    /// <summary>
    /// 定位
    /// </summary>
    /// <param name="locateid"></param>
    private void LocateEquipment(string locateid)
    {
        Debug.Log(locateid);
    }

    /// <summary>
    /// 确认
    /// </summary>
    /// <param name="confirmId"></param>
    private void ConfirmEquipment(string confirmId, ListViewItem item)
    {
        transform.GetComponentInChildren<ListView>().Items.Remove(item);
    }

    private void OnItemBecameInvisible(ListViewItem item)
    {
        var locateItem = item.SubItems[4];
        GameObject locateItemGameObject = locateItem.CustomControl.gameObject;


        var confirmItem = item.SubItems[5];
        GameObject confirmItemGameObject = confirmItem.CustomControl.gameObject;

        GameObject.Destroy(locateItemGameObject);
        GameObject.Destroy(confirmItemGameObject);
    }


    /// <summary>
    /// 显示标题
    /// </summary>
    /// <param name="titleName"></param>
    private  void CreateTitle(string titleName)
    {
        Transform title = transform.Find("Title");
        title.GetComponentInChildren<Text>().text =" 报警实时信息";
    }

    private  void SetColumWidth(ListView ListView)
    {
        ListView.Columns[0].Width = 100;
        ListView.Columns[1].Width = 150;
        ListView.Columns[2].Width = 150;
        ListView.Columns[3].Width = 100;
        ListView.Columns[4].Width = 50;
        ListView.Columns[5].Width = 50;
    }

    private  void AddColumns(ListView ListView)
    {
        ListView.SuspendLayout();
        {
            AddColumnHeader(ListView, "设备名称");
            AddColumnHeader(ListView, "报警内容");
            AddColumnHeader(ListView, "时间");
            AddColumnHeader(ListView, "地点");
            AddColumnHeader(ListView, "确认");
            AddColumnHeader(ListView, "定位");
        }
        ListView.ResumeLayout();
    }
    private  void AddColumnHeader(ListView ListView, string title)
    {
        ColumnHeader columnHeader = new ColumnHeader();
        columnHeader.Text = title;
        ListView.Columns.Add(columnHeader);
    }
    private  bool clickingAColumnSorts = true;
    private  void OnColumnClick(object sender, ListView.ColumnClickEventArgs e)
    {
        if (clickingAColumnSorts)
        {
            ListView listView = (ListView)sender;
            listView.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private class ListViewItemComparer : IComparer
    {
        private int columnIndex = 0;

        public ListViewItemComparer()
        {
        }

        public ListViewItemComparer(int columnIndex)
        {
            this.columnIndex = columnIndex;
        }

        public int Compare(object object1, object object2)
        {
            ListViewItem listViewItem1 = object1 as ListViewItem;
            ListViewItem listViewItem2 = object2 as ListViewItem;
            string text1 = listViewItem1.SubItems[this.columnIndex].Text;
            string text2 = listViewItem2.SubItems[this.columnIndex].Text;
            return string.Compare(text1, text2);
        }
    }

   
}
