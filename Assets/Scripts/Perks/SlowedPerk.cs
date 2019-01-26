using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowedPerk : Perk
{
    public float perkMultiplier;

    public override void TriggerPerk(PlayerController perkOwner)
    {
        gameController.TriggerSlowedPerk(perkOwner, perkMultiplier, duration);
    }
}
