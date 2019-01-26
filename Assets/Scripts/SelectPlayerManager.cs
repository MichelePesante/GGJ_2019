using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectPlayerManager : MonoBehaviour {

	public List<bool> players = new List<bool>{false, false, false, false};
	private List<string> actionButtonNames = new List<string>{"Perk_1", "Perk_2", "Perk_3", "Perk_4"};

	public List<Text> texts;

	void Start () {

	}
	
	void Update () {
		for(var i = 0; i < 4; i++) {
			if(Input.GetButtonDown(actionButtonNames[i]) && !players[i]) {
				texts[i].text = "Player 1";
				players[i] = true;
			}
			else if(Input.GetButtonDown(actionButtonNames[i]) && players[i]) {
				texts[1].text = "Press to join";
				players[i] = false;
			}
		}

		if(Input.GetButtonDown("Start")) {
			startGame();
		}
	}

	public void startGame() {
		int activePlayers = 0;
		foreach(bool player in players) {
			if(player)
				activePlayers++;
		}

		if(activePlayers >= 2) {
			PlayerPrefs.SetInt("player1", (players[0]) ? 1 : 0);
			PlayerPrefs.SetInt("player2", (players[0]) ? 1 : 0);
			PlayerPrefs.SetInt("player3", (players[0]) ? 1 : 0);
			PlayerPrefs.SetInt("player4", (players[0]) ? 1 : 0);

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}
