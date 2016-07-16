using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	[SerializeField] Transform[] toSpawn;
	[SerializeField] bool randomAngle;
	[SerializeField] int initiallySpawned;
	[SerializeField] int maxAllowedSpawned;
	[SerializeField] float startSpawnDelay;
	[SerializeField] float minSpawnTime;
	[SerializeField] float maxSpawnTime;
	[SerializeField] Bounds bounds;
	[SerializeField] int bagDuplicateCount = 2;

	List<Transform> spawnBag = new List<Transform>();

	IEnumerator Start() {

		for (int i = 0; i < initiallySpawned; i++) {
			Spawn();
		}

		yield return new WaitForSeconds(startSpawnDelay);

		if (minSpawnTime > 0.0f || maxSpawnTime > 0.0f) {
			do {
				yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
				if (transform.childCount < maxAllowedSpawned) {
					Spawn();
				}
			} while (true);
		}
	}

	void Spawn() {
		if (spawnBag.Count == 0) {
			foreach (var item in toSpawn) {
				for (int i = 0; i < bagDuplicateCount; i++) {
					spawnBag.Add(item);
				}
			}
		}

		int index = Random.Range(0, spawnBag.Count);
		Transform prefab = spawnBag[index];
		spawnBag.RemoveAt(index);

		Transform instance = (Transform)Instantiate(prefab);
		instance.parent = transform;
		instance.localPosition = RandomE.RandomPoint(bounds);
		instance.localRotation = Quaternion.identity;
	}
}
