using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeshTextAbstract : MonoBehaviour {

    public  string id { get; set; } 

    
    public abstract Transform Create(string text, Vector3 postion, Transform parentBox);

    
    
}
