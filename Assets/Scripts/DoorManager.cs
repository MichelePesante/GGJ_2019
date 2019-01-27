using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private List<Door> doors = new List<Door>();
    private bool isActiveGroup1;

   private void Awake()
    {
        doors = FindObjectsOfType<Door>().ToList();
    }

    public void swapDoors() 
    {
        if (isActiveGroup1) 
        {
            lockDoorsByGroup(DoorGroup.Group1);
            unlockDoorsByGroup(DoorGroup.Group2);
            isActiveGroup1 = false;
        }
        else
        {
            lockDoorsByGroup(DoorGroup.Group2);
            unlockDoorsByGroup(DoorGroup.Group1);
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
}