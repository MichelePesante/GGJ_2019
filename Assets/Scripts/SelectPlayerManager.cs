using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectPlayerManager : MonoBehaviour
{
    public BlinkText blink;
    public List<bool> players = new List<bool> { false, false, false, false };
    private List<string> actionButtonNames = new List<string> { "Perk_1", "Perk_2", "Perk_3", "Perk_4" };

    public List<Image> images;

    public List<Sprite> sprites;

    int activePlayers = 0;

    void Update()
    {
        for (var i = 0; i < 4; i++)
        {
            if (Input.GetButtonDown(actionButtonNames[i]) && !players[i])
            {
                images[i].sprite = sprites[i * 2 + 1];
                activePlayers++;
                players[i] = true;
            }
            else if (Input.GetButtonDown(actionButtonNames[i]) && players[i])
            {
                images[i].sprite = sprites[i * 2];
                activePlayers--;
                players[i] = false;
            }
        }

        if (Input.GetButtonDown("Start"))
        {
            startGame();
        }

        if (activePlayers >= 2)
        {
            blink.StartAnimation();
        }
        else
        {
            blink.StopAnimation();
        }
    }

    public void startGame()
    { 
        if (activePlayers >= 2)
        {
            PlayerPrefs.SetInt("player1", (players[0]) ? 1 : 0);
            PlayerPrefs.SetInt("player2", (players[1]) ? 1 : 0);
            PlayerPrefs.SetInt("player3", (players[2]) ? 1 : 0);
            PlayerPrefs.SetInt("player4", (players[3]) ? 1 : 0);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}