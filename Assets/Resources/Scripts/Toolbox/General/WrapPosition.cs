using UnityEngine;
using System.Collections;

[AddComponentMenu("Custom/General/WrapPosition")]
public class WrapPosition : MonoBehaviour {
	[SerializeField] Bounds bounds;

	void LateUpdate() {
		
		Vector3 position = transform.localPosition;

		for (int i = 0; i < 3; i++) {
				if (position[i] > bounds.max[i]) {
					position[i] -= bounds.max[i] - bounds.min[i];
				} else if (position[i] < bounds.min[i]) {
					position[i] += bounds.max[i] - bounds.min[i];
				}
		}

		if (transform.localPosition != position) {
			transform.localPosition = position;
		}
	}
}
