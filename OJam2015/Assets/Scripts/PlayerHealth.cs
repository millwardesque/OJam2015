using UnityEngine;
using System.Collections;

public class PlayerHealth : Health {

	protected override void OnChange(int oldHP, int newHP) {
		base.OnChange (oldHP, newHP);
		GUIManager.Instance.UpdateGUI();
	}

	protected override void OnDead() {
		base.OnDead();
		GameManager.Instance.OnPlayerDead();
	}
}
