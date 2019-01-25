using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public List<PlayerController> players = new List<PlayerController>();

    public void TriggerFreezedPerk (PlayerController perkOwner)
    {
        foreach (PlayerController player in players)
        {
            if (player != perkOwner)
            {
                player.EnableFreezedPerk();
            }
        }
    }

    public void TriggerConfusedPerk (PlayerController perkOwner)
    {
        foreach (PlayerController player in players)
        {
            if (player != perkOwner)
            {
                player.EnableConfusedPerk();
            }
        }
    }

    public void TriggerSlowedPerk(PlayerController perkOwner, float slowAmount)
    {
        foreach (PlayerController player in players)
        {
            if (player != perkOwner)
            {
                player.EnableSlowedPerk(slowAmount);
            }
        }
    }

    public void TriggerFasterPerk(PlayerController perkOwner, float speedAmount)
    {
        perkOwner.EnableFasterPerk(speedAmount);
    }
}
