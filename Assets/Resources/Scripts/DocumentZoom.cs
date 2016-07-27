using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DocumentZoom : MonoBehaviour {

	public Vector3 zoomtarget;
	public float travelTime;

	private Vector3 origscale;
	private Vector3 origpos;
	private Quaternion origrotat;

	private bool isMoving = false;

	private GameObject shadow;

	// Use this for initialization
	void Start () {
		origpos = transform.localPosition;
		origscale = transform.localScale;
		origrotat = transform.localRotation;
		shadow = transform.Find ("Shadow").gameObject;

		Select ();
		Invoke ("DeSelect", 4f);
	}
	
	// Update is called once per frame
	void Update () {
	}


	public void Select(){
		if (!isMoving) {
			shadow.SetActive (false);
			isMoving = !isMoving;
			transform.DOLocalRotate (new Vector3 (-12.78f, 0, 0), travelTime);
			transform.DOScale (Vector3.one, travelTime);
			transform.DOLocalMove (zoomtarget, travelTime).OnComplete (() => isMoving = !isMoving);
		}
	}

	public void DeSelect(){
		if (!isMoving) {
			shadow.SetActive (true);
			isMoving = !isMoving;
			transform.DOLocalRotate (origrotat.eulerAngles, travelTime/2f);
			transform.DOScale (origscale, travelTime/2f);
			transform.DOLocalMove (origpos, travelTime/2f).OnComplete (() => isMoving = !isMoving);
		}
	}

}
