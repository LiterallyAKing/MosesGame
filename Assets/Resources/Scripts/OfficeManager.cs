using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	public string[] endingParagraphs;
	string docCombo = "";
	private string articleP1, articleP2, articleP3;

	// Use this for initialization
	void Start () {
		StartCoroutine("loadNext");
//		for (int i = 0; i < docDescs.Length; i++) {
//			//if(docDescs[i].Length > 22)
//			//TODO: Check if string is longer than XX chars, and if so, add line break. See WrapText for how to do it;
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void docChosenNoise(){
		transform.Find ("ChosenNoise").GetComponent<AudioSource> ().Play ();
	}

	public void DocChosen(int chosen){
		docsChosenSoFar++;
	
		docCombo += chosen.ToString ();

		if (docsChosenSoFar == 1) {
			chosenDocsHolder.rectTransform.DOLocalMoveX (547f, 1f);
			//TODO: Find sound for this, like a typewriter.
			//choseEnter.Play ();
		}
		switch (docsChosenSoFar) {
		case 1:
			docA.text = docDescs [chosen - 1];
			articleP1 = endingParagraphs [chosen - 1];
			break;
		case 2:
			docB.text = docDescs [chosen - 1];
			articleP2 = endingParagraphs [chosen - 1];
			break;
		case 3:
			docC.text = docDescs [chosen - 1];
			articleP3 = endingParagraphs [chosen - 1];
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

		sortCombo ();

		newspaper.transform.Find ("Headline").GetComponent<Text> ().text = GenerateHeadLine (docCombo);
		newspaper.transform.Find ("Article").GetComponent<Text> ().text = "    " + articleP1 + '\n' + "    " + articleP2 + '\n' + "    " + articleP3;
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



	string GenerateHeadLine(string chosenDocs){
		string toreturn = "";
		if(chosenDocs == "123") { toreturn = "New Parks for New York City";}
		if(chosenDocs == "124") { toreturn = "Barrons Lose Battle: New Parks in North Shore";}
		if(chosenDocs == "126") { toreturn = "Parks expansion into Queens and Brooklyn";}
		if(chosenDocs == "134") { toreturn = "Barrons Lose Battle: New Parks in North Shore";}
		if(chosenDocs == "136") { toreturn = "Parks bill defeated; Smith vows to fight again in '28";}
		if(chosenDocs == "146") { toreturn = "Smith delivers Parks Across Long Island";}
		if(chosenDocs == "234") { toreturn = "New Parks for New York City";}
		if(chosenDocs == "236") { toreturn = "Parks bill defeated; Smith vows to fight again in '28";}
		if(chosenDocs == "246") { toreturn = "Parks expansion into Queens and Brooklyn";}
		if(chosenDocs == "346") { toreturn = "Smith delivers Parks Across Long Island";}
		return toreturn;
	}

	void sortCombo(){
		string temp = docCombo;
		List<int> templist = new List<int> ();
		templist.Add(int.Parse(docCombo.Substring(0,1)));
		templist.Add(int.Parse(docCombo.Substring(1,1)));
		templist.Add(int.Parse(docCombo.Substring(2,1)));
		templist.Sort ();
		docCombo = "";
		for (int i = 0; i < 3; i++) {
			docCombo += templist [i];
		}
	}

}
