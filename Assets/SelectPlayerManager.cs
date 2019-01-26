using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectPlayerManager : MonoBehaviour {

	private bool player_1 = false;
	private bool player_2 = false;
	private bool player_3 = false;
	private bool player_4 = false;

	public List<Text> texts;

	void Start () {

	}
	
	void Update () {
		if(Input.GetButtonDown("Perk_1") && !player_1) {
			texts[0].text = "Player 1";
			player_1 = true;
		}
		else if(Input.GetButtonDown("Perk_1") && player_1) {
			texts[0].text = "Press to join";
			player_1 = false;
		}

		if(Input.GetButtonDown("Perk_2") && !player_2) {
			texts[1].text = "Player 2";
			player_2 = true;
		}
		else if(Input.GetButtonDown("Perk_2") && player_2) {
			texts[0].text = "Press to join";
			player_2 = false;
		}

		if(Input.GetButtonDown("Perk_3") && !player_3) {
			texts[2].text = "Player 3";
			player_3 = true;
		}
		else if(Input.GetButtonDown("Perk_3") && player_3) {
			texts[2].text = "Press to join";
			player_3 = false;
		}

		if(Input.GetButtonDown("Perk_4") && !player_4) {
			texts[3].text = "Player 4";
			player_4 = true;
		}
		else if(Input.GetButtonDown("Perk_4") && player_4) {
			texts[3].text = "Press to join";
			player_4 = false;
		}
	}

	public void startGame() {
		PlayerPrefs.SetInt("player1", (player_1) ? 1 : 0);
		PlayerPrefs.SetInt("player2", (player_2) ? 1 : 0);
		PlayerPrefs.SetInt("player3", (player_3) ? 1 : 0);
		PlayerPrefs.SetInt("player4", (player_4) ? 1 : 0);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
