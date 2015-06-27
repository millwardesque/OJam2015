using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private GameObject player;
	private GameState currentState;
	private GameState[] availableStates;

	public GameObject Player {
		get { return player; }
	}

	public static GameManager Instance = null;

	void Awake() {
		if (Instance == null) {
			Instance = this;

			availableStates = Resources.LoadAll<GameState>("Game States");
			for (int i = 0; i < availableStates.Length; ++i) {
				Debug.Log("Loaded state '" + availableStates[i].name + "'");
			}

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
}
