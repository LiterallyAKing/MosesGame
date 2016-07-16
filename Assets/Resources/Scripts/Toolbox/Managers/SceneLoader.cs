using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	private string scenetopreload;
	private AsyncOperation async;

	public void LoadScene(string scenename){
		SceneManager.LoadScene (scenename, LoadSceneMode.Single);
	}
	public void preloadScene_start(string scenename){
		StopAllCoroutines ();
		scenetopreload = scenename;
		StartCoroutine ("preloadCoroutine");
	}
	public void preloadScene_go(string scenename){
		async.allowSceneActivation = true;
	}



	private IEnumerator preloadCoroutine() {
		async = SceneManager.LoadSceneAsync (scenetopreload);
		async.allowSceneActivation = false;
		yield return async;
	}

}
