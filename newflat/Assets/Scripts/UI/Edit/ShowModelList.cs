using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using SystemCore.Task;

/// <summary>
/// 显示模型列表数据
/// </summary>
public class ShowModelList : MonoBehaviour {

    public static ShowModelList instance;
    private List<ModelCategory> list = null;
    private void Awake()
    {
        instance = this;
    }
    void Start () {
        EditModelListData.GetModelListData((list) => {
            this.list = list;
            CreateModelList(list);
        });
    }
	
	
    private void  CreateModelList(List<ModelCategory> list)
    {

        Transform topCategory = transform.Find("1level");

        Transform modelList = transform.Find("2level");

     
        Transform topItem = topCategory.Find("item");
       
        ///创建一级菜单
       for (int i=0;i< list.Count;i++)
        {
            GameObject clone = GameObject.Instantiate(topItem.gameObject);
            clone.transform.SetParent(topItem.parent);
            clone.transform.localScale = Vector3.one;
            clone.SetActive(true);
            clone.name = list[i].id;
            clone.GetComponent<Button>().onClick.AddListener(delegate () { TopLevelClick(clone); });

            if (i==0)
            {
                TopLevelClick(clone);
            }
            string url = Config.parse("downPath") + "/icon/"+ list[i].icon;
            ResourceUtility.Instance.GetHttpTexture(url, (texture) => {
                Sprite sprite = UIUtility.GetSpriteByTexture((Texture2D)texture);
                clone.GetComponent<Image>().sprite = sprite;
            });
         
        }
    }


    private Dictionary<GameObject, List<ModelCategory>> dic = new Dictionary<GameObject, List<ModelCategory>>();
    /// <summary>
    /// 一级的菜单选中
    /// </summary>
    /// <param name="clickGameObject"></param>
    private void TopLevelClick(GameObject clickGameObject)
    {
        //一级菜单的选中
        SetSelectTopEffection(clickGameObject.transform.parent, clickGameObject);

        var result = from o in list where o.id.Equals(clickGameObject.name) select o;
        if(result.Count()>0)
        {
            ModelCategory mc = result.ToList<ModelCategory>()[0];
            List<ModelCategory> categoryList = mc.childs;
            Transform item = transform.Find("2level").Find("item");
            RemoveTwoLevel();
            dic.Clear();
            for (int i=0; categoryList!=null && i < categoryList.Count;i++)
            {
                
                GameObject cloneCategory = GameObject.Instantiate<GameObject>(item.gameObject);
                cloneCategory.SetActive(cloneCategory);
                cloneCategory.transform.SetParent(item.parent);
                cloneCategory.transform.localScale = Vector3.one;
                cloneCategory.GetComponentInChildren<Text>().text = categoryList[i].name;
                cloneCategory.name = "categorycolone";
                dic.Add(cloneCategory, categoryList[i].childs);
                cloneCategory.GetComponent<Button>().onClick.AddListener(delegate () { OnSecondClick(cloneCategory); });

                if(i==0)
                {
                    OnSecondClick(cloneCategory);
                }
               
            }
        }

      
    }

