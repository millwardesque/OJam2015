using UnityEngine;
using System.Collections;

public class FireLightingManager : MonoBehaviour {
	public float cycleDuration = 2f;
	float cycleTimeRemaining = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		cycleTimeRemaining -= Time.deltaTime;
		while (cycleTimeRemaining < 0f) {
			cycleTimeRemaining += cycleDuration;
		}

		bool direction = (cycleTimeRemaining / cycleDuration) > 0.5f;
		RenderSettings.ambientIntensity = Mathf.Lerp(0.4f, 1f, (direction ? cycleTimeRemaining / cycleDuration : 1f - (cycleTimeRemaining / cycleDuration)));
	}
}
