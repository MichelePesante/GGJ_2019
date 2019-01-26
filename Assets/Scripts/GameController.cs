using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour {

	public List<PlayerController> players = new List<PlayerController>();

    private void Awake()
    {
        players = FindObjectsOfType<PlayerController>().ToList();
    }

    public void TriggerFreezedPerk (PlayerController perkOwner, float perkDuration)
    {
        Timer timer = new Timer();

        foreach (PlayerController player in players)
        {
            if (player != perkOwner)
            {
                player.EnableFreezedPerk();
            }
        }

        StartCoroutine(CheckFreezedPerk(perkOwner, timer, perkDuration));
    }

    public void TriggerConfusedPerk (PlayerController perkOwner, float perkDuration)
    {
        Timer timer = new Timer();

        foreach (PlayerController player in players)
        {
            if (player != perkOwner)
            {
                player.EnableConfusedPerk();
            }
        }

        StartCoroutine(CheckConfusedPerk(perkOwner, timer, perkDuration));
    }

    public void TriggerSlowedPerk(PlayerController perkOwner, float slowAmount, float perkDuration)
    {
        Timer timer = new Timer();

        foreach (PlayerController player in players)
        {
            if (player != perkOwner)
            {
                player.EnableSlowedPerk(slowAmount);
            }
        }

        StartCoroutine(CheckSlowedPerk(perkOwner, timer, perkDuration));
    }

    public void TriggerFasterPerk(PlayerController perkOwner, float speedAmount, float perkDuration)
    {
        Timer timer = new Timer();

        perkOwner.EnableFasterPerk(speedAmount);

        StartCoroutine(CheckFasterPerk(perkOwner, timer, perkDuration));
    }

    private IEnumerator CheckFreezedPerk(PlayerController perkOwner, Timer timerToCheck, float perkDuration)
    {
        while (!timerToCheck.CheckTimer(perkDuration))
        {
            timerToCheck.TickTimer();
            yield return null;
        }

        if (timerToCheck.CheckTimer(perkDuration))
        {
            foreach (PlayerController player in players)
            {
                if (player != perkOwner)
                {
                    player.DisableFreezedPerk();
                }
            }
        }
    }

    private IEnumerator CheckConfusedPerk(PlayerController perkOwner, Timer timerToCheck, float perkDuration)
    {
        while (!timerToCheck.CheckTimer(perkDuration))
        {
            timerToCheck.TickTimer();
            yield return null;
        }

        if (timerToCheck.CheckTimer(perkDuration))
        {
            foreach (PlayerController player in players)
            {
                if (player != perkOwner)
                {
                    player.DisableConfusedPerk();
                }
            }
        }
    }

    private IEnumerator CheckSlowedPerk(PlayerController perkOwner, Timer timerToCheck, float perkDuration)
    {
        while (!timerToCheck.CheckTimer(perkDuration))
        {
            timerToCheck.TickTimer();
            yield return null;
        }

        if (timerToCheck.CheckTimer(perkDuration))
        {
            foreach (PlayerController player in players)
            {
                if (player != perkOwner)
                {
                    player.DisableSlowedPerk();
                }
            }
        }
    }

    private IEnumerator CheckFasterPerk(PlayerController perkOwner, Timer timerToCheck, float perkDuration)
    {
        while (!timerToCheck.CheckTimer(perkDuration))
        {
            timerToCheck.TickTimer();
            yield return null;
        }

        if (timerToCheck.CheckTimer(perkDuration))
        {
            perkOwner.DisableFasterPerk();
        }
    }
}
