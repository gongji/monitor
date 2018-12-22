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

public class TreeManager : MonoBehaviour
{
    public enum LF_Anchor
    {
        left = 1,
        right = -1
    }
    //设置的最长的值
    public float MaxHeight;
    //一级菜单总长度
    public float TopHeight;
    //子树对齐方向
    public LF_Anchor lf_anchor = LF_Anchor.right;
    //超出这个值就显示滚动功能
    [SerializeField]
    private int maxCount;
    public int MaxCount
    {
        get
        {
            return maxCount;
        }
    }

    [HideInInspector]
    public GameObject grid;
    //上移
    public GameObject arrow1;
    //下移
    public GameObject arrow2;
    // Use this for initialization
    public void Init(List<SubSystemItem> list)
    {
        grid = Tree.Constant.CreateBranchGrid();
        grid.transform.SetParent(this.transform);
        grid.transform.localScale = Vector3.one;
        grid.GetComponent<RectTransform>().anchoredPosition =new Vector2(50,0);
        grid.GetComponent<UITreeGrid>().index = 1;
        UITreeGrid treeGrid = grid.transform.GetComponent<UITreeGrid>();
        TopHeight = list.Count * treeGrid.cellSize.y;
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = Tree.Constant.CreateBranchButton();
            UIBranchButton _bbtn = temp.GetComponent <UIBranchButton>();
            temp.transform.SetParent(grid.transform);
            _bbtn.ReadChild(list[i]);
            _bbtn.Show();
            _bbtn.treeIndex = 1;
            _bbtn.id = list[i].id;
            _bbtn.type = list[i].type;
            _bbtn.sn = i;
            temp.GetComponent <Toggle>().group = grid.GetComponent<ToggleGroup>();
            temp.name = list[i].name;
            grid.GetComponent<UITreeGrid>().childs.Add(temp);
        }

        if (list.Count > maxCount)
        {
            arrow1 = Tree.Constant.CreateBranchArrowUp();
            arrow1.transform.SetParent(grid.transform);
            arrow1.transform.SetAsFirstSibling();
            arrow1.SetActive(true);
            arrow1.GetComponent<Button>().onClick.AddListener(treeGrid.OnArrowUpClicked);
            arrow2 = Tree.Constant.CreateBranchArrowDown();
            arrow2.transform.SetParent(grid.transform);
            arrow2.transform.SetAsLastSibling();
            arrow2.SetActive(true);
            arrow2.GetComponent<Button>().onClick.AddListener(treeGrid.OnArrowDownClicked);
            treeGrid.childs.Add(arrow1);
            treeGrid.childs.Add(arrow2);
        }
        grid.GetComponent<UITreeGrid>().OnInit(this);
    }
	
  
    public void OnBranchButtonClicked(object param)
    {
        UIBranchButton target = (UIBranchButton)param;

       
        //设备定位
        if(target.type.Equals("equipment"))
        {
            GameObject g =  EquipmentData.FindGameObjectById(target.id);
            if(g!=null&& g.GetComponent<Object3DElement>()!=null)
            {
                string sceneid = g.GetComponent<Object3DElement>().equipmentData.sceneId;
                //Debug.Log(sceneid);
                Main.instance.stateMachineManager.LocateEquipment(target.id, sceneid);
            }
           
        }

        Debug.Log("OnBranchButtonClicked id:" + target.id + target.name);
    }

    public void BranchRootHideAllChilds()
    {
        UIBranchButton[] _branchs = this.gameObject.GetComponentsInChildren<UIBranchButton>(true);
        for (int i = 0; i < _branchs.Length; i++)
        {
            UIBranchButton temp = _branchs[i];
            temp.ShowChilds(false);
        }
    }

    public UITreeGrid[] GetAllBranchTree()
    {
        return this.GetComponentsInChildren<UITreeGrid>(true);
    }

    public void ResetButtons()
    {
        
    }
}
