using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkImage : MonoBehaviour {

    public Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void SetPerkImage(Perk myPerk)
    {
        image.color = Color.white;
        image.sprite = myPerk.image.sprite;
    }

    public void SetNonePerkImage()
    {
        image.color = Color.clear;
    }
}
