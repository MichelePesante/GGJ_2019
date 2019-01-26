using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    public PlayerNumber CurrentPlayerNumber;

    public float StartingMovementSpeed;

    public Perk CurrentPerk;

    private float currentMovementSpeed;

    public ConfusionType CurrentConfusionType;

    private MeshRenderer meshRenderer;
    private PlayerStateMachine sm;

    [Header("Perks Bool")]
    public bool IsFreezed;
    public bool IsConfused;
    public bool IsSlowed;
    public bool IsFaster;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        sm = GetComponent<PlayerStateMachine>();
    }

    void Start()
    {
        ResetMovementSpeed();
    }

    void Update()
    {
        switch (CurrentPlayerNumber)
        {
            case PlayerNumber.Number_1:
                MovePlayer("Horizontal_Player1", "Vertical_Player1");
                break;
            case PlayerNumber.Number_2:
                MovePlayer("Horizontal_Player2", "Vertical_Player2");
                break;
            case PlayerNumber.Number_3:
                break;
            case PlayerNumber.Number_4:
                break;
            default:
                break;
        }

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

        UsePerk();
    }

    public void MovePlayer(string horizontalAxisName, string verticalAxisName)
    {
        float x = 0f;
        float z = 0f;

        x = Input.GetAxisRaw(horizontalAxisName);
        z = Input.GetAxisRaw(verticalAxisName);

        if (x == 0 && z == 0)
        {
            sm.SetMovingBool(false);
        }
        else
        {
            if (!IsFreezed)
            {
                if (new Vector3(x, 0f, z) != Vector3.zero)
                {
                    meshRenderer.transform.rotation = Quaternion.LookRotation(new Vector3(x, 0f, z));
                }

                sm.SetMovingBool(true);

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
            else
            {
                sm.SetMovingBool(false);
            }
        }
    }

    public void UsePerk()
    {
        if ((Input.GetButtonDown("Perk_1") && CurrentPerk != null && CurrentPlayerNumber == PlayerNumber.Number_1) ||
            (Input.GetButtonDown("Perk_2") && CurrentPerk != null && CurrentPlayerNumber == PlayerNumber.Number_2))
        {
            CurrentPerk.TriggerPerk(this);
            RemovePersonalPerk();
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

    public PlayerController IdentifyPlayer(int playerNumber)
    {
        CurrentPlayerNumber = (PlayerNumber)Enum.GetValues(typeof(PlayerNumber)).GetValue(playerNumber);
        return this;
    }

    #region Perks

    public void EnableFreezedPerk()
    {
        IsFreezed = true;
        sm.SetFreezeBool(true);
    }

    public void DisableFreezedPerk()
    {
        IsFreezed = false;
        sm.SetFreezeBool(false);
    }

    public void EnableConfusedPerk()
    {
        IsConfused = true;
        Array values = Enum.GetValues(typeof(ConfusionType));
        Random random = new Random();
        CurrentConfusionType = (ConfusionType)values.GetValue(random.Next(values.Length));
        sm.SetConfusedBool(true);
    }

    public void DisableConfusedPerk()
    {
        IsConfused = false;
        sm.SetConfusedBool(false);
    }

    public void EnableSlowedPerk(float speedToMultiply)
    {
        IsSlowed = true;
        sm.ChangeAnimatorSpeed(speedToMultiply);
        currentMovementSpeed *= speedToMultiply;
    }

    public void DisableSlowedPerk()
    {
        IsSlowed = false;
        sm.ChangeAnimatorSpeed(1);
        ResetMovementSpeed();
    }

    public void EnableFasterPerk(float speedToMultiply)
    {
        IsFaster = true;
        sm.ChangeAnimatorSpeed(speedToMultiply);
        currentMovementSpeed *= speedToMultiply;
    }

    public void DisableFasterPerk()
    {
        IsFaster = false;
        sm.ChangeAnimatorSpeed(1);
        ResetMovementSpeed();
    }

    public void DisableAllPerks()
    {
        DisableFreezedPerk();
        DisableConfusedPerk();
        DisableSlowedPerk();
        DisableFasterPerk();
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
        else if (other.GetComponent<Door>() != null)
        {
            other.GetComponent<Door>().lightUp();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Door") && other.GetComponent<Door>() != null)
        {
            sm.SetDoorTrigger();
            other.GetComponent<Door>().openClose();
        }
    }
}

public enum ConfusionType
{
    Inverted = 0,
    Flipped = 1,
    InvertedFlipped = 2
}

public enum PlayerNumber
{
    Number_1 = 0,
    Number_2 = 1,
    Number_3 = 2,
    Number_4 = 3
}