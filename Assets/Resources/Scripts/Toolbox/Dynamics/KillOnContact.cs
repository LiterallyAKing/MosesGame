using UnityEngine;
using System.Collections;

[AddComponentMenu("Custom/Dynamics/KillOnContact")]
public class KillOnContact: MonoBehaviour {

	[SerializeField] float timeToDie = 0f;
	[SerializeField] LayerMask killingLayers;
	[SerializeField] MonoBehaviour[] disableOnDeath;

	enum physicstype {_2D, _3D}
	[Tooltip("Whether to detect 2D or 3D triggers")]
	[SerializeField] physicstype physicsType = physicstype._2D;

	enum contacttype {Trigger, Collision}
	[Tooltip("Whether to detect OnTriggerEnter or OnColliderEnter")]
	[SerializeField] contacttype contactType = contacttype.Trigger;

	void OnTriggerEnter2D(Collider2D other) {
		if(physicsType == physicstype._2D && contactType == contacttype.Trigger){
			if (LayerManager.IsInLayerMask(other.gameObject, killingLayers)) {
				foreach (var toDisable in disableOnDeath) {
					toDisable.enabled = false;
				}

				Destroy(gameObject, timeToDie);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if(physicsType == physicstype._3D && contactType == contacttype.Trigger){
			if (LayerManager.IsInLayerMask(other.gameObject, killingLayers)) {
				foreach (var toDisable in disableOnDeath) {
					toDisable.enabled = false;
				}

				Destroy(gameObject, timeToDie);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(physicsType == physicstype._2D && contactType == contacttype.Collision){
			if (LayerManager.IsInLayerMask(other.gameObject, killingLayers)) {
				foreach (var toDisable in disableOnDeath) {
					toDisable.enabled = false;
				}

				Destroy(gameObject, timeToDie);
			}
		}
	}

	void OnCollisionEnter(Collision other) {
		if(physicsType == physicstype._3D && contactType == contacttype.Collision){
			if (LayerManager.IsInLayerMask(other.gameObject, killingLayers)) {
				foreach (var toDisable in disableOnDeath) {
					toDisable.enabled = false;
				}

				Destroy(gameObject, timeToDie);
			}
		}
	}

}
