using UnityEngine;
using System.Collections;

[AddComponentMenu("Custom/General/TimedDestroy")]
public class TimedDestroy : MonoBehaviour {

	[SerializeField] float timeToDestroy;

	void Start() {
		Destroy(gameObject, timeToDestroy);
	}
}
