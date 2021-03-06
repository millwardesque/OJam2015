﻿using UnityEngine;
using System.Collections;

public class PlayerHealth : Health {
	public AnimatorOverrideController onFireController;
	public float onFireDuration = 3f;
	public AudioClip damageSound;
	private RuntimeAnimatorController originalController;
	private Animator playerAnimator;
	private float onFireRemaining = 0f;
	private bool isOnFire = false;
	
	void Start() {
		playerAnimator = GetComponentInChildren<Animator>();
		if (!playerAnimator) {
			Debug.LogError(string.Format("Error starting PlayerHealth for {0}: Player doesn't have an animator attached", name));
		}
		originalController = playerAnimator.runtimeAnimatorController;
	}

	void Update() {
		if (isOnFire) {
			if (onFireRemaining > 0) {
				onFireRemaining -= Time.deltaTime;
			}
			else {
				playerAnimator.runtimeAnimatorController = originalController;
				isOnFire = false;
			}
		}
	}

	protected override void OnChange(int oldHP, int newHP) {
		GUIManager.Instance.UpdateGUI();

		if (damageSound) {
			GetComponent<AudioSource>().PlayOneShot(damageSound, 4f);
		}

		if (onFireController != null) {
			if (!isOnFire) {
				isOnFire = true;
				playerAnimator.runtimeAnimatorController = onFireController;
			}
			onFireRemaining = onFireDuration;
		}
	}

	protected override void OnDead() {
		GameManager.Instance.OnPlayerDead();
	}
}
