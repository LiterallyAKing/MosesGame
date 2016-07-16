using UnityEngine;
using System.Collections;

public class LambdaorCallbacks : MonoBehaviour {

	//Looks they let you assign methods dynamically!

	//http://www.blockypixel.com/2012/09/c-in-unity3d-dynamic-methods-with-lambda-expressions/

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	public float initAlpha;
//
//	enum FaderState { FadedIn, FadingIn, FadedOut, FadingOut};
//	FaderState state;
//
//	Action callback;
//	float currentAlpha;
//	float fadeInTargetAlpha;
//	float fadeOutTargetAlpha;
//
//	public float fadeSpeed = 4f;
//	public Image fadeImage;
//
//	void Start() {
//		currentAlpha = initAlpha;
//		FadeOut (() => { Debug.Log("Hello"); }, 1f);
//	}
//
//	void Update() {
//		if(state == FaderState.FadingIn) {
//			currentAlpha -= fadeSpeed * Time.deltaTime;
//			if(currentAlpha <= fadeInTargetAlpha) {
//				currentAlpha = fadeInTargetAlpha;
//				SetFadeAlpha(currentAlpha);
//				fadeImage.enabled = false;
//				state = FaderState.FadedIn;
//				if(callback != null) {
//					callback();
//				}
//			}
//			else {
//				SetFadeAlpha(currentAlpha);
//			}
//		}
//		if(state == FaderState.FadingOut) {
//			currentAlpha += fadeSpeed * Time.deltaTime;
//			if(currentAlpha >= fadeOutTargetAlpha) {
//				currentAlpha = fadeOutTargetAlpha;
//				SetFadeAlpha(currentAlpha);
//				state = FaderState.FadedOut;
//				if(callback != null) {
//					callback();
//				}
//			}
//			else {
//				SetFadeAlpha(currentAlpha);
//			}
//		}
//	}
//
//	public void FadeIn(Action onFadeInCallback, float targetAlpha = 0f) {
//		currentAlpha = fadeOutTargetAlpha;
//		fadeInTargetAlpha = targetAlpha;
//		SetFadeAlpha(currentAlpha);
//		callback = onFadeInCallback;
//		state = FaderState.FadingIn;
//	}
//
//	public void FadeOut(Action onFadeOutCallback, float targetAlpha = 1f) {
//		currentAlpha = fadeInTargetAlpha;
//		fadeOutTargetAlpha = targetAlpha;
//		SetFadeAlpha(currentAlpha);
//		fadeImage.enabled = true;
//		callback = onFadeOutCallback;
//		state = FaderState.FadingOut;
//	}
//
//	void SetFadeAlpha(float alpha) {
//		Color fadeImageColor = fadeImage.color;
//		fadeImageColor.a = alpha;
//		fadeImage.color = fadeImageColor;
//	}
}
