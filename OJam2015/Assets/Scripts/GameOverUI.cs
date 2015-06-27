using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour {
	public string restartLevelName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPlayAgain() {
		Application.LoadLevel(restartLevelName);
	}
}
