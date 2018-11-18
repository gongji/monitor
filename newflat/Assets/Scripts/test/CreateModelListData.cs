using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateModelListData : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CreateData();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateData()
    {

        List<ModelCategory> modelCategoryList = new List<ModelCategory>();
        for (int i=1;i<=8;i++)
        {
            //一级目录
            ModelCategory modelCategory = new ModelCategory();
            modelCategory.icon = "tubiao-" + i + ".png";
            modelCategory.id = i.ToString();

           
            modelCategory.type = ModelCategoryType.Category;
            List<ModelCategory> twoList = new List<ModelCategory>();
            for(int k=0;k<5;k++)
            {
                ModelCategory  twomodelCategory = new ModelCategory();
                twomodelCategory.name = i +"-"+(k+1) + "模型类别";
                twomodelCategory.type = ModelCategoryType.Category;

                List<ModelCategory> threeList = new List<ModelCategory>();
                for (int j=0;j<4;j++)
                {
                    ModelCategory threemodelCategory = new ModelCategory();
                    if(k%2==0)
                    {
                        threemodelCategory.icon = "model1" + (j + 1) + ".png";
                    }
                    else
                    {
                        threemodelCategory.icon = "model2" + (j + 1) + ".png";
                    }
                   
                    threemodelCategory.type = ModelCategoryType.Model;
                    threemodelCategory.path = "jigui";
                    threemodelCategory.name = twomodelCategory.name + "_" + j;
                    threeList.Add(threemodelCategory);
                }
                twomodelCategory.childs = threeList;
                twoList.Add(twomodelCategory);
            }
            modelCategory.childs = twoList;
            modelCategoryList.Add(modelCategory);
        }

        FileUtils.WriteContent(Application.streamingAssetsPath + "/modelData.bat", FileUtils.WriteType.Write, Utils.CollectionsConvert.ToJSON(modelCategoryList));

    }
}
