using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using SystemCore.Task;
/// <summary>
/// editor righttop modelList and property
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
       
       for (int i=0; list!=null && i < list.Count;i++)
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
            string url =  list[i].icon;
            ResourceUtility.Instance.GetHttpTexture(url, (texture) => {
                Sprite sprite = UIUtility.GetSpriteByTexture((Texture2D)texture);
                clone.GetComponent<Image>().sprite = sprite;
            });
         
        }
    }


    private Dictionary<GameObject, List<ModelCategory>> twoLeveTypeldic = new Dictionary<GameObject, List<ModelCategory>>();
    /// <summary>
    /// onelevel
    /// </summary>
    /// <param name="clickGameObject"></param>
    private void TopLevelClick(GameObject oneLevelSelectObject)
    { 
        SetSelectTopEffection(oneLevelSelectObject.transform.parent, oneLevelSelectObject);

        var result = from o in list where o.id.Equals(oneLevelSelectObject.name) select o;
        if(result.Count()>0)
        {
            ModelCategory mc = result.ToList<ModelCategory>()[0];
            List<ModelCategory> categoryList = mc.childs;
            Transform item = transform.Find("2level").Find("item");
            RemoveTwoLevel();
            twoLeveTypeldic.Clear();
            for (int i=0; categoryList!=null && i < categoryList.Count;i++)
            {
                
                GameObject cloneCategory = GameObject.Instantiate<GameObject>(item.gameObject);
                cloneCategory.SetActive(cloneCategory);
                cloneCategory.transform.SetParent(item.parent);
                cloneCategory.transform.localScale = Vector3.one;
                cloneCategory.GetComponentInChildren<Text>().text = categoryList[i].name;
                cloneCategory.name = "categorycolone";
                twoLeveTypeldic.Add(cloneCategory, categoryList[i].childs);
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
    private Dictionary<GameObject, ModelCategory> modelDataDic = new Dictionary<GameObject, ModelCategory>();
    /// <summary>
    ///two level click
    /// </summary>
    /// <param name="secondGameObject"></param>
    private void OnSecondClick(GameObject secondGameObject)
    {

        List<ModelCategory> modelList = twoLeveTypeldic[secondGameObject];
        Transform model = transform.Find("2level/modelList");
    
        foreach(Transform child in model.transform.parent)
        {
            if(child.name. Equals("model"))
            {
                GameObject.Destroy(child.gameObject);
                break;
            }
        }
        modelDataDic.Clear();
        GameObject modelPanelColone = GameObject.Instantiate<GameObject>(model.gameObject);
        modelPanelColone.SetActive(true);
        modelPanelColone.transform.SetParent(model.transform.parent);
        modelPanelColone.transform.localScale = Vector3.one;
        modelPanelColone.name = "model";
        modelPanelColone.transform.SetSiblingIndex(secondGameObject.transform.GetSiblingIndex()+1);
      
        Transform modelItem = modelPanelColone.transform.Find("item");
        float height = 0;
        foreach (ModelCategory itemData in modelList)
        { 
            GameObject modelItemObject = GameObject.Instantiate<GameObject>(modelItem.gameObject);
            modelDataDic.Add(modelItemObject, itemData);
            modelItemObject.SetActive(true);
            modelItemObject.transform.SetParent(modelItem.parent);
            modelItemObject.transform.localScale = Vector3.one;
            modelItemObject.name = itemData.path;
            modelItemObject.GetComponent<Button>().onClick.AddListener(delegate () { OnThreeClick(modelItemObject); });
            string url =  itemData.icon;
            ResourceUtility.Instance.GetHttpTexture(url, (texture) => {
                Sprite sprite = UIUtility.GetSpriteByTexture((Texture2D)texture);
                modelItemObject.GetComponent<Image>().sprite = sprite;
            });
            height = modelItemObject.GetComponent<RectTransform>().sizeDelta.y;
        }

        int rowCount = Mathf.CeilToInt(modelList.Count / 2.0f);
        Vector2 panelVector2 = modelPanelColone.GetComponent<RectTransform>().sizeDelta;
        modelPanelColone.GetComponent<RectTransform>().sizeDelta = new Vector2(panelVector2.x, 66 * rowCount + (rowCount - 1) * 5.5f);
    }

    /// <summary>
    /// one level effection
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
 
    public void OnThreeClick(GameObject threeItem)
    {
        selectThreeGameObject = threeItem;
        
        foreach (Transform child in selectThreeGameObject.transform.parent)
        {
            child.GetComponent<Image>().color = Color.white;
        }
        threeItem.GetComponent<Image>().color = new Color32(95, 156, 221, 255);
        ModelCategory mc = modelDataDic[threeItem];
       
        if(prefebGameObject!=null)
        {
            GameObject.Destroy(prefebGameObject);
        }
        if (ModelData.modelPrefebDic.ContainsKey(mc.id))
        {
            Debug.Log("开始创建");
            CreatePrefeb(mc);
        }
        else
        {
            StartDownLoad(mc);
            Debug.Log("未创建");
        }
    }

    private void CreatePrefeb(ModelCategory mc)
    {
        prefebGameObject = GameObject.Instantiate(ModelData.modelPrefebDic[mc.id]);
        prefebGameObject.name = mc.id+","+ mc.name;
        prefebGameObject.transform.localScale = Vector3.one;
       
        OperateTips.instance.Show("单击左键生成设备，双击左键或者按ESC键取消操作");
    }


    private void StartDownLoad(ModelCategory mc)
    {
        DownLoader.Instance.StartModelDownLoad(mc, () => {

            CreatePrefeb(mc);
        });
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
                OperateTips.instance.HideTips();

            }
        }
    }
}
