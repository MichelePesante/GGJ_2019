using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePerk : Perk {
	public override void TriggerPerk(PlayerController perkOwner)
    {
        gameController.TriggerFreezedPerk(perkOwner);
    }
}