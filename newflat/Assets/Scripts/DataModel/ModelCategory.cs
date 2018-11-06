using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCategory  {


    public string icon;

    public string id;

    public string name;

    public string modelid;

    public ModelCategoryType type;

    public List<ModelCategory> childs;

    
}

public enum ModelCategoryType
{
    None,
    Category,
    Model
}
