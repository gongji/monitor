using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CloundMsg  {

	public static void ShowClound()
	{
		FloorRoomSceneAlarm[] rooms = GameObject.FindObjectsOfType<FloorRoomSceneAlarm>();
	
		Debug.Log("rooms="+rooms.Length);
		foreach(FloorRoomSceneAlarm room in rooms)
		{
			GameObject  box =FindObjUtility.GetTransformChildByName(room.transform,Constant.ColliderName.Trim());
			if(box!=null)
			{
				box.GetComponent<BoxCollider>().enabled = true;
				Bounds b = box.GetComponent<BoxCollider>().bounds;
				box.GetComponent<BoxCollider>().enabled = false;
				GameObject plane  = CreateCoundRootGameObject(b);
				plane.name = room.name;
				
				CreateWenduItem(plane ,room.transform);

			}
		}
	}

	public static void RemoveClound()
	{

		foreach(CreateClound temp in createClounds)
		{
			GameObject.DestroyImmediate(temp.gameObject);
		}
		createClounds.Clear();
	}

	private static List<CreateClound> createClounds =new List<CreateClound>();
	private static GameObject CreateCoundRootGameObject(Bounds b)
	{
		GameObject cloundRoot =new GameObject();
		cloundRoot.transform.position = b.center;
		cloundRoot.transform.localRotation =Quaternion.identity;
		cloundRoot.transform.localScale =Vector3.one;
		CreateClound cc= cloundRoot.AddComponent<CreateClound>();
		cc.width = b.size.z;
		cc.length = b.size.x;
		cc.InitComponent();
		cc.CreateMesh();
		createClounds.Add(cc);
		return cloundRoot;
		
	}

	private static void CreateWenduItem(GameObject plane,Transform room)
	{
		WenShiduEquipmentControl[]  WenShiduEquipmentControls = room.GetComponentsInChildren<WenShiduEquipmentControl>();
		List<Transform> cloundPoints =new List<Transform>();
		foreach(WenShiduEquipmentControl wenshiduItem in WenShiduEquipmentControls)
		{
			GameObject cloundPoint = new GameObject();
			cloundPoint.transform.position = wenshiduItem.transform.position;
			cloundPoint.transform.SetParent(plane.transform);
			cloundPoint.name= ((int)UnityEngine.Random.Range(10,100)).ToString();
			cloundPoints.Add(cloundPoint.transform);
		
		}
		plane.GetComponent<CreateClound>().ts = cloundPoints.ToArray();
		plane.GetComponent<CreateClound>().ChangeTemptureValue();
		
	}
}
