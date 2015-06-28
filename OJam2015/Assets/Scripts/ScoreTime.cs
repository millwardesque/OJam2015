using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTime : MonoBehaviour {

	float startTime = 999;

	float currentTime = 0;

	Text text = null;

	bool shouldStopTimer = false;

	void Start () {
		currentTime = startTime;
		text = gameObject.GetComponent<Text> ();
	}

	private static ScoreTime instance = null;
	void Awake() {
		if (instance && instance.gameObject) {
			Destroy(gameObject);
		}

		instance = this;
	}

	void Update () {
		if (shouldStopTimer) {
			return;
		}

		currentTime -= Time.deltaTime;

		text.text = ((int)currentTime).ToString ();
	}

	public static int EndTime() {
		instance.shouldStopTimer = true;
		return (int)instance.currentTime;
	}
}
