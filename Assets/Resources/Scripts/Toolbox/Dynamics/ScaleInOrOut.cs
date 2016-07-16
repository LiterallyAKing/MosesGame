using UnityEngine;
using System.Collections;

[AddComponentMenu("Custom/Dynamics/ScaleInOrOut")]
public class ScaleInOrOut : MonoBehaviour {
	
	enum ScaleDirec {In, Out}
//	[Tooltip("Whether to detect 2D or 3D triggers")]
	[SerializeField] ScaleDirec scaleDirec = ScaleDirec.In;

	public float scaleTime = 1f;
	private float time;
	[Tooltip("What sort of wave to scale by")]
	[SerializeField] Easing.easing_family easeFamily = Easing.easing_family.sine;
	[SerializeField] Easing.easing_type easeType = Easing.easing_type.ease_in;
	public AnimationCurve customEasing = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

	private Vector3 startScale;
	private Vector3 endScale;
	private Vector3 delta;

	void Start(){
		if (scaleDirec == ScaleDirec.In) {
			startScale = Vector3.zero;
			endScale = transform.localScale;
		} else {
			startScale = transform.localScale;
			endScale = Vector3.zero;
		}
		transform.localScale = startScale;
		delta = endScale - startScale;
		time = 0;
	}

	void Update(){
		time += Time.deltaTime;
		if (easeFamily == Easing.easing_family.custom) {
			transform.localScale = Easing.Tween (startScale, delta, scaleTime, time, customEasing, easeType);
		} else {
			transform.localScale = Easing.Tween (startScale, delta, scaleTime, time, easeFamily, easeType);
		}

		if (time > scaleTime) {
			End ();
		}
	}

	void End(){
		if (scaleDirec == ScaleDirec.In) {
			Destroy (this);
		} else {
			Destroy (this.gameObject);
		}
	}


}
