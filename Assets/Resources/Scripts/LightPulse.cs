using UnityEngine;
using System.Collections;

[AddComponentMenu("Custom/Dynamics/LightPulse")]
public class LightPulse : MonoBehaviour {
	//This script will hover something via a sine wave
	//Attach it, configure values, and it'll run.
	//Can also be added in code, and then called with Initialize

	[Range(0f,4f)] public float oscillateMagnitude;
	public float travelTime = 1f;

	private Light mylight;
	private float original_intensity;
	private bool moving;

	void Start () {
		mylight = GetComponent<Light> ();
		Begin ();
	}


	void Update () {
		float displacement = 0;
		if (moving) {
			displacement = Easing.Oscillate_Sine (-oscillateMagnitude, oscillateMagnitude, travelTime, Time.time);
			mylight.intensity = original_intensity + displacement;
		}
	}



	public void Begin(){
		moving = true;
		original_intensity = mylight.intensity;
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
