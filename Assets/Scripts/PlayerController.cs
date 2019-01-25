using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed;

    private Vector3 normalMovement;
    private Vector3 invertedMovement;
    private Vector3 flippedMovement;
    private Vector3 invertedFlippedMovement;

    [Header("Perks Bool")]
    public bool IsFreezed;
    public bool IsInverted;

    void Start()
    {

    }

    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.E))
        {
            IsFreezed = !IsFreezed;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            IsInverted = !IsInverted;
        }
    }

    public void MovePlayer()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        
        if (!IsFreezed && !IsInverted)
        {
            transform.position += SetNormalMovement(x, z);
        }

        else if (IsInverted)
        {

        }
    }

    private Vector3 SetNormalMovement(float x, float z)
    {
        return normalMovement = new Vector3(x * MovementSpeed * Time.deltaTime, 0f, z * MovementSpeed * Time.deltaTime);
    }
}