using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int maxHP = 1;

	int currentHP;
	public int CurrentHP {
		get { return currentHP; }
	}
	
	void Awake() {
		ResetHP();
	}

	public void ResetHP() {
		currentHP = maxHP;
	}

	public void AdjustHP(int amount) {
		currentHP += amount;
		currentHP = Mathf.Clamp(currentHP, 0, maxHP);
		OnChange(currentHP - amount, currentHP);

		if (currentHP == 0) {
			OnDead ();
		}
	}

	protected virtual void OnChange(int oldHP, int newHP) {
		Debug.Log (string.Format("HP for {0} has changed from {1} to {2}", this.gameObject.name, oldHP, newHP));
	}

	protected virtual void OnDead() {
		Debug.Log (string.Format("HP for {0} has reached 0", this.gameObject.name));
	}
}
