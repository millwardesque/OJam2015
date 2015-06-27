using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameOverUI : MonoBehaviour {
	public string restartLevelName;

	public void OnPlayAgain() {
		Application.LoadLevel(restartLevelName);
	}
}
