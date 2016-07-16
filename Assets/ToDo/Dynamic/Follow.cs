using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform followTF;
	public float followSpeed = 50f;
	
	private Transform tf;

	void Awake() {
		tf = transform;
	}

	void Update () {
		tf.position = Vector3.MoveTowards (tf.position, followTF.position, followSpeed * Time.deltaTime);
	}
}
