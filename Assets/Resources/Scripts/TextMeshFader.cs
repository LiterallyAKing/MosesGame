using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TextMeshFader : MonoBehaviour {




	enum fadetype {Fade_In, Fade_Out}
	[SerializeField] fadetype fadeType = fadetype.Fade_Out;
	public float travelTime = 1f;
	public float beginDelay = 1f;

	private float original_alpha;
	private Color original_color;
	private float alphaval = 0f;


	private TextMesh textmes;

	private Timer delaytimer;

	void Start () {
		textmes = GetComponent < TextMesh> ();
		if (beginDelay == 0) {
			StartFade ();
		} else {
			delaytimer = new Timer (beginDelay, false);
		}
	}

	// Update is called once per frame
	void Update () {
		if (beginDelay > 0 && delaytimer.IsFinished ()) {
			StartFade ();
		}
		Color newval = textmes.color;
		newval.a = alphaval;
		textmes.color = newval;
	}

	void StartFade(){
		DOTween.To(()=> alphaval, x=> alphaval = x, 1f, 1);
	}
}
