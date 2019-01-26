using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class ThroneManager : MonoBehaviour {
	public Timer timer = new Timer();

	private LineRenderer timerLine;
	private Vector3 timerLineStartPosition = new Vector3(-1, 1, 0);
	private Vector3 timerLineEndPosition = new Vector3(-1, 1, 0);

	public float duration;

	public List<Throne> thrones = new List<Throne>();

	private void Awake() {
		thrones = FindObjectsOfType<Throne>().ToList();

		timerLine = GetComponentInChildren<LineRenderer>();
	}

	private void Start() {
		if(thrones.Count > 1) {
			ActiveRandomThrone();
		}

		setTimerLinePositions();
	}

	private void Update() {
		timer.TickTimer();

		timerLineEndPosition.x += 2 / duration * Time.deltaTime;
		setTimerLinePositions();

		if(timer.CheckTimer(duration)) {
			ActiveRandomThrone();
		}

		print(timer.GetTimer());	
	}

	public void ActiveRandomThrone() {
		if (thrones.Count > 1) {
			Random rnd = new Random();
			Throne rndThrone;
			do {
				rndThrone = thrones[rnd.Next(0, thrones.Count)];
			} while(rndThrone.isActive == true);
			
			timerLine.transform.SetParent(rndThrone.gameObject.transform, false);
			rndThrone.Activate();
			DeactivateOtherThrones(rndThrone);

			resetTimerLine();
			timer.StopTimer();
		}
	}

	private void DeactivateOtherThrones(Throne toNotDisable) {
		foreach (Throne throne in thrones)
		{
			if(throne != toNotDisable) {
				throne.Deactivate();
			}
		}
	}

	private void setTimerLinePositions() {
		timerLine.SetPosition(0, timerLineStartPosition);
		timerLine.SetPosition(1, timerLineEndPosition);
	}

	private void resetTimerLine() {
		timerLineEndPosition = timerLineStartPosition;
		setTimerLinePositions();
	}
}
