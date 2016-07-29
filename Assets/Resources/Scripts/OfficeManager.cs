using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class OfficeManager : MonoBehaviour {

	public bool docIsBeingExamined;
	public Text docA, docB, docC;
	public AudioSource choseEnter, choseWrite;
	public Image chosenDocsHolder;

	public GameObject[] documents;

	private int docsChosenSoFar = 0;
	public string[] docDescs;

	public string nextScene;
	private AsyncOperation async;
	public Image outroFade;

	public GameObject newspaper;

	// Use this for initialization
	void Start () {
		StartCoroutine("loadNext");
		for (int i = 0; i < docDescs.Length; i++) {
			//TODO: Check if string is longer than XX chars, and if so, add line break. See WrapText for how to do it;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void DocChosen(int chosen){
		docsChosenSoFar++;
		if (docsChosenSoFar == 1) {
			chosenDocsHolder.rectTransform.DOLocalMoveX (547f, 1f);
			//TODO: Find sound for this, like a typewriter.
			//choseEnter.Play ();
		}
		switch (docsChosenSoFar) {
		case 1:
			docA.text = docDescs [chosen - 1];
			break;
		case 2:
			docB.text = docDescs [chosen - 1];
			break;
		case 3:
			docC.text = docDescs [chosen - 1];
			break;
		}

		//Deactive the chooser thing.
		documents[chosen - 1].GetComponent<DocumentController>().textPrompt.SetActive(false);
		//Remove the doc
		docIsBeingExamined = false;
		documents[chosen - 1].GetComponent<SpriteRenderer>().material.DOFade(0,0.4f).OnComplete(() => documents[chosen - 1].SetActive(false));
		//TODO: Playing a sound here would be nice TBH

		if (docsChosenSoFar == 3) {
			Invoke ("BeginOfEnd", 1f);
		}

	}

	void BeginOfEnd(){
		outroFade.gameObject.SetActive (true);
		outroFade.DOFade (1f, 3f);
		//TODO: Set Headline
		//TODO: Set article
		//TODO: Fade out music probably.
		Invoke("Ending", 4f);
	}

	void Ending(){
		newspaper.GetComponent<Image>().rectTransform.DOLocalMoveY (-348f, 1f);
	}



	IEnumerator loadNext() {
		async = Application.LoadLevelAsync(nextScene);
		async.allowSceneActivation = false;
		yield return async;
	}
}
