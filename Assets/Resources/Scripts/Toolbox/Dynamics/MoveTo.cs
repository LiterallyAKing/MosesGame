using UnityEngine;
using System.Collections;

[AddComponentMenu("Custom/Dynamics/MoveTo")]
public class MoveTo : MonoBehaviour {

	[SerializeField] float timeToMove;
	[SerializeField] Vector3 destination;


	IEnumerator Start() {

		Vector3 startPosition = transform.localPosition;

		float dt = 0.0f;
		while (dt < timeToMove) {
			dt += Time.deltaTime;
			transform.localPosition = Vector3.Lerp(startPosition, destination, dt / timeToMove);
			yield return null;
		}

		Destroy(this);
	}
}
