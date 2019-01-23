using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualFrameEquipment {
	public static void Crate(Transform equipment,Material m)
	{
		BoxCollider box = equipment.GetComponent<BoxCollider>();
		if(!box)
		{
			return ;
		}

		Bounds b= box.bounds;

		GameObject wireObject= GameObject.CreatePrimitive(PrimitiveType.Cube);
		wireObject.transform.SetParent(equipment.parent);
		wireObject.transform.localPosition= equipment.transform.localPosition;
		wireObject.transform.localRotation = equipment.transform.localRotation;


	}
}
