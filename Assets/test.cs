using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour {


	void Start () {
		GameObject.Find ("SceneLoader").GetComponent<SceneLoader> ().LoadScene ("Test");
	}


	// Update is called once per frame
	void Update () {
	}


	void Testing(){
		print ("this is the local script testing");
	}


}
