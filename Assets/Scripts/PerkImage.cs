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

    public void SetFreezePerkImage()
    {
        image.color = Color.cyan;
    }

    public void SetSlowPerkImage()
    {
        image.color = Color.red;
    }

    public void SetFastPerkImage()
    {
        image.color = Color.gray;
    }

    public void SetConfusePerkImage()
    {
        image.color = Color.yellow;
    }

    public void SetThronePerkImage()
    {
        image.color = Color.green;
    }

    public void SetNonePerkImage()
    {
        image.color = Color.clear;
    }
}
