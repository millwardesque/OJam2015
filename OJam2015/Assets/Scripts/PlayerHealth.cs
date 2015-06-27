using UnityEngine;
using System.Collections;

public class PlayerHealth : Health {

	protected override void OnChange(int oldHP, int newHP) {
		GUIManager.Instance.UpdateGUI();
	}

	protected override void OnDead() {
		GameManager.Instance.OnPlayerDead();
	}
}
