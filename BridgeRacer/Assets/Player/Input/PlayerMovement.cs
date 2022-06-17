using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody playerRigidbody;
    public CharacterControls characterControls;

    bool canMove = false;

    float speed = 5;

    public Vector2 inputVector = Vector2.zero;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        characterControls = new CharacterControls();
        characterControls.Player.Enable();
    }

    private void FixedUpdate()
    {
        if (canMove)
            Move();
    }

    private void Move()
    {
        inputVector = characterControls.Player.Move.ReadValue<Vector2>();
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

    private void OnCollisionStay(Collision collision)
    {
        canMove = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        canMove = false;
    }
}
