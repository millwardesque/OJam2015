using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private GameObject player1;
	private GameObject player2;
	private GameState currentState;
	private GameState[] availableStates;
	
	public bool useSplitScreen = false;

	public GameObject Player1 {
		get { return player1; }
	}

	public GameObject Player2 {
		get { return player2; }
	}

	public static GameManager Instance = null;

	void Awake() {
		if (Instance == null) {
			Instance = this;

			availableStates = Resources.LoadAll<GameState>("Game States");

			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			if (players.Length > 0) {
				player1 = players[0];
				player1.GetComponent<PlayerMovement>().InputPrefix = "P1";
			}

			if (players.Length > 1) {
				player2 = players[1];
				player2.GetComponent<PlayerMovement>().InputPrefix = "P2";
			}

			if (player1 == null) {
				Debug.LogError("Unable to awaken Game Manager: At least one tagged player is required.");
			}
		}
		else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		if (useSplitScreen) {
			EnableSplitscreen();
		}
		else {
			DisableSplitscreen();
		}

		SetGameState("gs-Escape From Fire");
		GUIManager.Instance.HideWinMessage();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentState != null) {
			currentState.OnUpdate();
		}
		else {
			Debug.LogError ("Whoops! Game Manager update with no active game state!  That shouldn't happen...");
		}
	}

	public void OnPlayerDead() {
		LoadGameOver();
	}

	void LoadGameOver() {
		Application.LoadLevel("Game Over");
	}

	public void OnWin() {
		GUIManager.Instance.ShowWinMessage();
	}

	public void SetGameState(string stateName) {
		GameState newState = FindGameState(stateName);
		if (newState != null) {
			SetGameState(newState);
		}
		else {
			Debug.LogError(string.Format("Unable to set gamestate: State '{0}' wasn't found.", stateName));
		}
	}

	public void SetGameState(GameState newState) {
		if (currentState != null) {
			currentState.OnExit();
			Destroy(currentState.gameObject);
		}

		currentState = newState;
		newState.OnEnter();
	}

	public GameState FindGameState(string stateName) {
		for (int i = 0; i < availableStates.Length; ++i) {
			if (availableStates[i].name == stateName) {
				GameState newState = Instantiate(availableStates[i]) as GameState;
				newState.name = stateName;
				return newState;
			}
		}
		Debug.LogError("Couldn't find game state '" + stateName + "'");
		return null;
	}

	public void EnableSplitscreen() {
		Rect tmp;
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		tmp = camera.GetComponent<Camera>().rect;
		tmp.width = 0.5f;
		camera.GetComponent<Camera>().rect = tmp;

		GameObject p2Camera = GameObject.Instantiate<GameObject>(camera);
		p2Camera.name = "P2 Camera";
		tmp = p2Camera.GetComponent<Camera>().rect;
		tmp.width = 0.5f;
		tmp.x = 0.5f;
		p2Camera.GetComponent<Camera>().rect = tmp;
		p2Camera.GetComponent<AudioListener>().enabled = false;

		// @TODO Set the new camera to follow something.
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		if (players.Length > 1) {
			camera.GetComponent<CameraFollow>().target = players[0].transform;
			p2Camera.GetComponent<CameraFollow>().target = players[1].transform;
		}
		else if (players.Length == 1) {
			camera.GetComponent<CameraFollow>().target = players[0].transform;
			p2Camera.GetComponent<CameraFollow>().target = null;
		}
		else {
			camera.GetComponent<CameraFollow>().target = null;
			p2Camera.GetComponent<CameraFollow>().target = null;
		}
	}

	public void DisableSplitscreen() {
		GameObject p2Camera = GameObject.Find("P2 Camera");
		if (p2Camera != null) {
			Destroy (p2Camera);
		}

		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		Rect tmp = camera.GetComponent<Camera>().rect;
		tmp.width = 1f;
		camera.GetComponent<Camera>().rect = tmp;
	}

	public void RestartGame() {
		Application.LoadLevel(Application.loadedLevelName);
	}
}
