using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Rigidboy gracza
    Rigidbody playerRigidbody;
    //Skrypt wygenerowany z input actions
    public CharacterControls characterControls;
    //Zmienna decyduj�ca czy posta� mo�e si� rusza�
    bool canMove = false;
    //Szybko�� postaci
    float speed = 5;
    //Wektor ruchu postaci pobrany z pozycji joysticka
    public Vector2 inputVector = Vector2.zero;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        //W��cza kontroler postaci
        characterControls = new CharacterControls();
        characterControls.Player.Enable();
    }

    private void FixedUpdate()
    {
        //Je�li mo�esz si� rusza� rusz si�
        if (canMove)
            Move();
    }
    //Skrypt poruszania si�
    private void Move()
    {
        //Pobiera wektor z kontrolera postaci
        inputVector = characterControls.Player.Move.ReadValue<Vector2>();
        //Ustawia wektor pr�dko�ci postaci
        playerRigidbody.velocity = new Vector3(
            inputVector.x * speed,
            playerRigidbody.velocity.y,
            inputVector.y * speed);
        //Obraca posta� w kierunku wektora pr�dko�ci
        if (inputVector.magnitude > 0)
        {
            this.transform.rotation = Quaternion.LookRotation(
                new Vector3(
                    playerRigidbody.velocity.x,
                    0,
                    playerRigidbody.velocity.z));
        }
    }
    //Je�li jeste� w kolizji mo�esz si� rusza�, je�li nie to nie
    private void OnCollisionStay(Collision collision)
    {
        canMove = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        canMove = false;
    }
}
