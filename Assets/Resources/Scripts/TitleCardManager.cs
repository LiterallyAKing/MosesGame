﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleCardManager : MonoBehaviour {

	public string nextScene;
	public GameObject outrofade;
	public float timeTillNextScene;

	private Timer sceneTimer;
	private Timer fadeTimer;
	private AsyncOperation async;
	private MusicManager musicman;

	// Use this for initialization
	void Start () {
		sceneTimer = new Timer (timeTillNextScene, false);
		StartCoroutine("loadNext");
		float fadetime;
		fadetime = outrofade.GetComponent<Fader> ().travelTime;
		fadeTimer = new Timer (timeTillNextScene - (fadetime * 1.1f), false);
		musicman = GameObject.Find ("MusicManager").GetComponent<MusicManager> ();

		musicman.PlaySong ("TitleCardDriveMusic");
		musicman.PlaySong ("MainSong1");
		musicman.SetSongVolume ("MainSong1", 0.9f);
		//musicman.SongFadeIn ("MainSong1", 1f);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}

		if (fadeTimer.IsFinished () == true) {
			outrofade.SetActive (true);
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
