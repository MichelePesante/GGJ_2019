using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSpawnPoint : MonoBehaviour
{
    public Timer timer = new Timer();

    public List<GameObject> perksPrefab = new List<GameObject>();

    public float respawnTime;

    private void Start()
    {
        spawnNewPerk();
    }

    private void Update()
    {
        if (GetComponentInChildren<Perk>() == null)
        {
            timer.TickTimer();

            if (timer.CheckTimer(respawnTime))
            {
                spawnNewPerk();
            }
        }
    }

    private void spawnNewPerk()
    {
        resetTimer();
        Instantiate(perksPrefab[Random.Range(0, perksPrefab.Count)], transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
    }

    public void resetTimer()
    {
        timer.StopTimer();
    }
}
