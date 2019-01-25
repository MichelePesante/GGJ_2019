using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perk : MonoBehaviour {
	public float duration;

	protected GameController gameController;

	private void Awake() {
		gameController = FindObjectOfType<GameController>();
	}

	virtual public void TriggerPerk(PlayerController perkOwner) {

	}

	virtual public void TriggerPerk(PlayerController perkOwner, float perkValue) {
		
	}
}
