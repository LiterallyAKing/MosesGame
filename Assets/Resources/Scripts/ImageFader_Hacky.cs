using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class ImageFader_Hacky : MonoBehaviour {

	public float travelTime;

	// Use this for initialization
	void Start () {
		Image myimg = GetComponent<Image> ();
		myimg.DOFade (0f, travelTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
