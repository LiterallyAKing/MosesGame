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

	public string[] headlines;

	private MusicManager musicman;
	public string curSong;
	bool dayOver = false;

	// Use this for initialization
	void Start () {
		musicman = GameObject.Find ("MusicManager").GetComponent<MusicManager> ();
		dayOver = false;
		StartCoroutine("loadNext");
//		for (int i = 0; i < docDescs.Length; i++) {
//			//if(docDescs[i].Length > 22)
//			//TODO: Check if string is longer than XX chars, and if so, add line break. See WrapText for how to do it;
//		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}

		if (dayOver && Input.GetMouseButtonDown (0)) {
			async.allowSceneActivation = true;
		}
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
		musicman.SongFadeOut (curSong, 5f);
		Invoke("Ending", 4f);
	}

	void Ending(){
		dayOver = true;
		musicman.StopSong ("TitleCardDriveMusic");
		newspaper.GetComponent<Image>().rectTransform.DOLocalMoveY (-348f, 1.4f);
		transform.Find ("NewspaperMoveNoise").GetComponent<AudioSource> ().Play ();
	}



	IEnumerator loadNext() {
		async = Application.LoadLevelAsync(nextScene);
		async.allowSceneActivation = false;
		yield return async;
	}



	string GenerateHeadLine(string chosenDocs){
		string toreturn = "";
		if(chosenDocs == "123") { toreturn = headlines[0];}
		if(chosenDocs == "124") { toreturn = headlines[1];}
		if(chosenDocs == "126") { toreturn = headlines[2];}
		if(chosenDocs == "134") { toreturn = headlines[3];}
		if(chosenDocs == "136") { toreturn = headlines[4];}
		if(chosenDocs == "146") { toreturn = headlines[5];}
		if(chosenDocs == "234") { toreturn = headlines[6];}
		if(chosenDocs == "236") { toreturn = headlines[7];}
		if(chosenDocs == "246") { toreturn = headlines[8];}
		if(chosenDocs == "346") { toreturn = headlines[9];}
		if(chosenDocs == "125") { toreturn = headlines[10];}
		if(chosenDocs == "127") { toreturn = headlines[11];}
		if(chosenDocs == "135") { toreturn = headlines[12];}
		if(chosenDocs == "137") { toreturn = headlines[13];}
		if(chosenDocs == "145") { toreturn = headlines[14];}
		if(chosenDocs == "147") { toreturn = headlines[15];}
		if(chosenDocs == "156") { toreturn = headlines[16];}
		if(chosenDocs == "157") { toreturn = headlines[17];}
		if(chosenDocs == "167") { toreturn = headlines[18];}
		if(chosenDocs == "235") { toreturn = headlines[19];}
		if(chosenDocs == "237") { toreturn = headlines[20];}
		if(chosenDocs == "245") { toreturn = headlines[21];}
		if(chosenDocs == "247") { toreturn = headlines[22];}
		if(chosenDocs == "256") { toreturn = headlines[23];}
		if(chosenDocs == "257") { toreturn = headlines[24];}
		if(chosenDocs == "267") { toreturn = headlines[25];}
		if(chosenDocs == "345") { toreturn = headlines[26];}
		if(chosenDocs == "347") { toreturn = headlines[27];}
		if(chosenDocs == "356") { toreturn = headlines[28];}
		if(chosenDocs == "357") { toreturn = headlines[29];}
		if(chosenDocs == "367") { toreturn = headlines[30];}
		if(chosenDocs == "456") { toreturn = headlines[31];}
		if(chosenDocs == "457") { toreturn = headlines[32];}
		if(chosenDocs == "467") { toreturn = headlines[33];}
		if(chosenDocs == "567") { toreturn = headlines[34];}
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
