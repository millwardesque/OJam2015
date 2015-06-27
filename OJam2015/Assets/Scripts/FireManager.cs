using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireManager : MonoBehaviour {
	public static FireManager Instance = null;
	public Fire firePrefab;
	public Vector2 gridSquareDimensions = new Vector2(1f, 1f);	
	public float perFlameSpawnProbability = 0.01f;
	public List<Fire> flames = new List<Fire>();

	void Awake() {
		if (Instance == null) {
			Instance = this;

			if (firePrefab == null) {
				Debug.LogError("Unable to start Fire Manager: No fire prefab is set.");
			}
		}
		else {
			Destroy (gameObject);
		}
	}

	void Start() {
		SpawnFire(firePrefab, 1, 1);
	}

	void Update() {
		CleanUpFlames();

		for (int i = 0; i < flames.Count; ++i) {
			float n = Random.Range(0, 1f);
			if (n < perFlameSpawnProbability) {
				int newX = 0;
				int newY = 0;
				while (newX == 0 && newY == 0) {
					newX = Random.Range(-1, 2);
					newY = Random.Range(-1, 2);
				}
				SpawnFire(flames[i], newX, newY);
			}
		}
	}

	void CleanUpFlames() {
		List<Fire> deadList = new List<Fire>();
		for (int i = 0; i < flames.Count; ++i) {
			if (flames[i] == null) {
				deadList.Add(flames[i]);
			}
		}

		foreach (Fire fire in deadList) {
			flames.Remove(fire);
		}
	}

	void SpawnFire(Fire spawner, int xOffsetUnits, int yOffsetUnits) {
		Fire newFire = Instantiate<Fire>(firePrefab);
		newFire.transform.SetParent(transform, true);
		newFire.transform.position = spawner.transform.position + new Vector3(xOffsetUnits * gridSquareDimensions.x, yOffsetUnits * gridSquareDimensions.y, 0f);
		flames.Add(newFire);
	}
}
