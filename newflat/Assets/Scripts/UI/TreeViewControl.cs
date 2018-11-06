using Core.Common.Logging;
using DataModel;
using State;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
    /// <summary>
    /// In this demo we use game objects hierarchy as data source (each data item is game object)
    /// You can use any hierarchical data with treeview.
    /// </summary>
    public class TreeViewControl : MonoBehaviour
    {
        private static ILog log = LogManagers.GetLogger("TreeViewControl");
        public TreeView TreeView;
        public static TreeViewControl Instance;
        private void Awake()
        {
            Instance = this;
        }

        private List<Object3dItem> datas = null;
        public void Init()
        {
            if (!TreeView)
            {
                Debug.LogError("Set TreeView field");
                return;
            }

            Object3dItem object3dItem = new Object3dItem();
            object3dItem.name = "园区";
            object3dItem.id = "";
            object3dItem.type = DataModel.Type.Area;
            object3dItem.childs = SceneData.GetAllWq();
            datas = new List<Object3dItem>();
            datas.Add(object3dItem);

            //subscribe to events
            TreeView.ItemDataBinding += OnItemDataBinding;
            TreeView.SelectionChanged += OnSelectionChanged;
            TreeView.ItemExpanding += OnItemExpanding;
            //Bind data items
          
        }

        public void SetBrowserData()
        {
            EquipmentData.GetEquipmentListByParentId("", (list) => {
                SetEquipmentData(datas);
                TreeView.Items = datas;
            });
        }

        public void SetEditData()
        {
            EquipmentData.GetEquipmentListByParentId("", (list) => {

                TreeView.Items = datas;
            });
        }

        /// <summary>
        /// 绑定设备的数据
        /// </summary>
        /// <param name="datas"></param>
        private void SetEquipmentData(List<Object3dItem>  datas)
        {

            foreach(Object3dItem item in datas)
            {
                if (item.type != DataModel.Type.Equipment && item.type != DataModel.Type.None)
                {
                    EquipmentData.GetEquipmentListByParentId(item.id, (list) =>
                    {

                       // Debug.Log(list.Count);
                       if(list==null)
                        {
                            list = new List<Object3dItem>();
                        }
                        if(item.childs!=null && item.childs.Count > 0)
                        {
                            list.AddRange(item.childs);
                        }
                        item.childs = list;

                        if(item.childs!=null && item.childs.Count>0)
                        {
                            SetEquipmentData(item.childs);
                        }
                    });
                }
            }
        }

     
        private void OnDestroy()
        {
            if (!TreeView)
            {
                return;
            }

            TreeView.ItemDataBinding -= OnItemDataBinding;
            TreeView.SelectionChanged -= OnSelectionChanged;
            TreeView.ItemExpanding -= OnItemExpanding;
        
           
        }

        private void OnItemExpanding(object sender, ItemExpandingArgs e)
        {

            //get parent data item (game object in our case)
            Object3dItem object3dItem = (Object3dItem)e.Item;
            //Debug.Log(object3dItem.name);
            if(object3dItem.childs!=null && object3dItem.childs.Count>0)
            {
                e.Children = object3dItem.childs;
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedArgs e)
        {

            //Do something on selection changed (just syncronized with editor's hierarchy for demo purposes)
            // UnityEditor.Selection.objects = e.NewItems.OfType<GameObject>().ToArray();

            Object3dItem[] object3dItems = e.NewItems.OfType<Object3dItem>().ToArray();
            if(object3dItems.Length>0)
            {
                Switch(object3dItems[0]);
            }
            else
            {
                log.Error("selectItem not found");
            }
           
        }

        private void Switch(Object3dItem object3dItem)
        {
            DataModel.Type t = object3dItem.type;

            switch(t)
            {
                case (DataModel.Type.Area):
                {
                    Main.instance.stateMachineManager.SwitchStatus<AreaState>(string.Empty);
                    break;
                }
                case (DataModel.Type.Builder):
                {
                    if(object3dItem.childs!=null  && object3dItem.childs.Count>0  && AppInfo.Platform == BRPlatform.Browser)
                    {
                        Main.instance.stateMachineManager.SwitchStatus<BuilderState>(object3dItem.id);
                    }
                    else
                    {
                       log.Error("buider data is null");
                    }
                       
                    break;
                }
                case (DataModel.Type.Floor):
                {
                    Main.instance.stateMachineManager.SwitchStatus<FloorState>(object3dItem.id);
                    break;
                }
                case (DataModel.Type.Room):
                {
                    Main.instance.stateMachineManager.SwitchStatus<RoomState>(object3dItem.id);
                    break;
                }
                case (DataModel.Type.Equipment):
                {

                    Main.instance.stateMachineManager.LocateEquipment(object3dItem.id, object3dItem.parentid);
                    break;
                }

            }
        }

     

        /// <summary>
        /// This method called for each data item during databinding operation
        /// You have to bind data item properties to ui elements in order to display them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemDataBinding(object sender, TreeViewItemDataBindingArgs e)
        {
           
            Object3dItem dataItem = e.Item as Object3dItem;
          
            if (dataItem != null)
            {
                //We display dataItem.name using UI.Text 
                Text text = e.ItemPresenter.GetComponentInChildren<Text>(true);
                text.text = dataItem.name;

                //Load icon from resources
                Image icon = e.ItemPresenter.GetComponentsInChildren<Image>()[4];
                if(dataItem.type == DataModel.Type.Equipment)
                {
                    icon.sprite = Resources.Load<Sprite>("equipment");
                    
                }else if (dataItem.type == DataModel.Type.Area)
                {
                    icon.sprite = Resources.Load<Sprite>("cube");

                }
                else
                {
                    icon.sprite = Resources.Load<Sprite>("builder");
                }
                icon.GetComponent<RectTransform>().sizeDelta = new Vector2(24, 24);

                Button button = icon.GetComponent<Button>();
                if(button==null)
                {
                    button = icon.gameObject.AddComponent<Button>();
                }

                //And specify whether data item has children (to display expander arrow if needed)
                if (dataItem.name != "TreeView")
                {
                    if(dataItem.childs!=null && dataItem.childs.Count>0)
                    {
                        e.HasChildren = true;
                    }
                    else
                    {
                        e.HasChildren = false;
                    }
                    //  e.HasChildren = dataItem.transform.childCount > 0;
                }
            }

        }

      
    }
}
