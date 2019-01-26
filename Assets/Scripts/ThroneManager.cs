using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class ThroneManager : MonoBehaviour {
	private List<Throne> thrones = new List<Throne>();

	private void Awake() {
		thrones = FindObjectsOfType<Throne>().ToList();
	}

	public void ActiveRandomThrone() {
		Random rnd = new Random();
		Throne rndThrone;
		do {
			rndThrone = thrones[rnd.Next(0, thrones.Count)];
		} while(rndThrone.isActive == true);

		rndThrone.Activate();
		DeactivateThrones(rndThrone);
	}

	private void DeactivateThrones(Throne toNotDisable) {
		foreach (Throne throne in thrones)
		{
			if(throne != toNotDisable) {
				throne.Deactivate();
			}
		}
	}
}
