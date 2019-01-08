using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiMaterialTweenColor : UITweener {
//	public Color from = Color.white;
	public Color to = Color.white;
	
	MeshRenderer[] mMeshRenderers;
	Dictionary<Material,Color> mMatToColorDictionary = new Dictionary<Material, Color>();
	void Awake(){
		mMeshRenderers = GetComponentsInChildren<MeshRenderer>();
		foreach(MeshRenderer render in mMeshRenderers){
			 foreach(Material material in render.materials){
				if(!mMatToColorDictionary.ContainsKey(material)){
					mMatToColorDictionary.Add(material, material.color);
				}
			}
		}
	}
	override protected void OnUpdate(float factor, bool isFinished){
		foreach(var obj in mMatToColorDictionary){
			Material mat = obj.Key;
			Color color = obj.Value;
			mat.color = Color.Lerp(color,to,factor);
		}
	}
}
