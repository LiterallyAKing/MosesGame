﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SpriteFader : MonoBehaviour {

	enum fadetype {Fade_In, Fade_Out}
	[SerializeField] fadetype fadeType = fadetype.Fade_Out;
	public float travelTime = 1f;
	public float beginDelay = 1f;
	public float toAlpha = 1f;

	private float original_alpha;
	private Color original_color;
	private float alphaval = 0f;


	private SpriteRenderer sprend;

	private Timer delaytimer;

	void Start () {
		if (toAlpha == 0 || toAlpha == 1f) {
			alphaval = Mathf.Abs (1f - toAlpha);
		} else {
			alphaval = 0;
		}


		sprend = GetComponent < SpriteRenderer> ();

		if (beginDelay == 0) {
			StartFade ();
		} else {
			delaytimer = new Timer (beginDelay, false);
		}
		original_alpha = sprend.color.a;
	}

	// Update is called once per frame
	void Update () {

		if (beginDelay > 0 && delaytimer.IsFinished ()) {
			StartFade ();
		}
		Color newval = sprend.color;
		newval.a = alphaval;
		sprend.color = newval;

	}

	void StartFade(){
		DOTween.To (() => alphaval, x => alphaval = x, toAlpha, travelTime);
	}
}
