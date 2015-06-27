using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private GameObject player;
	private GameState currentState;
	private GameState[] availableStates;
	
	public bool useSplitScreen = false;

	public GameObject Player {
		get { return player; }
	}

	public static GameManager Instance = null;

	void Awake() {
		if (Instance == null) {
			Instance = this;

			availableStates = Resources.LoadAll<GameState>("Game States");

			player = GameObject.FindGameObjectWithTag("Player");
			if (player == null) {
				Debug.LogError("Unable to awaken Game Manager: No game object has Player tag.");
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
}
