using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private List<Door> doors = new List<Door>();

    private void Awake()
    {
        doors = FindObjectsOfType<Door>().ToList();
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
