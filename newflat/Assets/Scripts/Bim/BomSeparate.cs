using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BomSeparate  {
    
    public static void UpdatePostion(float distacne,BimMouse[] bimMouse,Vector3 centerPostion)
    {
        Debug.Log(distacne);
        foreach (BimMouse item in bimMouse)
        {
            item.transform.position = item.defaultPostion +  (item.transform.GetComponent<Collider>().bounds.center - centerPostion).normalized * distacne*5;
        }
    }

}
