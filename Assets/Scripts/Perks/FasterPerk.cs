using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterPerk : Perk
{
    public float perkValue;

    public override void TriggerPerk(PlayerController perkOwner)
    {
        gameController.TriggerFasterPerk(perkOwner, perkValue, duration);
    }
}
