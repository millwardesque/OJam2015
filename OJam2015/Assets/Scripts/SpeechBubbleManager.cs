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

	[HideInInspector]
	public Camera p1Camera;
	[HideInInspector]
	public Camera p2Camera;

	public static SpeechBubbleManager Instance = null;

	void Awake() {
		if (Instance == null) {
			Instance = this;

			if (speechBubblePrefab == null) {
				Debug.LogError("Unable to awaken speech bubble manager: No speech bubble prefab is set.");
			}
			if (p1Ghost == null) {
				Debug.LogError("Unable to awaken speech bubble manager: Player 1 ghost isn't set.");
			}
			if (p2Ghost == null) {
				Debug.LogError("Unable to awaken speech bubble manager: Player 2 ghost isn't set.");
			}

			p1GhostSpeechBubble = Instantiate<GameObject>(speechBubblePrefab);
			p1GhostSpeechBubble.transform.SetParent(canvas, true);
			DisablePlayer1Bubble();

			p2GhostSpeechBubble = Instantiate<GameObject>(speechBubblePrefab);
			p2GhostSpeechBubble.transform.SetParent(canvas, true);
			DisablePlayer2Bubble();
		}
		else {
			Destroy (gameObject);
		}
	}

	void Update() {
		if (p1GhostSpeechBubble.activeInHierarchy) {
			Vector3 anchorPoint = p2Ghost.transform.position + (p2Ghost.GetComponentInChildren<SpriteRenderer>().sprite.bounds.max * 3f);
			Vector3 screenPos = p1Camera.WorldToViewportPoint(anchorPoint);
			screenPos = new Vector3(Mathf.Clamp(screenPos.x, 0.2f, 0.8f), Mathf.Clamp (screenPos.y, 0.1f, 0.95f), screenPos.z);

			screenPos.x *= p1Camera.pixelWidth;
			screenPos.y *= p1Camera.pixelHeight;
			p1GhostSpeechBubble.transform.position = screenPos;
		}

		if (p2GhostSpeechBubble.activeInHierarchy) {
			Vector3 anchorPoint = p1Ghost.transform.position + (p1Ghost.GetComponentInChildren<SpriteRenderer>().sprite.bounds.max * 3f);
			Vector3 screenPos = p2Camera.WorldToViewportPoint(anchorPoint);
			screenPos += new Vector3(1f, 0, 0);
			screenPos = new Vector3(Mathf.Clamp(screenPos.x, 1.2f, 1.8f), Mathf.Clamp (screenPos.y, 0.1f, 0.95f), screenPos.z);

			screenPos.x *= p2Camera.pixelWidth;
			screenPos.y *= p2Camera.pixelHeight;
			p2GhostSpeechBubble.transform.position = screenPos;
		}
	}

	public void SetPlayer1Message(string message) {
		EnablePlayer1Bubble();
		p1GhostSpeechBubble.GetComponentInChildren<Text>().text = message;
	}

	public void EnablePlayer1Bubble() {
		p1GhostSpeechBubble.SetActive(true);
		p1GhostSpeechBubble.transform.localScale = Vector3.one;
	}
	public void DisablePlayer1Bubble() {
		p1GhostSpeechBubble.SetActive(false);
	}

	public void SetPlayer2Message(string message) {
		EnablePlayer2Bubble();
		p2GhostSpeechBubble.GetComponentInChildren<Text>().text = message;
	}
	
	public void EnablePlayer2Bubble() {
		p2GhostSpeechBubble.SetActive(true);
		p2GhostSpeechBubble.transform.localScale = Vector3.one;
	}
	public void DisablePlayer2Bubble() {
		p2GhostSpeechBubble.SetActive(false);
	}
}
