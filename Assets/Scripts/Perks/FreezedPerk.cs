using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezedPerk : Perk
{
    public override void TriggerPerk(PlayerController perkOwner)
    {
        gameController.TriggerFreezedPerk(perkOwner, duration);
    }

    public override void SetImage(PerkImage imageToChange)
    {
        imageToChange.SetPerkImage(this);
    }
}