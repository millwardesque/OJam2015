using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeechBubbleManager : MonoBehaviour {
	public GameObject speechBubblePrefab;
	public GameObject p1Ghost;
	public GameObject p2Ghost;
	public RectTransform canvas;
	GameObject p1GhostSpeechBubble;
	GameObject p2GhostSpeechBubble;

	public static SpeechBubbleManager Instance = null;

	void Awake() {
		if (Instance == null) {
			Instance = this;

			if (speechBubblePrefab == null) {
				Debug.LogError("Unable to awaken speech bubble manager: No speech bubble prefab is set.");
			}
			if (p1Ghost == null) {
				Debug.LogError("Unable to awaken speech bubble manager: Player 1 isn't set.");
			}
			if (p2Ghost == null) {
				Debug.LogError("Unable to awaken speech bubble manager: Player 2 isn't set.");
			}

			p1GhostSpeechBubble = Instantiate<GameObject>(speechBubblePrefab);
			p1GhostSpeechBubble.transform.SetParent(canvas);
			DisablePlayer1Bubble();
		}
		else {
			Destroy (gameObject);
		}
	}

	void Start() {

	}

	void Update() {
		if (p1GhostSpeechBubble.activeInHierarchy) {
			Vector3 screenPos = Camera.main.WorldToViewportPoint(p1Ghost.transform.position);
			screenPos.x *= canvas.rect.width;
			screenPos.y *= canvas.rect.height;
			p1GhostSpeechBubble.transform.position = screenPos;
		}
	}

	public void SetPlayer1Message(string message) {
		p1GhostSpeechBubble.GetComponentInChildren<Text>().text = message;
		EnablePlayer1Bubble();
	}

	public void EnablePlayer1Bubble() {
		p1GhostSpeechBubble.SetActive(true);
	}
	public void DisablePlayer1Bubble() {
		p1GhostSpeechBubble.SetActive(false);
	}
}
