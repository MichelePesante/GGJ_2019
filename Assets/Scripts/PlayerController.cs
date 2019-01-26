using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    public float StartingMovementSpeed;

    public Perk CurrentPerk;

    private float currentMovementSpeed;

    public ConfusionType CurrentConfusionType;

    [Header("Perks Bool")]
    public bool IsFreezed;
    public bool IsConfused;
    public bool IsSlowed;
    public bool IsFaster;

    void Start()
    {
        ResetMovementSpeed();
    }

    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.E))
        {
            EnableFreezedPerk();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EnableConfusedPerk();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            EnableSlowedPerk(1);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            EnableFasterPerk(1);
        }

        if (Input.GetButtonDown("Perk"))
        {
            CurrentPerk.TriggerPerk(this);
            RemovePersonalPerk();
        }
    }

    public void MovePlayer()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (!IsFreezed)
        {
            if (IsConfused)
            {
                switch (CurrentConfusionType)
                {
                    case ConfusionType.Inverted:
                        transform.position += SetInvertedMovement(x, z);
                        break;
                    case ConfusionType.Flipped:
                        transform.position += SetFlippedMovement(x, z);
                        break;
                    case ConfusionType.InvertedFlipped:
                        transform.position += SetInvertedFlippedMovement(x, z);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                transform.position += SetNormalMovement(x, z);
            }
        }
    }

    private Vector3 SetNormalMovement(float x, float z)
    {
        return new Vector3(x * currentMovementSpeed * Time.deltaTime, 0f, z * currentMovementSpeed * Time.deltaTime);
    }

    private Vector3 SetInvertedMovement(float x, float z)
    {
        return new Vector3(-x * currentMovementSpeed * Time.deltaTime, 0f, -z * currentMovementSpeed * Time.deltaTime);
    }

    private Vector3 SetFlippedMovement(float x, float z)
    {
        return new Vector3(z * currentMovementSpeed * Time.deltaTime, 0f, x * currentMovementSpeed * Time.deltaTime);
    }

    private Vector3 SetInvertedFlippedMovement(float x, float z)
    {
        return new Vector3(-z * currentMovementSpeed * Time.deltaTime, 0f, -x * currentMovementSpeed * Time.deltaTime);
    }

    public void ResetMovementSpeed()
    {
        currentMovementSpeed = StartingMovementSpeed;
    }

    #region Perks

    public void EnableFreezedPerk()
    {
        IsFreezed = true;
    }

    public void DisableFreezedPerk()
    {
        IsFreezed = false;
    }

    public void EnableConfusedPerk()
    {
        IsConfused = true;
        Array values = Enum.GetValues(typeof(ConfusionType));
        Random random = new Random();
        CurrentConfusionType = (ConfusionType)values.GetValue(random.Next(values.Length));
    }

    public void DisableConfusedPerk()
    {
        IsConfused = false;
    }

    public void EnableSlowedPerk(float speedToSub)
    {
        IsSlowed = true;
        currentMovementSpeed -= speedToSub;
    }

    public void DisableSlowedPerk()
    {
        IsSlowed = false;
        ResetMovementSpeed();
    }

    public void EnableFasterPerk(float speedToAdd)
    {
        IsFaster = true;
        currentMovementSpeed += speedToAdd;
    }

    public void DisableFasterPerk()
    {
        IsFaster = false;
        ResetMovementSpeed();
    }

    public void DisableAllPerks()
    {
        DisableFreezedPerk();
        DisableConfusedPerk();
    }

    public void RemovePersonalPerk()
    {
        CurrentPerk = null;
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Perk>() != null && CurrentPerk == null)
        {
            Perk pickedPerk = other.GetComponent<Perk>();
            CurrentPerk = pickedPerk;
            pickedPerk.ReturnToPool();
        }
    }
}

public enum ConfusionType
{
    Inverted = 0,
    Flipped = 1,
    InvertedFlipped = 2
}