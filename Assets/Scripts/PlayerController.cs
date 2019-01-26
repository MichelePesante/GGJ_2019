using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    public PlayerNumber CurrentPlayerNumber;

    public float StartingMovementSpeed;

    public PerkImage myPerkImage;

    public Timer fasterTimer = new Timer();

    public Perk CurrentPerk;

    private float currentMovementSpeed;

    private bool oldTriggerHeld;

    public ConfusionType CurrentConfusionType;

    private GameController gameController;

    private string DoorButton;
    private string HorizontalAxis;
    private string VerticalAxis;
    private string PerkButton;

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
        gameController = FindObjectOfType<GameController>();
    }

    void Start()
    {
        ResetMovementSpeed();
        switch (CurrentPlayerNumber)
        {
            case PlayerNumber.Number_1:
                HorizontalAxis = "Horizontal_Player1";
                VerticalAxis = "Vertical_Player1";
                DoorButton = "Door_1";
                PerkButton = "Perk_1";
                break;
            case PlayerNumber.Number_2:
                HorizontalAxis = "Horizontal_Player2";
                VerticalAxis = "Vertical_Player2";
                DoorButton = "Door_2";
                PerkButton = "Perk_2";
                break;
            case PlayerNumber.Number_3:
                HorizontalAxis = "Horizontal_Player3";
                VerticalAxis = "Vertical_Player3";
                DoorButton = "Door_3";
                PerkButton = "Perk_3";
                break;
            case PlayerNumber.Number_4:
                HorizontalAxis = "Horizontal_Player4";
                VerticalAxis = "Vertical_Player4";
                DoorButton = "Door_4";
                PerkButton = "Perk_4";
                break;
            default:
                break;
        }
    }

    void Update()
    {
        MovePlayer(HorizontalAxis, VerticalAxis);

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

        if(Input.GetAxisRaw(DoorButton) <= 0)
        {
            oldTriggerHeld = false;
        }
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
                sm.SetMovingBool(true);

                if (IsConfused)
                {
                    switch (CurrentConfusionType)
                    {
                        case ConfusionType.Inverted:
                            transform.Translate(SetInvertedMovement(x, z));
                            RotatePlayer(-x, -z);
                            break;
                        case ConfusionType.Flipped:
                            transform.Translate(SetFlippedMovement(x, z));
                            RotatePlayer(z, x);
                            break;
                        case ConfusionType.InvertedFlipped:
                            transform.Translate(SetInvertedFlippedMovement(x, z));
                            RotatePlayer(-z, -x);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    transform.Translate(SetNormalMovement(x, z));
                    RotatePlayer(x, z);
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
        if (Input.GetButtonDown(PerkButton) && CurrentPerk != null)
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

    public void RotatePlayer(float x, float z)
    {
        if (new Vector3(x, 0f, z) != Vector3.zero)
        {
            meshRenderer.transform.rotation = Quaternion.LookRotation(new Vector3(x, 0f, z));
        }
    }

    public PlayerController IdentifyPlayer(int playerNumber)
    {
        CurrentPlayerNumber = (PlayerNumber)Enum.GetValues(typeof(PlayerNumber)).GetValue(playerNumber);
        return this;
    }

    public void OpenDoor(Collider other)
    {
        sm.SetDoorTrigger();
        other.GetComponent<Door>().openClose();
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
        Destroy(CurrentPerk.gameObject);
        CurrentPerk = null;
        myPerkImage.SetNonePerkImage();
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Perk>() != null && CurrentPerk == null)
        {
            Perk pickedPerk = other.GetComponent<Perk>();
            CurrentPerk = pickedPerk;
            pickedPerk.SetImage(myPerkImage);
            other.transform.SetParent(null);
            pickedPerk.ReturnToPool();
        }
        else if (other.GetComponent<Door>() != null)
        {
            other.GetComponent<Door>().lightUp();
        }

        if (other.GetComponent<Throne>() != null && other.GetComponent<Throne>().isActive)
        {
            gameController.GoToVictoryScreen();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Door>() != null && Input.GetAxisRaw(DoorButton) >= 0.9f && !oldTriggerHeld)
        {
            oldTriggerHeld = true;
            OpenDoor(other);
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