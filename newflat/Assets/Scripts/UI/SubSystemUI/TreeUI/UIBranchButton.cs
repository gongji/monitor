/***********************************
    **  日期:
    **  姓名:jss
    **  审阅:jss
    **  功能:
    **  备注:
************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Tree;
using System.Linq;

public class UIBranchButton : MonoBehaviour
{
    private TreeManager _treeManager;

    public TreeManager treeManager
    {
        get
        {
            if (_treeManager == null)
                _treeManager = Tree.Constant.TraverseParentFind<TreeManager>(this.transform);
            return _treeManager;
        }
    }
    //子集
    public List<GameObject> childs = new List<GameObject>();
    public Vector2 size = new Vector2(100, 50);
    //归属树的层级
    public int treeIndex;
    //id
    public string id;
    //文本
    public Text label;

    public string type;

    public delegate void OnBranchBtnClicked(object param);
    //点击的回调
    public OnBranchBtnClicked onBranchBtnClicked;
    //子树节点
    public GameObject grid;
    //方向
    public int lf;
    //上移
    public GameObject arrow1;
    //下移
    public GameObject arrow2;
    //序号
    public int sn;

    void Awake()
    {
        this.GetComponent<Toggle>().onValueChanged.AddListener(OnClicked);
    }

    void Start()
    {
        if (treeManager != null)
            onBranchBtnClicked = treeManager.OnBranchButtonClicked;
    }

 
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void ReadChild(SubSystemItem data)
    {
        // Dictionary<string,object> dic = data as Dictionary<string,object>;
        string name = data.name;
        label.text = name;
        grid = Tree.Constant.CreateBranchGrid();
        grid.transform.SetParent(this.transform);
        grid.transform.localScale = Vector3.one;
        UITreeGrid treeGrid = grid.transform.GetComponent<UITreeGrid>();
        treeGrid.index = Tree.Constant.TraverseParentFind<UITreeGrid>(this.transform).index + 1;
        treeIndex = Tree.Constant.TraverseParentFind<UITreeGrid>(this.transform).index;
        if (data.childs != null && data.childs.Count>0)
        {
            List<SubSystemItem> _childs = data.childs;
//            float p = (float)(transform.GetSiblingIndex()) / transform.parent.childCount;


            lf = treeManager.lf_anchor == TreeManager.LF_Anchor.right ? 1 : -1;
            for (int i = 0; i < _childs.Count; i++)
            {
                GameObject temp = Tree.Constant.CreateBranchButton();
                temp.transform.SetParent(grid.transform);
                UIBranchButton _bbtn = temp.GetComponent <UIBranchButton>();
                _bbtn.ReadChild(_childs[i]);
                _bbtn.Hide();
                _bbtn.id = _childs[i].id;
                _bbtn.sn = i;
                _bbtn.type = _childs[i].type;
                temp.name = _childs[i].name;
                temp.GetComponent <Toggle>().group = grid.GetComponent<ToggleGroup>();
                childs.Add(temp);
                treeGrid.childs.Add(temp);
            }
            if (_childs.Count > treeManager.MaxCount)
            {
                arrow1 = Tree.Constant.CreateBranchArrowUp();
                arrow1.transform.SetParent(grid.transform);
                arrow1.transform.SetAsFirstSibling();
                arrow1.SetActive(false);
                arrow1.GetComponent<Button>().onClick.AddListener(treeGrid.OnArrowUpClicked);
                arrow2 = Tree.Constant.CreateBranchArrowDown();
                arrow2.transform.SetParent(grid.transform);
                arrow2.transform.SetAsLastSibling();
                arrow2.SetActive(false);
                arrow2.GetComponent<Button>().onClick.AddListener(treeGrid.OnArrowDownClicked);
                treeGrid.childs.Add(arrow1);
                treeGrid.childs.Add(arrow2);

                childs.Add(arrow1);
                childs.Add(arrow2);
            }
        }
    }

    public void OnClicked(bool p)
    {
        //点击树的最顶层
        if (!p)
            ShowChilds(false);
        else
            ShowChilds(true);
        if (onBranchBtnClicked != null)
            onBranchBtnClicked(this);

        grid.GetComponent<UITreeGrid>().OnInit(treeManager);
        Vector3 vv = treeManager.grid.transform.InverseTransformPoint(this.transform.position);
        //        Debug.Log("vv:" + vv);
        int activeCount = 0;
        childs.ForEach(o =>
            {
                if (o.activeSelf)
                    activeCount++;
            });
        float lenght = this.GetComponent<RectTransform>().sizeDelta.y / 2 - vv.y + activeCount * size.y;
        int arrow = lenght > treeManager.MaxHeight ? 1 : -1;
        grid.transform.localPosition = new Vector3(lf * size.x, arrow * (activeCount - 1) * size.y / 2);
        //        Debug.Log(this.transform.localPosition + "-lenght:" + lenght + "------" + treeManager.MaxHeight);
    }

    public void ShowChilds(bool p)
    {
        if (arrow1 != null)
            arrow1.SetActive(p);
        if (arrow2 != null)
            arrow2.SetActive(p);
        if (!p)
        {
            Tree.Constant.TraverseChild(this.transform, o =>
                {
                    UIBranchButton temp = o.GetComponent<UIBranchButton>();
                    if (temp != null)
                    {
                        temp.GetComponent<Toggle>().isOn = false;
                        temp.Hide();
                    }
                });
        }
        else
        {
            for (int i = 0; i < childs.Count; i++)
            {
                if (childs[i].GetComponent<UIBranchButton>() != null)
                    childs[i].GetComponent<UIBranchButton>().Show();
            }
        }
            
    }
}
