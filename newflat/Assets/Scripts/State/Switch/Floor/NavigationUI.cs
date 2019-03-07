using Core.Common.Logging;
using DataModel;
using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 生成导航的层ui，并切换到大楼和层
/// </summary>
public  class NavigationUI:MonoBehaviour
{

    private static ILog log = LogManagers.GetLogger("FloorNavigationUI");

    public GameObject item;

    public Button backButton;

	void Start () {
        backButton.onClick.AddListener(()=> {
            ClickBack();
        });
	}

    private int fontSize = 13;
    private string id  =string.Empty;
    /// <summary>
    /// 楼层和房间
    /// </summary>
    /// <param name="DataSouce"></param>
    /// <param name="parent"></param>
    /// <param name="currentData"></param>
    /// <param name="frontname">下边按钮的前缀</param>
    /// <param name="backName">第一个按钮的名字</param>
    public void CreateFloorRoomNavagitionList(List<Object3dItem> DataSouce,Transform parent, Object3dItem currentData, 
        string frontname,string backName,bool isFullArea =false)
    {
        this.id = currentData.id;
        backButton.GetComponentInChildren<Text>().text = backName;
        backButton.GetComponentInChildren<Text>().fontSize = fontSize;
        foreach (Object3dItem object3dItem in DataSouce)
        {
            if(object3dItem.type.ToString().StartsWith(Constant.Equipment_Prefix))
            {
                continue;
            }
            GameObject cloneObject = GameObject.Instantiate(item);
            cloneObject.SetActive(true);
            // cloneObject.transform.parent.SetParent( parent.GetComponentInChildren<VerticalLayoutGroup>().transform);
            Transform _parent = parent.GetComponentInChildren<VerticalLayoutGroup>().transform;
            cloneObject.transform.SetParent(_parent);

            cloneObject.transform.localScale = Vector3.one;
            //if(!isFullArea)
            //{
            //    cloneObject.GetComponentInChildren<Text>().text = frontname + object3dItem.number.Substring(object3dItem.number.Length - 1, 1);
            //}
            //else
            //{
              cloneObject.GetComponentInChildren<Text>().text = object3dItem.name;
           // }
           
            cloneObject.GetComponentInChildren<Text>().fontSize = fontSize;
            cloneObject.name = object3dItem.id;
            if(object3dItem.id.Equals(currentData.id))
            {
                SetSelectEffection(cloneObject.GetComponentInChildren<Button>());
            }
            else
            {
                cloneObject.GetComponentInChildren<Button>().onClick.AddListener(() => {

                    OnClickSelected(cloneObject);
                });
            }
            

           
        }
    }


   /// <summary>
   /// 生成建筑的组索引
   /// </summary>
   /// <param name="currentData"></param>
   /// <param name="floorIndexs"></param>
   /// <param name="currentIndex"></param>
   /// <param name="parent"></param>
    public void CreateBuiderNavagitionList(Object3dItem builderCurrentData,int floorIndexCount,int currentIndex, Transform parent)
    {
        this.id = builderCurrentData.id;
        backButton.GetComponentInChildren<Text>().text = "返回";
        backButton.GetComponentInChildren<Text>().fontSize = fontSize;
        for (int i=0;i< floorIndexCount;i++)
        {
          
            GameObject cloneObject = GameObject.Instantiate(item);
            cloneObject.SetActive(true);
            cloneObject.transform.parent = parent.GetComponentInChildren<VerticalLayoutGroup>().transform;
            cloneObject.transform.localScale = Vector3.one;
            cloneObject.GetComponentInChildren<Text>().text = (i + 1).ToString();
            cloneObject.GetComponentInChildren<Text>().fontSize = fontSize;
            cloneObject.name = i.ToString();
            //选中颜色
            if (i == currentIndex)
            {
                SetSelectEffection(cloneObject.GetComponentInChildren<Button>());
            }
            else
            {
                cloneObject.GetComponentInChildren<Button>().onClick.AddListener(() => {

                    OnClickSelected(cloneObject);
                });
            }
            


        }
    }
    /// <summary>
    /// 楼层的ui切换
    /// </summary>
    /// <param name="sender"></param>
    private void OnClickSelected(GameObject sender)
    {
        //log.Debug(sender.name);
        //log.Debug(currentData.id);

        //if ( sender.name.Equals(id))
        //{
        //    log.Debug(sender.name +  "is same ");
        //    return;
        //}
        SelectReset();
        SetSelectEffection(sender.GetComponentInChildren<Button>());
        IState currentstate = Main.instance.stateMachineManager.mCurrentState;
        if(currentstate is FloorState)
        {
            Main.instance.stateMachineManager.SwitchStatus<FloorState>(sender.name);
        }
        else if(currentstate is RoomState)
        {
            Main.instance.stateMachineManager.SwitchStatus<RoomState>(sender.name);
        }
        else if(currentstate is BuilderState)
        {
            Main.instance.stateMachineManager.SwitchStatus<BuilderState>(this.id,null, int.Parse(sender.name));
        }
        else if(currentstate is ColorAreaState)
        {
            Main.instance.stateMachineManager.SwitchStatus<ColorAreaState>("-1", null, 0,sender.name);
        }
        else if(currentstate is FullAreaState)
        {
            Main.instance.stateMachineManager.SwitchStatus<FullAreaState>("-1", null, 0, sender.name);
        }

        
    }
       


    private void SelectReset()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach(Button button in buttons)
        {
            //ColorBlock cb = new ColorBlock();
            //cb.normalColor = Color.white;
            //cb.highlightedColor = Color.white;
            //cb.pressedColor = button.colors.pressedColor;
            //cb.disabledColor = button.colors.disabledColor;
            //cb.colorMultiplier = 1;
            //button.colors = cb;
            button.GetComponent<Image>().color = new Color32(94, 98, 99, 255);
        }
    }
   
    private void SetSelectEffection(Button button)
    {
        //ColorBlock cb = new ColorBlock();
        //cb.normalColor = new Color32(132, 142, 166, 255);
        //cb.highlightedColor = new Color32(132, 142, 166, 255);
        //cb.pressedColor = button.colors.pressedColor;
        //cb.disabledColor = button.colors.disabledColor;
        //cb.colorMultiplier = 1;
        //button.colors = cb;
        button.GetComponent<Image>().color = new Color32(6, 148, 255, 255); 
    }

    /// <summary>
    /// 返回的上一级
    /// </summary>
    private void ClickBack()
    {
        Object3dItem curentdata = SceneData.FindObjUtilityect3dItemById(id);
        Object3dItem parentObject = SceneData.FindObjUtilityect3dItemById(curentdata.parentsId);

        IState currentstate = Main.instance.stateMachineManager.mCurrentState;
        if (currentstate is FloorState)
        {
            //Main.instance.stateMachineManager.SwitchStatus<BuilderState>(parentObject.id);
            Main.instance.stateMachineManager.SwitchStatus<FullAreaState>("-1", null, 0, parentObject.id);
        }
        else if (currentstate is RoomState)
        {
            Main.instance.stateMachineManager.SwitchStatus<FloorState>(parentObject.id);
        }
        else if(currentstate is BuilderState || currentstate is ColorAreaState || currentstate is FullAreaState)
        {
            SceneJump.JumpFirstPage();
        }
    }

    private void Update()
    {
        if(AppInfo.Platform == BRPlatform.Browser && Input.GetKeyUp(KeyCode.Escape))
        {
            if(CameraInitSet.IsBackDefaultPosition())
            {
                CameraInitSet.BackDefaultPostion();
            }
            else
            {
                ClickBack();
            }
           
        }
    }

}
