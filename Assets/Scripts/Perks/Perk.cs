using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Perk : MonoBehaviour
{
    public float duration;
    public Image image;

    private Vector3 PoolPosition = new Vector3 (0f, -20f, 0f);

    protected GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        image = GetComponentInChildren<Image>();
    }

    abstract public void TriggerPerk(PlayerController perkOwner);
    abstract public void SetImage(PerkImage imageToChange);

    public void ReturnToPool()
    {
        transform.position = PoolPosition;
    }
}