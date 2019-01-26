﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorGroup
{
    Group1,
    Group2
}

public class Door : MonoBehaviour
{
    private Animator myAnim;
    public DoorGroup group;

    public bool closed = true;
    public bool locked = false;

    public Material green;
    public Material lightGreen;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();

        switch (gameObject.tag)
        {
            case "Group1":
                this.group = DoorGroup.Group1;
                break;
            case "Group2":
                this.group = DoorGroup.Group2;
                break;
            default:
                break;
        }
    }

    public void openClose()
    {
        if (!locked)
        {
            if (closed)
            {
                open();
            }
            else
            {
                close();
            }
        }
    }

    private void open()
    {
        closed = false;
        myAnim.Play("OpenDoor");
    }

    private void close()
    {
        closed = true;
        myAnim.Play("CloseDoor");
    }

    public void setLocked()
    {
        locked = true;
    }

    public void unlock()
    {
        locked = false;
    }

    public void lightUp()
    {
        GetComponentInChildren<MeshRenderer>().material = lightGreen;
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponentInChildren<MeshRenderer>().material = green;
    }
}
