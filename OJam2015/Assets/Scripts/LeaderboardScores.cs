using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeaderboardScores : MonoBehaviour {

	Text text = null;
	Image image = null;

	bool shouldStopTimer = false;
	
	void Start () {

		if (instance && instance.gameObject) {
			Destroy(gameObject);
			return;
		}
		
		instance = this;

		text = gameObject.GetComponent<Text> ();
		image = gameObject.GetComponentInChildren<Image> ();
	}
	
	private static LeaderboardScores instance = null;

	public static Text GetText() {
		if (instance == null) {
			return null;
		}

		return instance.text;
	}

	public static Image GetImage() {
		if (instance == null) {
			return null;
		}

		return instance.image;
	}
}
