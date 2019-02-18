using Endgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public  class SetControlPoint:MonoSingleton<SetControlPoint>,IEventListener
{
    private  GameObject Init()
    {
        GameObject grid = GameObject.Instantiate(Resources.Load<GameObject>("Grid/Control/ControlPoint"));

        grid.transform.Find("Set").GetComponent<Button>().onClick.AddListener(SubmitSetData);
        grid.transform.Find("Title/close").GetComponent<Button>().onClick.AddListener(CloseWindow);
        grid.transform.SetParent(UIUtility.GetRootCanvas());
        grid.transform.localScale = Vector3.zero;
        grid.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        grid.transform.localRotation = Quaternion.identity;
        ListView listView = grid.GetComponentInChildren<ListView>();
        listView.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 290);

        return grid;
    }

   
    private string HorizontalScrollBarName = "HorizontalScrollBar";
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
             Debug.Log(result);
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
      
        grid = Init();
        CreateTitle(equipmentName);
        listView = grid.GetComponentInChildren<ListView>();
        listView.ItemBecameVisible += OnItemBecameVisible;
        listView.ItemBecameInvisible += OnItemBecameInvisible;
    
        AddColumns(listView);
        SetColumWidth(listView);
        SetStyle(listView);
       
        //foreach (EquipmentControlPoint item in ecps)
        //{
        //    string[] subItemTexts = new string[] { "23", item.name, item.describe, item.name, "12" };
        //    ListViewItem _item = new ListViewItem(subItemTexts);
        //    listView.Items.Add(_item);
        //}
        
        foreach(EquipmentControlPointItem ecp in ecps)
        {
            string note = "";
            if(ecp.type==2)
            {
                note = "范围：" + ecp.StartValue + "-" + ecp.endValue;
            }
            string[] subItemTexts = new string[] { ecp.number.ToString(), ecp.name, ecp.number.ToString(),note, "" };
            ListViewItem _item = new ListViewItem(subItemTexts);
            _item.Tag = ecp;
            listView.Items.Add(_item);
        }
        HideHorizontalScrollBar();
        listView.GetComponent<Image>().color = listView.DefaultHeadingBackgroundColor;
        grid.transform.DOScale(Vector3.one, 0.5f);
        isShow = true;

    }

    /// <summary>
    /// set title
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
        ListView.Columns[1].Width = 130;
        ListView.Columns[2].Width = 130;
        ListView.Columns[3].Width = 140;
        ListView.Columns[4].Width = 145;
    }

    private  void AddColumns(ListView ListView)
    {
        ListView.SuspendLayout();
        {
            AddColumnHeader(ListView, "选择");
            AddColumnHeader(ListView, "名称");
            AddColumnHeader(ListView, "编号");
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

        var setUI = item.SubItems[4];
        EquipmentControlPointItem ecp = item.Tag as EquipmentControlPointItem;
        GameObject target = null;
        //输入型
        if(ecp.type != 1)
        {
            target = GameObject.Instantiate(Resources.Load("Grid/Control/Input")) as GameObject;
            //字符串
            if(ecp.type == 0)
            {
                target.GetComponent<InputField>().characterValidation = InputField.CharacterValidation.Name;
            }
            else
            {
                target.GetComponent<InputField>().characterValidation = InputField.CharacterValidation.Decimal;
            }
           
        }
        //枚举类型
        else
        {
            target = GameObject.Instantiate(Resources.Load("UI/DropDownList")) as GameObject;
            SetDropDownListDataSource(target.GetComponent<DropDonwListUI>(), ecp);
        }
       
        setUI.CustomControl = target.transform as RectTransform;
       
    }

    private void SetDropDownListDataSource(DropDonwListUI dorpUI, EquipmentControlPointItem ecp)
    {
       // string values = ecp.values;
        string[] valueArray = ecp.values.Split(',');
        string describe = ecp.describes;

        string[] describeArray = ecp.describes.Split(',');
        List<DropDownItem> dropDownList = new List<DropDownItem>();
        for(int i=0;i< valueArray.Length;i++)
        {
            DropDownItem item = new DropDownItem();
            item.id = valueArray[i];
            item.name = describeArray[i];
            dropDownList.Add(item);
        }
        dorpUI.InitData(dropDownList);
    }


    private  void OnItemBecameInvisible(ListViewItem item)
    {
        var Check = item.SubItems[0];
        GameObject CheckItemGameObject = Check.CustomControl.gameObject;


        var inputItem = item.SubItems[4];
        GameObject inputItemGameObject = inputItem.CustomControl.gameObject;

        GameObject.Destroy(CheckItemGameObject);
        GameObject.Destroy(inputItemGameObject);
    }

    /// <summary>
    /// S0-C0-E0-S0-VA | S0-C0-E0-R0 & aaaaa & 度 & admin & 2019-01-08 11:11:11
    /// </summary>
    public void SubmitSetData()
    {
        ListView.ListViewItemCollection list = listView.Items;


        List<string> result = new List<string>();
        for(int i=0;i< list.Count;i++)
        {
            ListViewItem item = list[i];
            var Check = item.SubItems[0];
            GameObject CheckItemGameObject = Check.CustomControl.gameObject;
            if (CheckItemGameObject.GetComponent<Toggle>().isOn)
            {
                var setUI = item.SubItems[4];
                GameObject inputItemGameObject = setUI.CustomControl.gameObject;
                EquipmentControlPointItem ecp = item.Tag as EquipmentControlPointItem;
                string inputValue = string.Empty;
                //输入型
                if(ecp.type!=1)
                {
                    inputValue = inputItemGameObject.GetComponent<InputField>().text;

                    if(string.IsNullOrEmpty(inputValue))
                    {
                        continue;
                    }
                    //数据范围检测
                    if(ecp.type ==2)
                    {
                        bool isCheck = CheckRange(inputValue, ecp.StartValue, ecp.endValue);
                        if (!isCheck)
                        {
                            continue;
                        }
                    }
                }
                //枚举类型
                else
                {
                    inputValue = inputItemGameObject.GetComponent<DropDonwListUI>().SelectId;
                    //please select
                    if (string.IsNullOrEmpty(inputValue))
                    {
                        continue;
                    }
                }

               
                string str = ecp.unnumber + "|" + ecp.number + "&" + inputValue + "& " + ecp.unit + "&" + UserLogin.loginUserName + "&" + TimeUtility.FormatCurrentDate();
                Debug.Log(str);
                result.Add(str);
            }
        }

        ///Debug.Log(FormatUtil.ConnetString(result,","));
       // WebsocjetService.Instance.SendData(FormatUtil.ConnetString(result, ","));
        UIUtility.ShowTips("下发成功");
        CloseWindow();
       
    }


    private bool CheckRange(string inputValue,float minValue, float maxValue)
    {
        if(float.Parse( inputValue)>= minValue && float.Parse(inputValue) <= maxValue)
        {
            return true;
        }

        return false;

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
        if(eventName.Equals(EventName.DeleteObject))
        {
            DestryGrid();
        }
        return true;
    }
}
