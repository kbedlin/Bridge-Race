using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    //Tablica wszystkich pod��g na poziomie
    GameObject[] floors;
    //Platforma finishu
    GameObject finish;

    private void Start()
    {
        floors = GameObject.FindGameObjectsWithTag("Floor");
        finish = GameObject.FindGameObjectWithTag("Finish");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Je�li �cian� dotknie gracz i gracz ma przynajmniej jedn� ceg�� przesu� �cian� do przodu w osi z
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<BrickHolder>().bricks.Count > 0)
            {
                this.transform.Translate(Vector3.down * 0.3f);
                CheckIfShouldBeDestroyed();
            }
        }
    }
    //Je�li ta �ciana znajdzie si� w pod�odze lub w platformie finishu, zniszcz j� 
    private void CheckIfShouldBeDestroyed()
    {
        foreach (var floor in floors)
        {
            if (this.transform.position.z >= floor.transform.position.z - floor.transform.lossyScale.z * 10 / 2 &&
                this.transform.position.z <= floor.transform.position.z + floor.transform.lossyScale.z * 10 / 2)
                Destroy(this.gameObject);
        }
        if (this.transform.position.z >= finish.transform.position.z - finish.transform.lossyScale.z / 2 &&
            this.transform.position.z <= finish.transform.position.z + finish.transform.lossyScale.z / 2)
            Destroy(this.gameObject);
    }
}
