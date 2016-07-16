using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("Custom/Dynamics/Blinker")]
public class Blinker : MonoBehaviour {

	public float unblinkTime = 0.4f;
	public Vector2 blinkIntervalRange = new Vector2 (5f, 10f);

	private SpriteRenderer sr;
	private string rendType;
	private Renderer r;
	private Image i;
	private bool isBlinking;
	private Timer blinktimer;


	void Awake() {
		if (GetComponent ("SpriteRenderer") == null) {
			if (GetComponent ("Renderer") == null) {
				if (GetComponent ("Image") == null) {
					return;
				} else {
					i = GetComponent<Image> ();
					rendType = "Image";
				}
			} else {
				r = GetComponent<Renderer> ();
				rendType = "Renderer";
			}
		} else {
			sr = GetComponent<SpriteRenderer> ();
			rendType = "Sprite";
		}

		blinktimer = new Timer (Random.Range (blinkIntervalRange.x, blinkIntervalRange.y), true, false);
		Unblink ();
	}

	void Update() {		
		if (blinktimer.IsFinished()) {
			if (isBlinking) {
				Unblink ();
			} else {
				Blink ();
			}
		}

	}
	void Blink() {
		if (rendType == "Sprite") {
			sr.enabled = false;
		} else if (rendType == "Renderer") {
			r.enabled = false;
		} else {
			i.enabled = false;
		}
		blinktimer.SetInterval (unblinkTime);
		isBlinking = true;
	}
	void Unblink() {
		if (rendType == "Sprite") {
			sr.enabled = true;
		} else if (rendType == "Renderer") {
			r.enabled = true;
		} else {
			i.enabled = true;
		}
		blinktimer.SetInterval (Random.Range (blinkIntervalRange.x, blinkIntervalRange.y));
		isBlinking = false;
	}
}
