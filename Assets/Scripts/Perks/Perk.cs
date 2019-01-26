using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perk : MonoBehaviour
{
    public float duration;

    private Vector3 PoolPosition = new Vector3 (10000f, 10000f, 10000f);

    protected GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    abstract public void TriggerPerk(PlayerController perkOwner);

    public void ReturnToPool()
    {
        transform.position = PoolPosition;
    }
}
