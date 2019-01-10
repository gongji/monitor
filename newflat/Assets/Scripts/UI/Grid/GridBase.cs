using Endgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using animation;

public abstract class GridBase : MonoBehaviour {
    private void InitParameter(Button closeButton)
    {
        ListView listView = GetComponentInChildren<ListView>();
        listView.ColumnClick += OnColumnClick;
        listView.ItemBecameVisible += OnItemBecameVisible;
        listView.ItemBecameInvisible += OnItemBecameInvisible;
        closeButton.onClick.AddListener(CloseWindow);

        HideHorizontalScrollBar();
        listView.GetComponent<Image>().color = listView.DefaultHeadingBackgroundColor;

    }
    private string HorizontalScrollBarName = "HorizontalScrollBar";
    private void HideHorizontalScrollBar()
    {
       
        Scrollbar[] scrollBas = GetComponentsInChildren<Scrollbar>(true);
        foreach (Scrollbar sb in scrollBas)
        {
            if (sb.name.Equals(HorizontalScrollBarName))
            {
                sb.gameObject.SetActive(false);
                return;
            }
        }
    }

    private UICenterScaleBig uiCenterScaleBig;
    public void Show()
    {
        uiCenterScaleBig = new UICenterScaleBig(gameObject, 0.5f);
        uiCenterScaleBig.EnterAnimation(() => {
           
        });
    }

    protected abstract void OnItemBecameVisible(ListViewItem item);
    protected abstract void OnItemBecameInvisible(ListViewItem item);
     private void SetTitle(string titleName)
    {
        Transform title = transform.Find("Title");
        title.GetComponentInChildren<Text>().text = " "+titleName;
    }


    public void Init(string title,Button closeButton)
    {
        //实例化
        InitParameter(closeButton);

        SetTitle(title);
    }

    private bool clickingAColumnSorts = true;
    private void OnColumnClick(object sender, ListView.ColumnClickEventArgs e)
    {
        if (clickingAColumnSorts)
        {
            ListView listView = (ListView)sender;
            listView.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }
    }

    /// <summary>
    /// 设置列的宽度
    /// </summary>
    /// <param name="widths"></param>
    public void SetColumProperty(int[] columWidths, string[] columTitles)
    {
        AddColumnsTitle(columTitles);

        ListView listView = GetComponentInChildren<ListView>();
        for(int i=0;i< columWidths.Length;i++)
        {
            listView.Columns[i].Width = columWidths[i];
        }
    }
    /// <summary>
    /// 设置列标题
    /// </summary>
    /// <param name="ListView"></param>
    protected void AddColumnsTitle(string[] titles)
    {
        ListView listView = GetComponentInChildren<ListView>();

        listView.SuspendLayout();
        {
            foreach(string title in titles)
            {
                AddColumnHeader(listView, title);
            }
            
        }
        listView.ResumeLayout();
    }

    private void AddColumnHeader(ListView ListView, string title)
    {
        ColumnHeader columnHeader = new ColumnHeader();
        columnHeader.Text = title;
        ListView.Columns.Add(columnHeader);
    }
    public void CloseWindow()
    {
        if (uiCenterScaleBig != null)
        {
            uiCenterScaleBig.ExitAnimation(() => {
                DestryGrid();
            });
        }
    }

    public void DestryGrid()
    {
        MaskManager.Instance.Hide();

        if (gameObject != null)
        {
            GameObject.Destroy(gameObject);
        }
       
    }


    public void SetDataSource(List<List<string>> dataSouces)
    {
        ListView listView = GetComponentInChildren<ListView>();

        foreach (List<string> item in dataSouces)
        {
            string[] subItemTexts = item.ToArray();
            ListViewItem _item = new ListViewItem(subItemTexts);
            listView.Items.Add(_item);
        }

        listView.GetComponent<Image>().color = listView.DefaultHeadingBackgroundColor;
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
}
