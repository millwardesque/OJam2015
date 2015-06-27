using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	public Slider p1Healthbar;
	PlayerHealth p1Health;

	public Slider p2Healthbar;
	PlayerHealth p2Health;

	public static GUIManager Instance;

	void Awake() {
		if (Instance == null) {
			Instance = this;

			if (p1Healthbar == null) {
				Debug.LogError("Unable to awaken GUI Manager: No P1 Healthbar is set.");
			}
		}
		else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		if (GameManager.Instance.Player1) {
			p1Health = GameManager.Instance.Player1.GetComponent<PlayerHealth>();
			p1Healthbar.minValue = 0;
			p1Healthbar.maxValue = p1Health.maxHP;
			p1Healthbar.value = p1Health.CurrentHP;
		}

		if (GameManager.Instance.Player2) {
			p2Health = GameManager.Instance.Player2.GetComponent<PlayerHealth>();
			p2Healthbar.minValue = 0;
			p2Healthbar.maxValue = p1Health.maxHP;
			p2Healthbar.value = p1Health.CurrentHP;
		}
		else {
			p2Healthbar.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void UpdateGUI() {
		p1Healthbar.value = p1Health.CurrentHP;
		p2Healthbar.value = p2Health.CurrentHP;
	}
}