    private void RemoveTwoLevel()
    {
        Transform level2 = transform.Find("2level");

        foreach(Transform child in level2)
        {
            if(child.gameObject.activeSelf)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    /// <summary>
    /// 二级菜单的点击
    /// </summary>
    /// <param name="secondGameObject"></param>
    private void OnSecondClick(GameObject secondGameObject)
    {
        List<ModelCategory> modelList = dic[secondGameObject];
        Transform model = transform.Find("2level/modelList");
       
        //每次创建之前先删除

        foreach(Transform child in model.transform.parent)
        {
            if(child.name. Equals("model"))
            {
                GameObject.Destroy(child.gameObject);
                break;
            }
        }
        
        GameObject modelPanelColone = GameObject.Instantiate<GameObject>(model.gameObject);
        modelPanelColone.SetActive(true);
        modelPanelColone.transform.SetParent(model.transform.parent);
        modelPanelColone.transform.localScale = Vector3.one;
        modelPanelColone.name = "model";
        modelPanelColone.transform.SetSiblingIndex(secondGameObject.transform.GetSiblingIndex()+1);
       
        //行数

        Transform modelItem = modelPanelColone.transform.Find("item");
        float height = 0;
        foreach (ModelCategory itemData in modelList)
        {
            //创建模型的对象
            GameObject coloneItem = GameObject.Instantiate<GameObject>(modelItem.gameObject);
            coloneItem.SetActive(true);
            coloneItem.transform.SetParent(modelItem.parent);
            coloneItem.transform.localScale = Vector3.one;
            coloneItem.name = itemData.modelid;
            coloneItem.GetComponent<Button>().onClick.AddListener(delegate () { OnThreeClick(coloneItem); });
            string url = Config.parse("downPath") + "/icon/" + itemData.icon;
            ResourceUtility.Instance.GetHttpTexture(url, (texture) => {
                Sprite sprite = UIUtility.GetSpriteByTexture((Texture2D)texture);
                coloneItem.GetComponent<Image>().sprite = sprite;
            });

            height = coloneItem.GetComponent<RectTransform>().sizeDelta.y;

            
        }

        int rowCount = Mathf.CeilToInt(modelList.Count / 2.0f);
        Vector2 panelVector2 = modelPanelColone.GetComponent<RectTransform>().sizeDelta;
        modelPanelColone.GetComponent<RectTransform>().sizeDelta = new Vector2(panelVector2.x, 66 * rowCount + (rowCount - 1) * 5.5f);
    }


     
    /// <summary>
    /// 选中一级菜单的效果
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="selectGameObject"></param>
    private  void SetSelectTopEffection(Transform parent, GameObject selectGameObject)
    {
        Outline outLine = parent.GetComponentInChildren<Outline>();
        if(outLine!=null)
        {
            GameObject.Destroy(outLine);

        }
        foreach (Transform child in parent)
        {
            child.GetComponent<Image>().color = Color.white;
             
        }
        
        outLine = selectGameObject.gameObject.AddComponent<Outline>();
        outLine.effectColor = Color.black;
        outLine.effectDistance = new Vector2(3,3);
        selectGameObject.GetComponent<Image>().color = new Color32(95,156,221,255);



    }

    public GameObject prefebGameObject;
    private GameObject selectThreeGameObject;
    /// <summary>
    /// 三级菜单的创建
    /// </summary>
    /// <param name="threeItem"></param>
    public void OnThreeClick(GameObject threeItem)
    {
        selectThreeGameObject = threeItem;
        //颜色复位
        foreach (Transform child in selectThreeGameObject.transform.parent)
        {
            child.GetComponent<Image>().color = Color.white;
        }
        threeItem.GetComponent<Image>().color = new Color32(95, 156, 221, 255);

        string modelid = threeItem.name;


        if(prefebGameObject!=null)
        {
            GameObject.Destroy(prefebGameObject);
        }
        if (EquipmentData.modelPrefebDic.ContainsKey(modelid))
        {
            Debug.Log("开始创建");
            CreatePrefeb(modelid);
        }
        else
        {
            StartDownLoad(modelid);
            Debug.Log("未创建");
        }
    }

    private void CreatePrefeb(string modelid)
    {
        
        prefebGameObject = GameObject.Instantiate(EquipmentData.modelPrefebDic[modelid]);
        prefebGameObject.name = modelid;
        prefebGameObject.transform.localScale = Vector3.one * 0.1f;
    }


    private void StartDownLoad(string modelid)
    {
        TaskQueue taskQueue = new TaskQueue(this);

        string downequipmentPath = Config.parse("downPath") + "model/";
        string path = downequipmentPath + modelid;

        ABModelDownloadTask abDownloadTask = new ABModelDownloadTask(path, path, modelid);
        taskQueue.Add(abDownloadTask);
        taskQueue.StartTask();
        //下载完成
        taskQueue.OnFinish = () =>
        {
            EquipmentData.UpdateModelDic(modelid, abDownloadTask);
            CreatePrefeb(modelid);
        };
    }

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            RemoveReset();
        }
        
    }

    public void RemoveReset()
    {
        if(selectThreeGameObject!=null)
        {
            foreach (Transform child in selectThreeGameObject.transform.parent)
            {
                child.GetComponent<Image>().color = Color.white;
            }
            if (prefebGameObject != null)
            {
                GameObject.Destroy(prefebGameObject);

            }
        }
        
    }




}
