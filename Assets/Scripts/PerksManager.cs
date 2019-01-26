using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class PerksManager : MonoBehaviour {
	public List<GameObject> perksPrefab = new List<GameObject>();

	private List<GameObject> perksSpawnPoint = new List<GameObject>();

	private void Awake() {
		perksSpawnPoint = GameObject.FindGameObjectsWithTag("PerkSpawnPoint").ToList();
	}

	private void Start() {
		SpawnPerks();
	}

	public void SpawnPerks() {
		Random rnd = new Random();
		foreach (GameObject perkSpawnPoint in perksSpawnPoint)
		{
			Instantiate(perksPrefab[rnd.Next(0, perksPrefab.Count)], perkSpawnPoint.transform.position, Quaternion.identity);
		}
	}
}
