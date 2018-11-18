using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCategory  {

    public string id;

    public string icon;

    public string name;

    public string path;

    public ModelCategoryType type;

    public List<ModelCategory> childs;
    
}

public enum ModelCategoryType
{
    None,
    Category,
    Model
}
