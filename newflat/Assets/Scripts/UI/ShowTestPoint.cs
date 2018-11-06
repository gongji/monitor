using Endgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public sealed class ShowTestPoint
{

    private static GameObject Init()
    {
        GameObject grid = GameObject.Instantiate(Resources.Load<GameObject>("Grid/TestPointCenter"));
        grid.transform.SetParent(UIUtility.GetRootCanvas());
        grid.transform.localScale = Vector3.zero;
        grid.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        grid.transform.localRotation = Quaternion.identity;
        ListView listView = grid.GetComponentInChildren<ListView>();
        listView.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 290);
        
        return grid;
    }


    private static string HorizontalScrollBarName = "HorizontalScrollBar";

   
    private static void HideHorizontalScrollBar()
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
    private static void SetStyle(ListView listView)
    {
        //listView.DefaultHeadingTextColor = Color.red;
    }
    private static GameObject grid;
    public static void Show(string name,string id)
    {

        
        TestPointProxy.GetTestPointData(id, (result) =>
        {

            MaskManager.Instance.Show();
            //实例化
            grid = Init();
            grid.GetComponentInChildren<Button>().onClick.AddListener(CloseWindow);
            CreateTitle(name);
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
            foreach (EquipmentTestPoint item in result)
            {
                string[] subItemTexts = new string[] { item.name, item.value, item.unit };
                ListViewItem _item = new ListViewItem(subItemTexts);
                listView.Items.Add(_item);
            }
            HideHorizontalScrollBar();
            listView.GetComponent<Image>().color = listView.DefaultHeadingBackgroundColor;
            grid.transform.DOScale(Vector3.one, 0.5f);
        });

    }

    /// <summary>
    /// 显示标题
    /// </summary>
    /// <param name="titleName"></param>
    private static void CreateTitle(string titleName)
    {
        Transform title = grid.transform.Find("Title");
        title.GetComponentInChildren<Text>().text ="  "+ titleName + "测点信息";
    }

    private static void SetColumWidth(ListView ListView)
    {
        ListView.Columns[0].Width = 300;
        ListView.Columns[1].Width = 100;
        ListView.Columns[2].Width = 100;
    }

    private static void AddColumns(ListView ListView)
    {
        ListView.SuspendLayout();
        {
            AddColumnHeader(ListView, "测点名称");
            AddColumnHeader(ListView, "值");
            AddColumnHeader(ListView, "单位");
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

    public static void DestryGrid()
    {
        if(grid!=null)
        {
            GameObject.Destroy(grid);
        }
        MaskManager.Instance.Hide();
        
    }

    public static void CloseWindow()
    {
        grid.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        grid.transform.DOScale(Vector3.zero, 0.5f).OnComplete(()=> {
            DestryGrid();

        });
    }
}
