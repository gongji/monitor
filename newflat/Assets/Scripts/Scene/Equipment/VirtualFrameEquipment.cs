using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class VirtualFrameEquipment {
	public static  GameObject  Create(Transform equipment,Material material)
	{
		BoxCollider box = equipment.GetComponent<BoxCollider>();
		if(!box)
		{
			return null;
		}

		Bounds b= box.bounds;
		GameObject wireObject= GameObject.CreatePrimitive(PrimitiveType.Cube);
		wireObject.transform.SetParent(null);
		wireObject.transform.position = equipment.transform.position + equipment.transform.up * b.size.y/2;
		wireObject.transform.rotation = equipment.transform.rotation;
        wireObject.transform.localScale = b.size;
        wireObject.name = equipment.name + "_VirtualFrame";
        wireObject.GetComponent<MeshRenderer>().material = material;

        return wireObject;
    }
}
