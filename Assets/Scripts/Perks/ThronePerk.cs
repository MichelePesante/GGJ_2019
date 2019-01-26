using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThronePerk : Perk
{
    public override void TriggerPerk(PlayerController perkOwner)
    {
        FindObjectOfType<ThroneManager>().ActiveRandomThrone();
    }
}
