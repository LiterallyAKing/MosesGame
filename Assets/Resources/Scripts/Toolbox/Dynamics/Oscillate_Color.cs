using UnityEngine;
using System.Collections;

[AddComponentMenu("Custom/Dynamics/Oscillate_Color")]
public class Oscillate_Color : MonoBehaviour {
	//This script will interpolate whatever its attached to between two colors
	//Attach it, configure values, and it'll run.
	//Can also be added in code, and then called with Initialize


	enum colormovetype {PointToPoint, Gradient}
	[Header("Origin & Destination")]
	[Tooltip("Whether to go from color to color, or along gradient")]
	[SerializeField] colormovetype colorMoveType = colormovetype.PointToPoint;
	public Color fromPoint;
	public Color toPoint;
	public Gradient colorGradient;
	public float travelTime = 1f;
	public bool beginImmediately = true;


	//enum oscillation_type {Sine, Square, Triangle, Custom}
	[Tooltip("What sort of wave to osccilate by")]
	[Header("Oscillation Type")]
	[SerializeField] Easing.oscillation_type oscillationType = Easing.oscillation_type.Sine;
	public AnimationCurve customOscillation = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

	private Color original_position;
	private bool moving;
	private bool stopnextloop = false;

	void Start () {
		if (beginImmediately)
			Begin ();
	}


	void Update () {
		Color displacement = new Color();
		float gradientLoc = 0;

		if (moving) {
			if (colorMoveType == colormovetype.PointToPoint) {
				switch (oscillationType) {
				case Easing.oscillation_type.Sine:
					displacement = Easing.Oscillate_Sine (fromPoint, toPoint, travelTime, Time.time);
					break;
				case Easing.oscillation_type.Square:
					displacement = Easing.Oscillate_Square (fromPoint, toPoint, travelTime, Time.time);
					break;
				case Easing.oscillation_type.Triangle:
					displacement = Easing.Oscillate_Triangle (fromPoint, toPoint, travelTime, Time.time);
					break;
				case Easing.oscillation_type.Custom:
					displacement = Easing.Oscillate_Custom (fromPoint, toPoint, travelTime, Time.time, customOscillation);
					break;
				}
			} else if (colorMoveType == colormovetype.Gradient) {
				switch (oscillationType) {
				case Easing.oscillation_type.Sine:
					gradientLoc = Easing.Oscillate_Sine (0, 1f, travelTime, Time.time);
					break;
				case Easing.oscillation_type.Square:
					gradientLoc = Easing.Oscillate_Square (0, 1f, travelTime, Time.time);
					break;
				case Easing.oscillation_type.Triangle:
					gradientLoc = Easing.Oscillate_Triangle (0, 1f, travelTime, Time.time);
					break;
				case Easing.oscillation_type.Custom:
					gradientLoc = Easing.Oscillate_Custom (0, 1f, travelTime, Time.time, customOscillation);
					break;
				}
			}

			if (colorMoveType == colormovetype.PointToPoint) {
				GetComponent<Renderer> ().material.color = displacement;
			} else if (colorMoveType == colormovetype.Gradient) {
				GetComponent<Renderer> ().material.color = colorGradient.Evaluate (gradientLoc);
			}



		}
		if (stopnextloop && displacement == fromPoint) {
			moving = false;
			GetComponent<Renderer> ().material.color = original_position;
			Deactivate ();
		}
	}


	public void Initialize(Color _fromPoint, Color _toPoint, float _travelTime, bool _begin, Easing.oscillation_type _oscType){
		fromPoint = _fromPoint;
		toPoint = _toPoint;
		travelTime = _travelTime;
		beginImmediately = _begin;
		oscillationType = _oscType;
	}

	public void Begin(){
		moving = true;
		original_position = GetComponent<Renderer> ().material.color;
	}
	public void End(bool immediately){
		if (immediately) {
			moving = false;
			Deactivate ();
		} else if (!immediately){
			stopnextloop = true;
		}
	}
	public void Pause(){
		moving = false;
	}
	public void Resume(){
		moving = true;
	}

	private void Deactivate(){
		Destroy (this);
		//this.enabled = false;
	}


}
