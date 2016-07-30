using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class ImageFader_Hacky_ForCredits : MonoBehaviour {

	public float travelTime;

	// Use this for initialization
	void Start () {
		Image myimg = GetComponent<Image> ();





		Tween myTween = myimg.DOFade (0f, travelTime).OnComplete(() => gameObject.SetActive(false));
		myTween.SetEase (Ease.InSine);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
