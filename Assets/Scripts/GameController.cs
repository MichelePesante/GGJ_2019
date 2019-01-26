using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlayerController PlayerPrefab;

    public int PlayerToSpawn;

    public List<PlayerController> players = new List<PlayerController>();
    public List<GameObject> playerSpawnPoints = new List<GameObject>();

    private void Awake()
    {
        playerSpawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint").ToList();
        
        List<string> playersPrefsName = new List<string>{"player1", "player2", "player3", "player4"};

        for (var i = 0; i < 4; i++) {
            if(PlayerPrefs.GetInt(playersPrefsName[i]) == 1)
                players.Add(Instantiate(PlayerPrefab, playerSpawnPoints[i].transform.position, Quaternion.identity).IdentifyPlayer(i));
        }

        //for (int i = 0; i < PlayerToSpawn; i++)
        //{
        //    players.Add(Instantiate(PlayerPrefab, playerSpawnPoints[i].transform.position, Quaternion.identity).IdentifyPlayer(i));
        //}
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GoToPreviousScene();
        }
    }

    public void TriggerFreezedPerk(PlayerController perkOwner, float perkDuration)
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

    public void TriggerConfusedPerk(PlayerController perkOwner, float perkDuration)
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
        if (!perkOwner.IsFaster)
        {
            perkOwner.EnableFasterPerk(speedAmount);
            StartCoroutine(CheckFasterPerk(perkOwner, perkDuration));
        }
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
                    timerToCheck.StopTimer();
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
                    timerToCheck.StopTimer();
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
                    timerToCheck.StopTimer();
                }
            }
        }
    }

    private IEnumerator CheckFasterPerk(PlayerController perkOwner, float perkDuration)
    {
        while (!perkOwner.fasterTimer.CheckTimer(perkDuration))
        {
            Debug.Log("Timer tick: " + perkOwner.fasterTimer.GetTimer());
            perkOwner.fasterTimer.TickTimer();
            yield return null;
        }

        if (perkOwner.fasterTimer.CheckTimer(perkDuration))
        {
            perkOwner.DisableFasterPerk();
            perkOwner.fasterTimer.StopTimer();
        }
    }

    #region SceneManagement

    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void GoToVictoryScreen()
    {
        // Vai allo screen di vittoria
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    #endregion
}