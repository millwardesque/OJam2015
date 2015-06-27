using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int maxHP = 1;

	int currentHP;
	public int CurrentHP {
		get { return currentHP; }
	}
	
	void Start() {
		ResetHP();
	}

	public void ResetHP() {
		currentHP = maxHP;
	}

	public void AdjustHP(int amount) {
		currentHP += amount;
		currentHP = Mathf.Clamp(currentHP, 0, maxHP);

		if (currentHP == 0) {
			OnDead ();
		}
	}

	protected virtual void OnDead() {
		Debug.Log (string.Format("HP for {0} has reached 0", this.gameObject.name));
	}
}
