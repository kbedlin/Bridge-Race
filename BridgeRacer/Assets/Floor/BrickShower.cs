using UnityEngine;

public class BrickShower : MonoBehaviour
{
    Brick[,] brickMatrix;

    private void Start()
    {
        //Pobiera macierz cegie�ek z BrickSpawnera
        brickMatrix = GetComponent<BrickSpawner>().brickMatrix;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Je�li gracz wchodzi na pod�og� to ujawiane s� wszystkie cegie�ki o kolorze tego gracza
        if (collision.gameObject.tag == "Player")
        {
            foreach (var brick in brickMatrix)
            {
                if (brick.GetComponent<Renderer>().material.color == collision.gameObject.GetComponent<Renderer>().material.color)
                    brick.gameObject.SetActive(true);
            }
            bool allActive = true;
            foreach (var brick in brickMatrix)
            {
                if (brick.gameObject.activeSelf == false)
                {
                    allActive = false;
                    break;
                }
            }
            //Je�li wszystkie cegie�ki zosta�y ujawnione niszczy ten skrypt
            if (allActive)
                Destroy(GetComponent<BrickShower>());
        }
    }
}
