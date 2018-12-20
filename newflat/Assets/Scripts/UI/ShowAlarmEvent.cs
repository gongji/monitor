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
    private void Start()
    {
        Show();
    }

    private static void SetStyle(ListView listView)
    {
        //listView.DefaultHeadingTextColor = Color.red;
    }
    
    public  void Show()
    {

        CreateTitle(name);
        ListView listView = transform.GetComponentInChildren<ListView>();
        listView.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 290);
        listView.ColumnClick += OnColumnClick;
        listView.ItemBecameVisible += this.OnItemBecameVisible;
        listView.ItemBecameInvisible += this.OnItemBecameInvisible;


        //增加列
        AddColumns(listView);
        SetColumWidth(listView);
        SetStyle(listView);
        //listView.SuspendLayout();
        //{
        //    listView.Items.Clear();
        //}
        //listView.ResumeLayout();
        for (int   i = 0;i<10;i++)
        {
            string locateid = i + "locateid";
            string confirmId = i + "confirmId";
            string[] subItemTexts = new string[] { "ups"+i, "通讯中断无法连接", "2018-09-09 11:12:11" , "配电房1f101" ,"",""};
            ListViewItem _item = new ListViewItem(subItemTexts);
            ItemData item = new ItemData();
            item.locateId = locateid;
            item.confirmId = confirmId;
            _item.Tag = item;
            listView.Items.Add(_item);
        }
        horScrollBar.gameObject.SetActive(false);
        listView.GetComponent<Image>().color = listView.DefaultHeadingBackgroundColor;
    }

    private void OnItemBecameVisible(ListViewItem item)
    {
        // Create locate 按钮
        var locateItem = item.SubItems[4];

        ItemData itemData = item.Tag as ItemData;

        GameObject locateButton = GameObject.Instantiate(Resources.Load("UI/Alarm/locate")) as GameObject;
        locateItem.CustomControl = locateButton.transform as RectTransform;
        locateButton.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            LocateEquipment(itemData.locateId);
        });

        var confirmItem = item.SubItems[5];
        GameObject confirmButton = GameObject.Instantiate(Resources.Load("UI/Alarm/confirm")) as GameObject;
        confirmItem.CustomControl = confirmButton.transform as RectTransform;
        confirmButton.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            ConfirmEquipment(itemData.confirmId);
        });

    }


    private void LocateEquipment(string locateid)
    {
        Debug.Log(locateid);
    }

    private void ConfirmEquipment(string confirmId)
    {
        Debug.Log(confirmId);
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

    private static void SetColumWidth(ListView ListView)
    {
        ListView.Columns[0].Width = 100;
        ListView.Columns[1].Width = 150;
        ListView.Columns[2].Width = 150;
        ListView.Columns[3].Width = 100;
        ListView.Columns[4].Width = 50;
        ListView.Columns[5].Width = 50;
    }

    private static void AddColumns(ListView ListView)
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
    private static void AddColumnHeader(ListView ListView, string title)
    {
        ColumnHeader columnHeader = new ColumnHeader();
        columnHeader.Text = title;
        ListView.Columns.Add(columnHeader);
    }
    private static bool clickingAColumnSorts = true;
    private static void OnColumnClick(object sender, ListView.ColumnClickEventArgs e)
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
