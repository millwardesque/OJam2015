using UnityEngine;
using System.Collections;

public class PlayerHealth : Health {

	protected override void OnDead() {
		base.OnDead();
		GameManager.Instance.OnPlayerDead();
	}
}
