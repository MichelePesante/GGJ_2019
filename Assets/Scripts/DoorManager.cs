using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private List<Door> doors = new List<Door>();
    private bool isActiveGroup1;
    public bool automaticDoorsOpen;

   private void Awake()
    {
        doors = FindObjectsOfType<Door>().ToList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            automaticDoorsOpen = !automaticDoorsOpen;
        }
    }

    public void swapDoors() 
    {
        if (isActiveGroup1) 
        {
            lockDoorsByGroup(DoorGroup.Group1);
            unlockDoorsByGroup(DoorGroup.Group2);
            if(automaticDoorsOpen)
            {
                OpenUnlockedDoors();
            }
            isActiveGroup1 = false;
        }
        else
        {
            lockDoorsByGroup(DoorGroup.Group2);
            unlockDoorsByGroup(DoorGroup.Group1);
            if (automaticDoorsOpen)
            {
                OpenUnlockedDoors();
            }
            isActiveGroup1 = true;
        }
    }

    public void lockDoorsByGroup(DoorGroup doorGroup)
    {
        foreach (Door door in doors)
        {
            if (door.group == doorGroup)
            {
                door.setLocked();
            }
        }
    }

    public void unlockDoorsByGroup(DoorGroup doorGroup)
    {
        foreach (Door door in doors)
        {
            if (door.group == doorGroup)
            {
                door.unlock();
            }
        }
    }

    public void OpenUnlockedDoors()
    {
        foreach (Door door in doors)
        {
            door.openClose();
        }
    }
}