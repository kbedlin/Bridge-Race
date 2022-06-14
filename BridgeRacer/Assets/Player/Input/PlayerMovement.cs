using UnityEngine;

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

    private void OnCollisionStay(Collision collision)
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = characterControls.Player.Move.ReadValue<Vector2>();
        playerRigidbody.velocity = new Vector3(
            inputVector.x * speed,
            playerRigidbody.velocity.y,
            inputVector.y * speed);

        if (inputVector.magnitude > 0)
        {
            this.transform.rotation = Quaternion.LookRotation(
                new Vector3(
                    playerRigidbody.velocity.x,
                    0,
                    playerRigidbody.velocity.z));
        }
    }
}
