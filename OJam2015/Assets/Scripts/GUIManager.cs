using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	public Slider p1Healthbar;
	PlayerHealth p1Health;

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
		if (GameManager.Instance.Player) {
			p1Health = GameManager.Instance.Player.GetComponent<PlayerHealth>();
			Debug.Log ("Updating..." + p1Health.CurrentHP);
			p1Healthbar.minValue = 0;
			p1Healthbar.maxValue = p1Health.maxHP;
			p1Healthbar.value = p1Health.CurrentHP;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void UpdateGUI() {
		p1Healthbar.value = p1Health.CurrentHP;
	}
}
