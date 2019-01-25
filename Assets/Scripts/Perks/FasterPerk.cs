using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterPerk : Perk {
	public override void TriggerPerk(PlayerController perkOwner, float perkValue) {
		gameController.TriggerFasterPerk(perkOwner, perkValue);
	}
}
