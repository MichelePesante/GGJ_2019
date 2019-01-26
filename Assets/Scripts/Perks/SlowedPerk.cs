using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowedPerk : Perk
{
    public float perkValue;

    public override void TriggerPerk(PlayerController perkOwner)
    {
        gameController.TriggerSlowedPerk(perkOwner, perkValue, duration);
    }
}
