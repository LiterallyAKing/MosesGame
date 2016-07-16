using UnityEngine;
using System.Collections;

public static class Unity_Utilities {


	public static bool HasParent(this Transform transform, Transform potentialParent) {
		if (transform.parent == null) {
			if (potentialParent == null) {
				return true;
			} else {
				return false;
			}
		} else if (transform.parent == potentialParent) {
			return true;
		} else {
			return transform.parent.HasParent(potentialParent);
		}
	}

	public static void ParentTo(this Transform transform, Transform newParent, 
		Vector3 localPos, 
		Quaternion localRot, 
		Vector3 localScale)
	{
		transform.SetParent(newParent);
		transform.localPosition = localPos;
		transform.localRotation = localRot;
		transform.localScale = localScale;	
	}

	public static void SetChildren(this Transform transform, bool isActive)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(isActive);	
		}
	}
	public static void SetChildren(this GameObject go, int layer, bool includGrandchildren) {
		for (int i = 0; i < go.transform.childCount; i++) {
			go.transform.GetChild(i).gameObject.SetLayers(layer, includGrandchildren);
		}
	}

	public static Component CopyComponent(Component original, GameObject destination)
	{
		System.Type type = original.GetType();
		Component copy = destination.AddComponent(type);
		// Copied fields can be restricted with BindingFlags
		System.Reflection.FieldInfo[] fields = type.GetFields(); 
		foreach (System.Reflection.FieldInfo field in fields)
		{
			Debug.Log(field.Name);
			field.SetValue(copy, field.GetValue(original));
		}
		return copy;
	}
}
