using Endgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class ShowAlarmEvent :MonoBehaviour
{

    class EventItemData
    {
        public string equipmentid;   
        public string eventId;
        public AlarmEventItem id;
    }

    public static ShowAlarmEvent instance;

    public Transform horScrollBar;

    private void Awake()
    {
        instance = this;
    }

    private  void SetStyle(ListView listView)
    {
        //listView.DefaultHeadingTextColor = Color.red;
    }

    private void Start()
    {
       // Debug.Log("Start");
        Init();
    }
   
    private  bool isInit = false;

    private Color dgColor = Color.white;

    public static readonly int MAX_VALUE = 100;
    private   void Init()
    {
        
        CreateTitle(name);
        ListView listView = transform.GetComponentInChildren<ListView>();
        listView.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(605, 287);
        listView.ColumnClick += OnColumnClick;
        listView.ItemBecameVisible += this.OnItemBecameVisible;
        listView.ItemBecameInvisible += this.OnItemBecameInvisible;
        //增加列
        AddColumns(listView);
        SetColumWidth(listView);
        SetStyle(listView);
        horScrollBar.gameObject.SetActive(false);
        listView.GetComponent<Image>().color = listView.DefaultHeadingBackgroundColor;
        dgColor = listView.DefaultHeadingBackgroundColor;
    }
    
    public  void Show(AlarmEventItem aei)
    {
        if(transform.GetComponentInChildren<ListView>().Items.Count == MAX_VALUE)
        {
            transform.GetComponentInChildren<ListView>().Items.RemoveAt(transform.GetComponentInChildren<ListView>().Items.Count-1);
        }
        transform.GetComponentInChildren<ListView>().Items.Insert(0, CreateItem(aei));
        transform.GetComponentInChildren<ListView>().GetComponent<Image>().color = dgColor;
        
        PlayAlarmSound();
    }

    private void PlayAlarmSound()
    {
        SoundUtilty.StopSound();
        string url = Application.streamingAssetsPath + "/Sound/alarm.mp3";
        SoundUtilty.PlayServerSound(url, false);
    }


    /// <summary>
    /// 创建行数据
    /// </summary>
    /// <param name="aei"></param>
    /// <returns></returns>
    private ListViewItem CreateItem(AlarmEventItem aei)
    {
        string[] subItemTexts = new string[] { aei.name,  aei.dateTime, aei.stationName,aei.content, aei.id, aei.id,aei.eventId};
        ListViewItem _item = new ListViewItem(subItemTexts);
        _item.Tag = aei;

        return _item;
    }

    #region AddRemoveItem Event
    /// <summary>
    /// 创建确认和定位的按钮
    /// </summary>
    /// <param name="item"></param>
    private void OnItemBecameVisible(ListViewItem item)
    {
        // Debug.Log("增加确认按钮");
        AlarmEventItem itemData = item.Tag as AlarmEventItem;
        var confirmItem = item.SubItems[4];
        GameObject confirmButton = GameObject.Instantiate(Resources.Load("UI/Alarm/confirm")) as GameObject;
        confirmItem.CustomControl = confirmButton.transform as RectTransform;
        confirmButton.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            ConfirmEquipment(itemData, item);
        });

        // Create locate 按钮
        var locateItem = item.SubItems[5];
        GameObject locateButton = GameObject.Instantiate(Resources.Load("UI/Alarm/locate")) as GameObject;
        locateItem.CustomControl = locateButton.transform as RectTransform;
        locateButton.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            LocateEquipment(itemData);
        });

        // 详情按钮
        var detailItem = item.SubItems[6];
        GameObject detailButton = GameObject.Instantiate(Resources.Load("UI/Alarm/detail")) as GameObject;
        detailButton.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 30);
        detailItem.CustomControl = detailButton.transform as RectTransform;
        detailButton.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            DetailEvent(itemData);
        });

    }

    private void OnItemBecameInvisible(ListViewItem item)
    {
        var locateItem = item.SubItems[4];
        GameObject locateItemGameObject = locateItem.CustomControl.gameObject;


        var confirmItem = item.SubItems[5];
        GameObject confirmItemGameObject = confirmItem.CustomControl.gameObject;

        var detailItem = item.SubItems[6];
        GameObject detailItemGameObject = detailItem.CustomControl.gameObject;

        GameObject.Destroy(locateItemGameObject);
        GameObject.Destroy(confirmItemGameObject);
        GameObject.Destroy(detailItemGameObject);
    }
    #endregion


    #region Button Event
    /// <summary>
    /// 定位
    /// </summary>
    /// <param name="locateid"></param>
    private void LocateEquipment(AlarmEventItem itemData)
    {
        if(itemData!=null && !string.IsNullOrEmpty(itemData.id) && !string.IsNullOrEmpty(itemData.sceneId))
        {
            Main.instance.stateMachineManager.LocateEquipment(itemData.id, itemData.sceneId);
        }
    }

    private ListViewItem item = null;
    /// <summary>
    /// 确认事件
    /// </summary>
    /// <param name="confirmId"></param>
    private void ConfirmEquipment(AlarmEventItem itemData, ListViewItem item)
    {
        GameObject detaillUI = TransformControlUtility.CreateItem("UI/Alarm/AlarmEventConfirm", UIUtility.GetRootCanvas());
        AlarmEventConfirm acf = detaillUI.GetComponent<AlarmEventConfirm>();

        if(acf==null)
        {
            acf = detaillUI.AddComponent <AlarmEventConfirm>();
        }
        acf.Show(itemData);

        acf.callBack = SetConfirmEquipment;

        this.item = item;
    }

    public void SetConfirmEquipment(AlarmEventItem itemData,string repairPerson,string eventContent)
    {
        if (item != null)
        {
            transform.GetComponentInChildren<ListView>().Items.Remove(item);
        }

        item = null;

        transform.GetComponentInChildren<ListView>().GetComponent<Image>().color = dgColor;


        List<string> list = new List<string>();
        list.Add(itemData.key);
        list.Add(itemData.eventId);
        list.Add(TimeUtility.FormatCurrentDate());
       
        list.Add(UserLogin.loginUserName);
        list.Add(eventContent);
        list.Add(repairPerson);
        list.Add("25");

        string sendData = Utils.StrUtil.ConnetString(list, "|");
        Debug.Log("sendData="+ sendData);
        //WebsocjetService.Instance.SendData(sendData);
        UIUtility.ShowTips("确认成功");

    }

    private void DetailEvent(AlarmEventItem itemData)
    {
       // Debug.Log("详情：" + itemData.id);

       GameObject detaillUI = TransformControlUtility.CreateItem("UI/Alarm/AlarmEventDetail", UIUtility.GetRootCanvas());
        Debug.Log("11");
       AlarmEventWindowBase ads =  detaillUI.AddComponent<AlarmEventDetailShow>();
       ads.Show(itemData);
    }

    #endregion


    /// <summary>
    /// 显示标题
    /// </summary>
    /// <param name="titleName"></param>
    private void CreateTitle(string titleName)
    {
        Transform title = transform.Find("Title");
        title.GetComponentInChildren<Text>().text =" 报警事件";
    }

    private  void SetColumWidth(ListView ListView)
    {
        ListView.Columns[0].Width = 70;
        ListView.Columns[1].Width = 130;
        ListView.Columns[2].Width = 100;
        ListView.Columns[3].Width = 150;
        ListView.Columns[4].Width = 50;
        ListView.Columns[5].Width = 50;
        ListView.Columns[6].Width = 50;
    }

    private  void AddColumns(ListView ListView)
    {
        ListView.SuspendLayout();
        {
            AddColumnHeader(ListView, "名称");
            AddColumnHeader(ListView, "时间");
            AddColumnHeader(ListView, "地点");
            AddColumnHeader(ListView, "报警内容");
            AddColumnHeader(ListView, "确认");
            AddColumnHeader(ListView, "定位");
            AddColumnHeader(ListView, "详情");
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
