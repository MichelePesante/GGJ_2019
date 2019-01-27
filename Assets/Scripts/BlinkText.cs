using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    public Animator myAnim;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        myAnim.Play("Blink_Start");
    }

    public void StopAnimation()
    {
        myAnim.Play("Blink_Idle");
    }
}