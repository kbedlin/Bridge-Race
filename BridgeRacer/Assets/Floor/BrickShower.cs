using UnityEngine;

public class BrickShower : MonoBehaviour
{
    Brick[,] brickMatrix;

    private void Start()
    {
        //Pobiera macierz cegie³ek z BrickSpawnera
        brickMatrix = GetComponent<BrickSpawner>().brickMatrix;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Jeœli gracz wchodzi na pod³ogê to ujawiane s¹ wszystkie cegie³ki o kolorze tego gracza
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
            //Jeœli wszystkie cegie³ki zosta³y ujawnione niszczy ten skrypt
            if (allActive)
                Destroy(GetComponent<BrickShower>());
        }
    }
}
