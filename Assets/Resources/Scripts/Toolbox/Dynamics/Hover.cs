using UnityEngine;
using System.Collections;

[AddComponentMenu("Custom/Dynamics/Hover")]
public class Hover : MonoBehaviour {
	//This script will hover something via a sine wave
	//Attach it, configure values, and it'll run.
	//Can also be added in code, and then called with Initialize

	public bool x;
	public bool y;
	public bool z;
	public float magnitude = 1f;
	public float travelTime = 1f;

	private Vector3 original_position;
	private bool moving;

	void Start () {
		Begin ();
	}


	void Update () {
		float displacement = 0;
		if (moving) {
			displacement = Easing.Oscillate_Sine (-magnitude, magnitude, travelTime, Time.time);
			transform.localPosition = original_position + new Vector3 (displacement * (x?1:0), displacement * (y?1:0), displacement * (z?1:0));
		}
	}



	public void Begin(){
		moving = true;
		original_position = transform.localPosition;
	}
	public void End(){
		moving = false;
		Deactivate ();
	}
	public void Pause(){
		moving = false;
	}
	public void Resume(){
		moving = true;
	}

	private void Deactivate(){
		Destroy (this);
	}


}
