using UnityEngine;
using System;
using System.Collections;

public class OneBitObject : MonoBehaviour {

  public bool isSperical, isTerrain;

	void Start() {
    Renderer rndr = GetComponent<MeshRenderer>();
    rndr.material = new Material(Shader.Find("Custom/OneBitObject2"));
    if(isSperical) rndr.material.SetFloat("_Spherical", 1.0f);
    if(isTerrain) rndr.material.SetFloat("_Terrain", 1.0f);
	}

}
