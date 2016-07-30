using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpeningManager : MonoBehaviour {

	public string nextScene;
	public GameObject outrofade;
	public float timeTillNextScene;

	private Timer sceneTimer;
	private Timer fadeTimer;
	private float fadetime;
	private Timer fadesongTimer;
	private AsyncOperation async;
	private MusicManager musicman;

	// Use this for initialization
	void Start () {
		sceneTimer = new Timer (timeTillNextScene, false);
		StartCoroutine("loadNext");

		fadetime = outrofade.GetComponent<SpriteFader> ().travelTime;
		fadeTimer = new Timer (timeTillNextScene - (fadetime * 1.1f), false);
		fadesongTimer = new Timer (timeTillNextScene - (fadetime * 2f), false);
		musicman = GameObject.Find ("MusicManager").GetComponent<MusicManager> ();

		//musicman.PlaySong ("TitleCardDriveMusic");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}

		if (fadeTimer.IsFinished () == true) {
			outrofade.SetActive (true);
		}

		if (fadesongTimer.IsFinished () == true) {
			musicman.SongFadeOut ("TitleCardDriveMusic", fadetime * 1.8f);
		}


		if (sceneTimer.IsFinished() == true) {
			async.allowSceneActivation = true;
		}
	}


	IEnumerator loadNext() {
		async = Application.LoadLevelAsync(nextScene);
		async.allowSceneActivation = false;
		yield return async;
	}
}
