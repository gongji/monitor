using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveObject3dData  {


    private static  List<Object3DElement> deleteList = new List<Object3DElement>();
   
    public static void AddDeleteEquipmentItem(Object3DElement equipmentItem)
    {
        deleteList.Add(equipmentItem);
    }

    public static  void ClearAllDeleteEquipment()
    {
        deleteList.Clear();
    }
}
