using UnityEngine;
using UnityEngine.UI;
using System;

[AddComponentMenu("Custom/Dynamics/Fader")]
public class Fader : MonoBehaviour {


	enum fadetype {Fade_In, Fade_Out}
	[SerializeField] fadetype fadeType = fadetype.Fade_Out;
	public float travelTime = 1f;
	public bool beginImmediately = true;
	public bool killWhenFinished = true;
	private float fromPoint = 0, toPoint = 0;

	//enum oscillation_type {Sine, Square, Triangle, Custom}
	[Tooltip("What sort of wave to osccilate by")]
	[Header("Oscillation Type")]
	[SerializeField] Easing.oscillation_type oscillationType = Easing.oscillation_type.Sine;
	public AnimationCurve customOscillation = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

	private float original_alpha;
	private Color original_color;
	private bool moving;
	private float internalTime = 0;

	enum rendtype {Image, Sprite, Renderer}
	private rendtype rendType;
	private Renderer r;
	private Image i;
	private SpriteRenderer sr;

	void Awake() {
		if (GetComponent ("SpriteRenderer") == null) {
			if (GetComponent ("Renderer") == null) {
				if (GetComponent ("Image") == null) {
					return;
				} else {
					i = GetComponent<Image> ();
					rendType = rendtype.Image;
				}
			} else {
				r = GetComponent<Renderer> ();
				rendType = rendtype.Renderer;
			}
		} else {
			sr = GetComponent<SpriteRenderer> ();
			rendType = rendtype.Sprite;
		}
	}

	void Start () {
		if (beginImmediately)
			Begin ();
	}


	void Update () {
		float displacement = 0;

		if (moving) internalTime += Time.deltaTime;
		if (internalTime > travelTime) {
			moving = false;

			Color toshift = original_color;
			toshift.a = toPoint;
			switch (rendType) {
			case rendtype.Image:
				i.color = toshift;
				break;
			case rendtype.Renderer:
				r.material.color = toshift;
				break;
			case rendtype.Sprite:
				sr.color = toshift;
				break;
			}

		}

		if (moving) {
			internalTime += Time.deltaTime;

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

			Color toshift = original_color;
			toshift.a = displacement;
			switch (rendType) {
			case rendtype.Image:
				i.color = toshift;
				break;
			case rendtype.Renderer:
				r.material.color = toshift;
				break;
			case rendtype.Sprite:
				sr.color = toshift;
				break;
			}
		} 



	}


	public void Initialize(bool fadein, float _travelTime, Easing.oscillation_type _oscType){
		if (fadein) {
			fadeType = fadetype.Fade_In;
		} else {
			fadeType = fadetype.Fade_Out;
		}
		travelTime = _travelTime;
		beginImmediately = true;
		oscillationType = _oscType;
		Begin ();
	}

	private void Begin(){
		moving = true;
		switch (rendType) {
		case rendtype.Image:
			original_color = i.color;
			original_alpha = i.color.a;
			break;
		case rendtype.Renderer:
			original_color = r.material.color;
			original_alpha = r.material.color.a;

			r.material.SetFloat ("_Mode",3);
			//material.SetFloat("_Mode", 2);
			r.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			r.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			r.material.SetInt("_ZWrite", 0);
			r.material.DisableKeyword("_ALPHATEST_ON");
			r.material.EnableKeyword("_ALPHABLEND_ON");
			r.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			//r.material.renderQueue = 3000;



			break;
		case rendtype.Sprite:
			original_color = sr.color;
			original_alpha = sr.color.a;
			break;
		}

		if (fadeType == fadetype.Fade_Out) {
			fromPoint = original_alpha;
			toPoint = 0;
		} else {
			fromPoint = 0;
			toPoint = 1f;
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
