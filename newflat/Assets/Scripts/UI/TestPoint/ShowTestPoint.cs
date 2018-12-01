using Endgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public  class ShowTestPoint:MonoSingleton<ShowTestPoint>,IEventListener
{
    private  GameObject Init()
    {
        GameObject grid = GameObject.Instantiate(Resources.Load<GameObject>("Grid/TestPointCenter"));
        grid.transform.SetParent(UIUtility.GetRootCanvas());
        grid.transform.localScale = Vector3.zero;
        grid.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        grid.transform.localRotation = Quaternion.identity;
        ListView listView = grid.GetComponentInChildren<ListView>();
        listView.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 290);
        
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
    private static GameObject grid;
    public  void Show(string equipmentName,string id)
    {
        TestPointProxy.GetTestPointData(id, (result) =>
        {
            Create(equipmentName, result);


        },()=> {

        Create(equipmentName, new List<EquipmentTestPoint>());
        });
    }

    private  void Create(string equipmentName,List<EquipmentTestPoint> dataSource)
    {
        if(dataSource==null || dataSource.Count==0)
        {
            Debug.Log("test point is null");
            return;
        }
        EventMgr.Instance.AddListener(this, EventName.DeleteObject);
        MaskManager.Instance.Show();
        //实例化
        grid = Init();
        grid.GetComponentInChildren<Button>().onClick.AddListener(CloseWindow);
        CreateTitle(equipmentName);
        ListView listView = grid.GetComponentInChildren<ListView>();
        listView.ColumnClick += OnColumnClick;
        //增加列
        AddColumns(listView);
        SetColumWidth(listView);
        SetStyle(listView);
        //listView.SuspendLayout();
        //{
        //    listView.Items.Clear();
        //}
        //listView.ResumeLayout();
        List<TestPointItem> _dataSource = dataSource[0].data;
        //设置值
        foreach (TestPointItem item in _dataSource)
        {
            string[] subItemTexts = new string[] { item.name, item.value, item.unit };
            ListViewItem _item = new ListViewItem(subItemTexts);
            listView.Items.Add(_item);
        }
        HideHorizontalScrollBar();
        listView.GetComponent<Image>().color = listView.DefaultHeadingBackgroundColor;
        grid.transform.DOScale(Vector3.one, 0.5f);
    }

    /// <summary>
    /// 显示标题
    /// </summary>
    /// <param name="titleName"></param>
    private  void CreateTitle(string titleName)
    {
        Transform title = grid.transform.Find("Title");
        title.GetComponentInChildren<Text>().text ="  "+ titleName + "测点信息";
    }

    private  void SetColumWidth(ListView ListView)
    {
        ListView.Columns[0].Width = 300;
        ListView.Columns[1].Width = 150;
        ListView.Columns[2].Width = 150;
    }

    private  void AddColumns(ListView ListView)
    {
        ListView.SuspendLayout();
        {
            AddColumnHeader(ListView, "测点名称");
            AddColumnHeader(ListView, "值");
            AddColumnHeader(ListView, "单位");
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
            DestryGrid();

        });
    }

    public bool HandleEvent(string eventName, IDictionary<string, object> dictionary)
    {
       // throw new System.NotImplementedException();
        if(eventName.Equals(EventName.DeleteObject))
        {
            DestryGrid();

        }

        return true;
    }
}
