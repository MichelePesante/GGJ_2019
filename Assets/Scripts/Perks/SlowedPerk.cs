using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowedPerk : Perk {
	public override void TriggerPerk (PlayerController perkOwner, float perkValue) {
		gameController.TriggerSlowedPerk(perkOwner, perkValue);
	}
}
