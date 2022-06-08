using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody playerRigidbody;
    CharacterControls characterControls;

    float speed = 5;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        characterControls = new CharacterControls();
        characterControls.Player.Enable();
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = characterControls.Player.Move.ReadValue<Vector2>();
        playerRigidbody.velocity = new Vector3(inputVector.x * speed, playerRigidbody.velocity.y, inputVector.y * speed);
    }

}
