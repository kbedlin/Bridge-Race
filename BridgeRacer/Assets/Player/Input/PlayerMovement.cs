using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Rigidboy gracza
    Rigidbody playerRigidbody;
    //Skrypt wygenerowany z input actions
    public CharacterControls characterControls;
    //Zmienna decyduj¹ca czy postaæ mo¿e siê ruszaæ
    bool canMove = false;
    //Szybkoœæ postaci
    float speed = 5;
    //Wektor ruchu postaci pobrany z pozycji joysticka
    public Vector2 inputVector = Vector2.zero;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        //W³¹cza kontroler postaci
        characterControls = new CharacterControls();
        characterControls.Player.Enable();
    }

    private void FixedUpdate()
    {
        //Jeœli mo¿esz siê ruszaæ rusz siê
        if (canMove)
            Move();
    }
    //Skrypt poruszania siê
    private void Move()
    {
        //Pobiera wektor z kontrolera postaci
        inputVector = characterControls.Player.Move.ReadValue<Vector2>();
        //Ustawia wektor prêdkoœci postaci
        playerRigidbody.velocity = new Vector3(
            inputVector.x * speed,
            playerRigidbody.velocity.y,
            inputVector.y * speed);
        //Obraca postaæ w kierunku wektora prêdkoœci
        if (inputVector.magnitude > 0)
        {
            this.transform.rotation = Quaternion.LookRotation(
                new Vector3(
                    playerRigidbody.velocity.x,
                    0,
                    playerRigidbody.velocity.z));
        }
    }
    //Jeœli jesteœ w kolizji mo¿esz siê ruszaæ, jeœli nie to nie
    private void OnCollisionStay(Collision collision)
    {
        canMove = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        canMove = false;
    }
}
