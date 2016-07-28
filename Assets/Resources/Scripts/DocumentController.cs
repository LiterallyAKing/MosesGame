using UnityEngine;
using System.Collections;

public class DocumentController : MonoBehaviour {

	public bool selected;

	public Color selectColor;
	private Color originalColor;
	private DocumentZoom doczoomer; 
	private SpriteRenderer sprend;
	private OfficeManager officeman;
	private AudioSource upNoise;
	private AudioSource downNoise;

	private int origRendLayer;

	// Use this for initialization
	void Start () {
		doczoomer = GetComponent<DocumentZoom> ();
		sprend = GetComponent<SpriteRenderer> ();
		officeman = GameObject.Find ("Docs").GetComponent<OfficeManager> ();
		upNoise = transform.Find ("UpNoise").gameObject.GetComponent<AudioSource> ();
		downNoise = transform.Find ("DownNoise").gameObject.GetComponent<AudioSource> ();
		originalColor = sprend.material.color;
		origRendLayer = sprend.sortingOrder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (!selected && !officeman.docIsBeingExamined) {
			Selected ();
		} else if (selected && !doczoomer.isMoving){
			Deselected ();
		}
	}



	void Selected(){
		selected = true;
		upNoise.Play ();
		officeman.docIsBeingExamined = true;
		sprend.sortingOrder = 100;
		doczoomer.Select ();
	}
	void Deselected(){
		selected = false;
		downNoise.Play ();
		officeman.docIsBeingExamined = false;
		sprend.sortingOrder = origRendLayer;
		doczoomer.DeSelect ();
	}





	public void revertColor(){
		sprend.material.color = originalColor;
	}

	void OnMouseEnter(){
		if (!selected && !officeman.docIsBeingExamined) {
			sprend.material.color = selectColor;
		}
	}
	void OnMouseExit(){
		if (!selected) {
			sprend.material.color = originalColor;
		}
	}


}
