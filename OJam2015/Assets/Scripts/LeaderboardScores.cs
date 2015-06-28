using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeaderboardScores : MonoBehaviour {

	Text text = null;
	
	bool shouldStopTimer = false;
	
	void Start () {
		text = gameObject.GetComponent<Text> ();
	}
	
	private static LeaderboardScores instance = null;
	void Awake() {
		if (instance && instance.gameObject) {
			Destroy(gameObject);
		}
		
		instance = this;
	}

	public static Text GetText() {
		return instance.text;
	}
}
