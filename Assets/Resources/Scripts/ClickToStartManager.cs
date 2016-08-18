using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickToStartManager : MonoBehaviour {
//
//	public string nextScene;
//
//
//	private AsyncOperation async;
//
//
//
//	// Use this for initialization
//	void Start () {
//		StartCoroutine("loadNext");
//	
//	
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if (Input.GetKeyDown (KeyCode.Escape)) {
//			Application.Quit ();
//		}
//
//		if (Time.timeSinceLevelLoad > 2f) {
//			if (GameObject.Find ("Tutorial") == null) {
//				if (Input.GetMouseButtonDown (0)) {
//					async.allowSceneActivation = true;
//				}
//			}
//		}
//			
//	}
//
//
//	IEnumerator loadNext() {
//		async = Application.LoadLevelAsync(nextScene);
//		async.allowSceneActivation = false;
//		yield return async;
//	}
}
