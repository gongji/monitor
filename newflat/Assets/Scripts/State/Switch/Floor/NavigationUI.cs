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
            ClickBuiding();
        });
	}
	
    private string id  =string.Empty;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="DataSouce"></param>
    /// <param name="parent"></param>
    /// <param name="currentData"></param>
    /// <param name="frontname">下边按钮的前缀</param>
    /// <param name="backName">第一个按钮的名字</param>
    public void CreateNavagitionList(List<Object3dItem> DataSouce,Transform parent, Object3dItem currentData, string frontname,string backName)
    {
        this.id = currentData.id;
        backButton.GetComponentInChildren<Text>().text = backName;
        foreach (Object3dItem object3dItem in DataSouce)
        {
            if(object3dItem.type == Type.Equipment)
            {
                continue;
            }
            GameObject cloneObject = GameObject.Instantiate(item);
            cloneObject.SetActive(true);
            cloneObject.transform.parent = parent.GetComponentInChildren<VerticalLayoutGroup>().transform;
            cloneObject.GetComponentInChildren<Text>().text = frontname + object3dItem.code.Substring(object3dItem.code.Length - 1, 1);
            cloneObject.GetComponentInChildren<Text>().fontSize = 15;
            cloneObject.name = object3dItem.id;
            if(object3dItem.id.Equals(currentData.id))
            {
                SetSelect(cloneObject.GetComponentInChildren<Button>());
            }
            cloneObject.GetComponentInChildren<Button>().onClick.AddListener(()=> {

                OnClickFloor(cloneObject);
            });

           
        }
    }

    /// <summary>
    /// 楼层的ui切换
    /// </summary>
    /// <param name="sender"></param>
    private void OnClickFloor(GameObject sender)
    {
        //log.Debug(sender.name);
        //log.Debug(currentData.id);
        
        //if ( sender.name.Equals(id))
        //{
        //    log.Debug(sender.name +  "is same ");
        //    return;
        //}
        ClearAllSelect();
        SetSelect(sender.GetComponentInChildren<Button>());
        id = sender.name;
        IState currentstate = Main.instance.stateMachineManager.mCurrentState;
        if(currentstate is FloorState)
        {
            Main.instance.stateMachineManager.SwitchStatus<FloorState>(sender.name);
        }
        else if(currentstate is RoomState)
        {
            Main.instance.stateMachineManager.SwitchStatus<RoomState>(sender.name);
        }

        
    }
       


    private void ClearAllSelect()
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
            button.GetComponent<Image>().color = new Color32(38, 44, 85, 255);
        }
    }
   
    private void SetSelect(Button button)
    {
        //ColorBlock cb = new ColorBlock();
        //cb.normalColor = new Color32(132, 142, 166, 255);
        //cb.highlightedColor = new Color32(132, 142, 166, 255);
        //cb.pressedColor = button.colors.pressedColor;
        //cb.disabledColor = button.colors.disabledColor;
        //cb.colorMultiplier = 1;
        //button.colors = cb;
        button.GetComponent<Image>().color = new Color32(132, 142, 166, 255); 
    }

    /// <summary>
    /// 返回的上一级
    /// </summary>
    private void ClickBuiding()
    {
        Object3dItem curentdata = SceneData.FindObjUtilityect3dItemById(id);
        Object3dItem parentObject = SceneData.FindObjUtilityect3dItemById(curentdata.parentid);

        IState currentstate = Main.instance.stateMachineManager.mCurrentState;
        if (currentstate is FloorState)
        {
            Main.instance.stateMachineManager.SwitchStatus<BuilderState>(parentObject.id);
        }
        else if (currentstate is RoomState)
        {
            Main.instance.stateMachineManager.SwitchStatus<FloorState>(parentObject.id);
        }

        
    }

}
