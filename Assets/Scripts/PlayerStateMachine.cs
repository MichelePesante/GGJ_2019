using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour {

    private Animator myAnim;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    public void SetDoorTrigger()
    {
        myAnim.SetTrigger("DoorTrigger");
    }

    public void SetConfusedBool(bool value)
    {
        myAnim.SetBool("IsConfused", value);
    }

    public void SetMovingBool(bool value)
    {
        myAnim.SetBool("IsMoving", value);
    }

    public void SetFreezeBool(bool value)
    {
        myAnim.SetBool("IsFreezed", value);
    }

    public void ChangeAnimatorSpeed(float speedValue)
    {
        myAnim.speed = speedValue;
    }
}
