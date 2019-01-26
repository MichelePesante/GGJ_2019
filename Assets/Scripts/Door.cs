using System.Collections;
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
        print("OpenClose");
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
}
