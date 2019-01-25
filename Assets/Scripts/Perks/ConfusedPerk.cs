using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfusedPerk : Perk {
	public override void TriggerPerk(PlayerController perkOwner) {
		gameController.TriggerConfusedPerk(perkOwner);
	}
}
