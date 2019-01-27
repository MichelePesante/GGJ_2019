﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerController PlayerPrefab;
    public Camera myCamera;
    public bool victoryState;
    public ThroneManager tm;
    public Image Exit_Image;
    public Image Resume_Image;

    public bool oldUpTriggerHeld;
    public bool oldDownTriggerHeld;
    public int menuInt;

    public List<PlayerController> players = new List<PlayerController>();
    public List<GameObject> playerSpawnPoints = new List<GameObject>();
    public List<PerkImage> perkImages = new List<PerkImage>();

    public GameObject pauseMenu;

    private void Awake()
    {
        tm = FindObjectOfType<ThroneManager>();
        myCamera = FindObjectOfType<Camera>();

        List<string> playersPrefsName = new List<string> { "player1", "player2", "player3", "player4" };

        for (var i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt(playersPrefsName[i]) == 1)
            {
                players.Add(Instantiate(PlayerPrefab, playerSpawnPoints[i].transform.position, Quaternion.identity).IdentifyPlayer(i));
                players.FindLast(player => true).myPerkImage = perkImages[i];
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start") && victoryState)
        {
            SceneManager.LoadScene(0);
        }
        else if (Input.GetButtonDown("Start"))
        {
            pauseGame();
        }
        if (Time.timeScale == 0f)
        {
            if (Input.GetAxisRaw("Vertical_Player1") <= 0)
            {
                oldUpTriggerHeld = false;
            }

            if (Input.GetAxisRaw("Vertical_Player1") >= 0)
            {
                oldDownTriggerHeld = false;
            }

            if (Input.GetAxisRaw("Vertical_Player1") >= 0.9f && !oldUpTriggerHeld)
            {
                oldUpTriggerHeld = true;
                SwapTexts();
            }

            if (Input.GetAxisRaw("Vertical_Player1") <= 0.9f && !oldDownTriggerHeld)
            {
                oldDownTriggerHeld = true;
                SwapTexts();
            }

            if (Input.GetButtonDown("Perk_1"))
            {
                switch (menuInt)
                {
                    case 0:
                        pauseGame();
                        break;
                    case 1:
                        Application.Quit();
                        break;
                    default:
                        break;
                }
            }
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

    public void pauseGame()
    {
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0f;

            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;

            pauseMenu.SetActive(false);
        }
    }

    public void SwapTexts()
    {
        switch (menuInt)
        {
            case 0:
                ResumeGameTextActive();
                menuInt = 1;
                break;
            case 1:
                ExitGameTextActive();
                menuInt = 0;
                break;
            default:
                break;
        }
    }

    public void ResumeGameTextActive()
    {
        Resume_Image.color = Color.white;
        Exit_Image.color = new Color(160f, 160f, 160f, 1f);
    }

    public void ExitGameTextActive()
    {
        Exit_Image.color = Color.white;
        Resume_Image.color = new Color(160f, 160f, 160f, 1f);
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

    public void GoToVictoryScreen(PlayerController player)
    {
        foreach (PlayerController xplayer in players)
        {
            if (xplayer != player)
            {
                xplayer.transform.position = new Vector3(0f, -20f, 0f);
            }
        }
        player.transform.position = new Vector3(-22.5f, 0f, 40f);
        player.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        myCamera.transform.SetParent(player.meshRenderer.transform, false);
        player.meshRenderer.transform.localRotation = new Quaternion(0f, 90f, 0f, 0f);
        myCamera.transform.localPosition = new Vector3(0f, 2f, 5f);
        myCamera.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
        player.myAnim.speed = 1f;
        player.myAnim.Play("Victory");
        victoryState = true;
        tm.maxWidthTimerLine = 0f;
    }

    #endregion
}