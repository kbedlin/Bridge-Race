using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontStickToWallsScript : MonoBehaviour
{
    Rigidbody body;
    bool bodyOnRight;

    private void OnCollisionStay(Collision collision)
    {
        //Nie pozwól graczowi na napieranie na œcianê, co mog³o powodowaæ lewitacjê
        body = collision.gameObject.GetComponent<Rigidbody>();
        bodyOnRight = this.transform.position.x < collision.gameObject.transform.position.x;
        
        if ((bodyOnRight && body.velocity.x < 0) ||
            (!bodyOnRight && body.velocity.x > 0))
            body.velocity = new Vector3(0, body.velocity.y, body.velocity.z);
    }
}
