using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class ThroneManager : MonoBehaviour {
	public Timer timer = new Timer();

	private LineRenderer timerLine;
    public Vector3 timerLineStartPosition;
    private Vector3 timerLineEndPosition;
    public float maxWidthTimerLine;
	private DoorManager dm;

	public float duration;

	public List<Throne> thrones = new List<Throne>();

	private void Awake() {
		thrones = FindObjectsOfType<Throne>().ToList();
		dm = FindObjectOfType<DoorManager>();

		timerLine = GetComponentInChildren<LineRenderer>();

        timerLineEndPosition = timerLineStartPosition;
	}

	private void Start() {
		if(thrones.Count > 1) {
			ActiveRandomThrone();
		}

		setTimerLinePositions();
	}

	private void Update() {
		timer.TickTimer();

		timerLineEndPosition.x += maxWidthTimerLine / duration * Time.deltaTime;
		setTimerLinePositions();

		if(timer.CheckTimer(duration)) {
			ActiveRandomThrone();
		}
	}

	public void ActiveRandomThrone() {
		if (thrones.Count > 1) {
			Random rnd = new Random();
			Throne rndThrone;
			do {
				rndThrone = thrones[rnd.Next(0, thrones.Count)];
			} while(rndThrone.isActive == true);
			
			dm.swapDoors();
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
