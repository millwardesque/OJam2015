﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireManager : MonoBehaviour {
	public static FireManager Instance = null;
	public Fire firePrefab;
	public Vector2 gridSquareDimensions = new Vector2(1f, 1f);	
	List<Fire> flames = new List<Fire>();
	List<Fire> spreadableFlames = new List<Fire> ();
	List<Fire> nonspreadableFlames = new List<Fire> ();
	int flamesGenerated = 0; 

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
		foreach (GameObject flame in GameObject.FindGameObjectsWithTag("Fire")) {
			SpawnFire(flame.GetComponent<Fire>(), 0, 0);
			Destroy (flame);
		}
	}

	void FixedUpdate() {
		CleanUpFlames();

		for (int i = 0; i < spreadableFlames.Count; ++i) {
			Fire flame = spreadableFlames[i];

			float n = Random.Range(0, 1f);
			if (n < flame.spawnProbability) {
				Vector2 gridOffset = Vector2.zero;
				if (FindFireSpawnPoint(flame, ref gridOffset)) {
					SpawnFire(flame, (int)gridOffset.x, (int)gridOffset.y);
				}
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
			spreadableFlames.Remove (fire);
			nonspreadableFlames.Remove (fire);
		}
	}

	bool FindFireSpawnPoint(Fire spawner, ref Vector2 gridOffset) {
		bool oldRaycastsStartInColliders = Physics2D.raycastsStartInColliders; // Store the current value of raycastsStartInColliders because we're going to change it here temporarily.
		Physics2D.raycastsStartInColliders = false;

		Vector2[] possibleDirections = {
			new Vector2(-1, 0), //Vector2.left, //TODO Change back
			new Vector2(1, 0), //Vector2.right,
			new Vector2(0, 1), //Vector2.up,
			new Vector2(0, -1), //Vector2.down
		};
		List<Vector2> allowedDirections = new List<Vector2>();
		for (int i = 0; i < possibleDirections.Length; ++i) {
			float distanceToAdjacentsquare = new Vector2(possibleDirections[i].x * gridSquareDimensions.x, possibleDirections[i].y * gridSquareDimensions.y).magnitude;
			RaycastHit2D hit = Physics2D.Raycast (spawner.transform.position, possibleDirections[i]);

			if (hit.collider != null) {
				if (hit.distance > distanceToAdjacentsquare) {
					allowedDirections.Add(possibleDirections[i]);
				}
			}
		}
		Physics2D.raycastsStartInColliders = oldRaycastsStartInColliders;	// Restore the old value.

		if (allowedDirections.Count > 0) {
			int selected = Random.Range(0, allowedDirections.Count);
			gridOffset = allowedDirections[selected];
			return true;
		}
		else {
			MakeFlameNonspreadable(spawner);
			return false;
		}
	}

	void MakeFlameNonspreadable(Fire flame) {
		spreadableFlames.Remove(flame);
		nonspreadableFlames.Add (flame);
	}

	void SpawnFire(Fire spawner, int xOffsetUnits, int yOffsetUnits) {
		Vector3 newPosition = spawner.transform.position + new Vector3(xOffsetUnits * gridSquareDimensions.x, yOffsetUnits * gridSquareDimensions.y, 0f);
		float fireSpawnProbability = spawner.spawnProbability;
		StartFire(newPosition, fireSpawnProbability);
	}

	public void StartFire(Vector3 position, float fireSpawnProbability) {
		Fire newFire = Instantiate<Fire>(firePrefab);
		newFire.name = "Flame - " + flamesGenerated;
		newFire.transform.SetParent(transform, true);
		newFire.transform.position = position;
		newFire.spawnProbability = fireSpawnProbability;
		flames.Add(newFire);
		spreadableFlames.Add(newFire);
		flamesGenerated++;
	}
}
