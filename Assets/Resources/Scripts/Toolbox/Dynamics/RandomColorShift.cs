using UnityEngine;
using System.Collections;

[AddComponentMenu("Custom/Dynamics/RandomColorShift")]
public class RandomColorShift : MonoBehaviour {


	[SerializeField] float oscSpeed = 6.777f; //6.8
	private float oscValue;
	private Color rainbowColor;
	public Material objMat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		oscValue = Mathf.Sin(Time.time * oscSpeed);


		if (oscValue <= -0.97f)
			rainbowColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

		float colorMult = (oscValue + 1f) * 0.5f;
		objMat.SetColor( "_EmissionColor", rainbowColor * colorMult);
	}
}
