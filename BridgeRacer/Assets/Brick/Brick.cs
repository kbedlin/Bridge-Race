using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    //Pierwotny kolor ceg³y i jej pierwotna pozycja
    public Vector3 originPosition;
    public Color originColor;

    private void Start()
    {
        //Ustawia pierwotne parametry cegie³ki
        originPosition = this.transform.position;
        originColor = this.GetComponent<Renderer>().material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Jeœli przez ciegie³kê przechodzi gracz o tym samym kolorze zabiera t¹ cegie³kê do "plecaka"
        if (other.gameObject.GetComponent<Renderer>().material.color == originColor &&
            other.gameObject.tag == "Player")
        {
            PushBrick(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Jeœli cegie³ka wypad³a graczowi mo¿e j¹ podnieœæ ka¿dy 
        if (this.GetComponent<Renderer>().material.color == Color.gray &&
            collision.gameObject.tag == "Player")
        {
            PushBrick(collision.gameObject);
            this.GetComponent<Renderer>().material.color = collision.gameObject.GetComponent<Renderer>().material.color;
            this.GetComponent<Collider>().isTrigger = true;
            Destroy(this.GetComponent<Rigidbody>());
        }
    }

    //Ustawia cegie³kê w "plecaku" gracza
    private void PushBrick(GameObject collider)
    {
        this.transform.parent = collider.transform;
        this.transform.rotation = collider.transform.rotation;
        this.transform.localPosition = Vector3.back / 4.5f + Vector3.up / 4 + new Vector3(0, 0.07f, 0) * collider.gameObject.transform.childCount;
        collider.gameObject.GetComponent<BrickHolder>().bricks.Push(this);
    }

    //Przywraca cegie³kê do pierwotnej pozycji
    public void GoOrigin()
    {
        this.transform.parent = null;
        this.GetComponent<Renderer>().material.color = originColor;
        this.transform.position = originPosition;
        this.transform.rotation = Quaternion.identity;
        this.transform.parent = GameObject.Find("Bricks").transform;
        if (this.TryGetComponent<Rigidbody>(out Rigidbody body))
            Destroy(body);
        this.GetComponent<Collider>().isTrigger = true;
    }
}
